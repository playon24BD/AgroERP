using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;

using ERPBO.Agriculture.DomainModels;


using ERPBO.Agriculture.ReportModels;


using ERPBO.Agriculture.DTOModels;

using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class PaymentMoneyReciptBusiness : IPaymentMoneyRecipt
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PaymentMoneyReciptRepository _paymentMoneyReciptRepository;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;
        private readonly AgroProductSalesInfoRepository _agroProductSalesInfoRepository;
        private readonly IAgroProductSalesInfoBusiness _agroProductSalesInfoBusiness;
        private readonly ICommissionOnProductOnSalesBusiness _commissionOnProductOnSalesBusiness;


        //public PaymentMoneyReciptBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)

        public PaymentMoneyReciptBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness, ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness)

        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);
            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
            this._paymentMoneyReciptRepository = new PaymentMoneyReciptRepository(this._agricultureUnitOfWork);
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._commissionOnProductOnSalesBusiness = commissionOnProductOnSalesBusiness;
        }


        public IEnumerable<PaymentMoneyRecipt> GetAllPaymentMoneyRecipt()
        {
            return _paymentMoneyReciptRepository.GetAll().ToList();

        }

        public IEnumerable<MoneyReciptRepotData> GetMoneyReciptReport(string moneyReceipt)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<MoneyReciptRepotData>(QueryForMoneyReciptRepot(moneyReceipt)).ToList();
        }

        private string QueryForMoneyReciptRepot(string moneyReceipt)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(moneyReceipt))
            {
                param += string.Format(@"and mr.MoneyReciptNo ='{0}'", moneyReceipt);
            }
            query = string.Format(@"


select mr.MoneyReciptNo,info.InvoiceNo,s.StockiestName,mr.EntryDate,mr.BankName,mr.BranchName,ph.PaymentDate,mr.TotalAmount,ph.PaymentAmount,
            dbo.fnIntegerToWords(mr.TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText from tblPaymentMoneyRecipt mr

inner join tblProductSalesPaymentHistory ph on mr.PaymentMoneyReciptId=ph.PaymentMoneyReciptId
inner join tblStockiestInfo s on mr.StockiestId=s.StockiestId
inner join tblProductSalesInfo info on ph.ProductSalesInfoId=info.ProductSalesInfoId




Where 1=1 {0}", Utility.ParamChecker(param));

            return query;



        }

        public bool SavePaymentMOneyReciept(PaymentMoneyReciptDTO infoDTO, List<SalesPaymentRegisterDTO> detailsDTO, long userId, long OrgId)
        {
            bool isSucccess = false;
            double total = 0;
            foreach(var bom in detailsDTO)
            {
                double subtotal = bom.PaymentAmount;
                total += subtotal;
            }
            PaymentMoneyRecipt paymentMoneyRecipt = new PaymentMoneyRecipt();

            List<SalesPaymentRegister> salesPaymentRegisters = new List<SalesPaymentRegister>();

            //if (infoDTO.CommisionPercent == 0)
            //{


                paymentMoneyRecipt.PaymentMoneyReciptId = infoDTO.PaymentMoneyReciptId;
                paymentMoneyRecipt.MoneyReciptNo = infoDTO.MoneyReciptNo;
                paymentMoneyRecipt.StockiestId = infoDTO.StockiestId;
                paymentMoneyRecipt.TotalAmount = total;
                paymentMoneyRecipt.EntryDate = DateTime.Now;
                paymentMoneyRecipt.BankName = infoDTO.BankName;
                paymentMoneyRecipt.BranchName = infoDTO.BranchName;
                paymentMoneyRecipt.PaymentMode= infoDTO.PaymentMode;
                paymentMoneyRecipt.AccounrNumber= infoDTO.AccounrNumber;
                //paymentMoneyRecipt.CommisionPercent = infoDTO.CommisionPercent;
                paymentMoneyRecipt.EntryUserId = userId;
                _paymentMoneyReciptRepository.Insert(paymentMoneyRecipt);
                isSucccess = _paymentMoneyReciptRepository.Save();

                foreach(var item in detailsDTO)
                {
                    if (item.PaymentAmount > 0)
                    {
                        SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
                        {
                            PaymentAmount = item.PaymentAmount,
                            ProductSalesInfoId= item.ProductSalesInfoId,
                            PaymentMode= infoDTO.PaymentMode,
                            AccounrNumber=infoDTO.AccounrNumber,
                            PaymentMoneyReciptId=paymentMoneyRecipt.PaymentMoneyReciptId,
                            EntryUserId= userId,
                            PaymentDate= DateTime.Now,

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        salesPayment.PaidAmount += item.PaymentAmount;
                        salesPayment.DueAmount -= item.PaymentAmount;
                        _agroProductSalesInfoRepository.Update(salesPayment);
                    }
                }
                isSucccess= _salesPaymentRegisterRepository.Save();


            //}


            //else
            //{


            //    paymentMoneyRecipt.PaymentMoneyReciptId = infoDTO.PaymentMoneyReciptId;
            //    paymentMoneyRecipt.MoneyReciptNo = infoDTO.MoneyReciptNo;
            //    paymentMoneyRecipt.StockiestId = infoDTO.StockiestId;
            //    //paymentMoneyRecipt.TotalAmount = (total * infoDTO.CommisionPercent)/100;
            //    paymentMoneyRecipt.TotalAmount = total;
            //    paymentMoneyRecipt.EntryDate = DateTime.Now;
            //    paymentMoneyRecipt.BankName = infoDTO.BankName;
            //    paymentMoneyRecipt.BranchName = infoDTO.BranchName;
            //    paymentMoneyRecipt.PaymentMode = infoDTO.PaymentMode;
            //    paymentMoneyRecipt.AccounrNumber = infoDTO.AccounrNumber;
            //    paymentMoneyRecipt.CommisionPercent = infoDTO.CommisionPercent;
            //    paymentMoneyRecipt.EntryUserId = userId;
            //    _paymentMoneyReciptRepository.Insert(paymentMoneyRecipt);
            //    isSucccess = _paymentMoneyReciptRepository.Save();

            //    foreach (var item in detailsDTO)
            //    {
            //        if (item.PaymentAmount > 0)
            //        {
            //            SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
            //            {
            //                PaymentAmount = item.PaymentAmount,
            //                ProductSalesInfoId = item.ProductSalesInfoId,
            //                PaymentMode = infoDTO.PaymentMode,
            //                AccounrNumber = infoDTO.AccounrNumber,
            //                PaymentMoneyReciptId = paymentMoneyRecipt.PaymentMoneyReciptId,
            //                EntryUserId = userId,
            //                PaymentDate = DateTime.Now,

            //            };
            //            _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

            //            var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
            //            salesPayment.PaidAmount += item.PaymentAmount;
            //            salesPayment.DueAmount -= item.PaymentAmount;
            //            _agroProductSalesInfoRepository.Update(salesPayment);
            //            ;
            //        }
            //    }
            //    isSucccess = _salesPaymentRegisterRepository.Save();


            //}

            return isSucccess;
        }

        public IEnumerable<PaymentMoneyReciptDTO> GetLastMoneyRecipt(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PaymentMoneyReciptDTO>(QueryForLastMoneyRecipt(orgId)).ToList();
        }

        private string QueryForLastMoneyRecipt(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"
select top 1 * from tblPaymentMoneyRecipt
order by PaymentMoneyReciptId desc

", Utility.ParamChecker(param));

            return query;
        }

    }
}

