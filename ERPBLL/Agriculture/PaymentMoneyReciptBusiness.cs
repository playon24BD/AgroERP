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
            foreach (var bom in detailsDTO)
            {
                double subtotal = bom.PaymentAmount;
                total += subtotal;
            }
            PaymentMoneyRecipt paymentMoneyRecipt = new PaymentMoneyRecipt();

            List<SalesPaymentRegister> salesPaymentRegisters = new List<SalesPaymentRegister>();


            paymentMoneyRecipt.PaymentMoneyReciptId = infoDTO.PaymentMoneyReciptId;
            paymentMoneyRecipt.MoneyReciptNo = infoDTO.MoneyReciptNo;
            paymentMoneyRecipt.StockiestId = infoDTO.StockiestId;
            paymentMoneyRecipt.TotalAmount = total;
            paymentMoneyRecipt.EntryDate = DateTime.Now;
            paymentMoneyRecipt.BankName = infoDTO.BankName;
            paymentMoneyRecipt.BranchName = infoDTO.BranchName;
            paymentMoneyRecipt.PaymentMode = infoDTO.PaymentMode;
            paymentMoneyRecipt.AccounrNumber = infoDTO.AccounrNumber;
            paymentMoneyRecipt.EntryUserId = userId;
            _paymentMoneyReciptRepository.Insert(paymentMoneyRecipt);
            isSucccess = _paymentMoneyReciptRepository.Save();

            foreach (var item in detailsDTO)
            {
                if (item.PaymentAmount > 0)
                {
                    if (item.CommisionPercent > 0)
                    {
                        SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
                        {
                            PaymentAmount = item.PaymentAmount,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            PaymentMode = infoDTO.PaymentMode,
                            AccounrNumber = infoDTO.AccounrNumber,
                            PaymentMoneyReciptId = paymentMoneyRecipt.PaymentMoneyReciptId,
                            EntryUserId = userId,
                            PaymentDate = DateTime.Now,
                            CommisionPercent = item.CommisionPercent,
                            CommisionAmount = (item.PaymentAmount * item.CommisionPercent)/100,

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        salesPayment.PaidAmount += item.PaymentAmount;
                        salesPayment.DueAmount -= item.PaymentAmount;
                        _agroProductSalesInfoRepository.Update(salesPayment);

                    }
                    else
                    {
                        SalesPaymentRegister salesPaymentRegister = new SalesPaymentRegister()
                        {
                            PaymentAmount = item.PaymentAmount,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            PaymentMode = infoDTO.PaymentMode,
                            AccounrNumber = infoDTO.AccounrNumber,
                            PaymentMoneyReciptId = paymentMoneyRecipt.PaymentMoneyReciptId,
                            EntryUserId = userId,
                            PaymentDate = DateTime.Now,
                            CommisionPercent = 0,
                            CommisionAmount = 0,
                       

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        salesPayment.PaidAmount += item.PaymentAmount;
                        salesPayment.DueAmount -= item.PaymentAmount;
                        _agroProductSalesInfoRepository.Update(salesPayment);

                    }

                }
            }
            isSucccess = _salesPaymentRegisterRepository.Save();




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

        public IEnumerable<PaymentMoneyReciptDTO> GetMoneyReceiptList(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PaymentMoneyReciptDTO>(QueryForMoneyReceiptList(moneyReceiptNo,stockiestId,fromDate,toDate)).ToList();
        }

        private string QueryForMoneyReceiptList(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(moneyReceiptNo))
            {
                param += string.Format(@"and PMR.MoneyReciptNo like '%{0}%'", moneyReceiptNo);
            }
            if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and PMR.StockiestId={0}", stockiestId);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(PMR.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(PMR.EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(PMR.EntryDate as date)='{0}'", tDate);
            }


            query = string.Format(@"

SELECT PMR.PaymentMoneyReciptId,PMR.MoneyReciptNo,PMR.StockiestId,f.StockiestName,PMR.BankName,PMR.BranchName,PMR.TotalAmount,PMR.EntryDate
 FROM tblPaymentMoneyRecipt PMR 
 INNER JOIN  tblStockiestInfo f
 on PMR.StockiestId=f.StockiestId
 
where 1=1 {0}

", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<PaymentMoneyReciptDTO> GetMoneyReceiptById(long id, long orgId)
        {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<PaymentMoneyReciptDTO>(QueryForMoneyReceiptDetails(id,orgId)).ToList();

        }

        private string QueryForMoneyReceiptDetails(long id, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (id != 0 && id > 0)
            {
                param += string.Format(@" and h.PaymentMoneyReciptId={0}", id);
            }

            query = string.Format(@"



select h.PaymentMoneyReciptId,i.InvoiceNo,h.PaymentAmount,h.CommisionPercent, h.CommisionAmount from tblPaymentMoneyRecipt m
inner join tblProductSalesPaymentHistory h on m.PaymentMoneyReciptId= h.PaymentMoneyReciptId
inner join tblProductSalesInfo i on h.ProductSalesInfoId=i.ProductSalesInfoId
Where 1=1 {0} ", Utility.ParamChecker(param));

            return query;
        }
    }
}

