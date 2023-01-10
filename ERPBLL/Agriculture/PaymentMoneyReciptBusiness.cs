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
            paymentMoneyRecipt.Status = "Pending";
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
                            CommisionPercent = 0,
                            CommisionAmount = item.CommisionPercent,
                            Status = "Pending",

                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        //var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        //salesPayment.PaidAmount += item.PaymentAmount;
                        //salesPayment.DueAmount -= item.PaymentAmount;
                        //_agroProductSalesInfoRepository.Update(salesPayment);

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
                            Status = "Pending",


                        };
                        _salesPaymentRegisterRepository.Insert(salesPaymentRegister);

                        //var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(item.ProductSalesInfoId);
                        //salesPayment.PaidAmount += item.PaymentAmount;
                        //salesPayment.DueAmount -= item.PaymentAmount;
                        //_agroProductSalesInfoRepository.Update(salesPayment);

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

SELECT PMR.Status,PMR.PaymentMoneyReciptId,PMR.MoneyReciptNo,PMR.StockiestId,f.StockiestName,PMR.BankName,PMR.BranchName,PMR.TotalAmount,PMR.EntryDate
 FROM tblPaymentMoneyRecipt PMR 
 INNER JOIN  tblStockiestInfo f
 on PMR.StockiestId=f.StockiestId
 
where 1=1 {0} order by PMR.PaymentMoneyReciptId desc

", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<SalesPaymentRegisterDTO> GetMoneyReceiptById(long id, long orgId)
        {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesPaymentRegisterDTO>(QueryForMoneyReceiptDetails(id,orgId)).ToList();

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



select i.ProductSalesInfoId,h.PaymentRegisterID,h.PaymentMoneyReciptId,i.InvoiceNo,h.PaymentAmount,h.CommisionPercent, h.CommisionAmount from tblPaymentMoneyRecipt m
inner join tblProductSalesPaymentHistory h on m.PaymentMoneyReciptId= h.PaymentMoneyReciptId
inner join tblProductSalesInfo i on h.ProductSalesInfoId=i.ProductSalesInfoId
Where 1=1 {0} ", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<PaymentMoneyReciptDTO> GetMoneyReceiptListApprove(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PaymentMoneyReciptDTO>(QueryForMoneyReceiptListApprove(moneyReceiptNo, stockiestId, fromDate, toDate)).ToList();
        }

        private string QueryForMoneyReceiptListApprove(string moneyReceiptNo, long? stockiestId, string fromDate, string toDate)
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

SELECT PMR.PaymentMoneyReciptId,PMR.MoneyReciptNo,PMR.StockiestId,f.StockiestName,PMR.BankName,PMR.BranchName,PMR.TotalAmount,PMR.EntryDate,PMR.Status
 FROM tblPaymentMoneyRecipt PMR 
 INNER JOIN  tblStockiestInfo f
 on PMR.StockiestId=f.StockiestId
 
where 1=1 {0} and PMR.Status = 'Pending' order by PMR.PaymentMoneyReciptId desc

", Utility.ParamChecker(param));

            return query;
        }


        public PaymentMoneyRecipt getmoneyreciptbyid(long PaymentMoneyReciptId)
        {
            return _paymentMoneyReciptRepository.GetOneByOrg(a => a.PaymentMoneyReciptId == PaymentMoneyReciptId);
        }
        public SalesPaymentRegister getSalesPaymentRegisterbyid(long PaymentRegisterID)
        {
            return _salesPaymentRegisterRepository.GetOneByOrg(a => a.PaymentRegisterID == PaymentRegisterID);
        }
        public AgroProductSalesInfo getinvoicebyid(long ProductSalesInfoId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(s => s.ProductSalesInfoId == ProductSalesInfoId);
        }

        public bool SavePaymentMOneyRecieptApprove(PaymentMoneyReciptDTO infoDTO, List<SalesPaymentRegisterDTO> detailsDTO, long userId)
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


            if (infoDTO.Status == "Approved")
            {
                paymentMoneyRecipt = getmoneyreciptbyid(infoDTO.PaymentMoneyReciptId);
                paymentMoneyRecipt.Status = "Approved";
                _paymentMoneyReciptRepository.Update(paymentMoneyRecipt);


                if (_paymentMoneyReciptRepository.Save())
                {
                    List<SalesPaymentRegister> details = new List<SalesPaymentRegister>();
                    SalesPaymentRegister details1 = new SalesPaymentRegister();

                    foreach (var item in detailsDTO)
                    {
                        details1 = getSalesPaymentRegisterbyid(item.PaymentRegisterID);
                        details1.Status = "Approved";
                        details.Add(details1);

                    }
                    _salesPaymentRegisterRepository.UpdateAll(details);

                    if (_salesPaymentRegisterRepository.Save())
                    {
                        List<AgroProductSalesInfo> agroProductSalesInfos= new List<AgroProductSalesInfo>();
                        AgroProductSalesInfo agroProductSalesInfo = new AgroProductSalesInfo();

                        foreach(var item in detailsDTO)
                        {
                            agroProductSalesInfo = getinvoicebyid(item.ProductSalesInfoId);
                            agroProductSalesInfo.PaidAmount += item.PaymentAmount;
                            agroProductSalesInfo.DueAmount -= item.PaymentAmount;
                            agroProductSalesInfos.Add(agroProductSalesInfo);
                        }
                        _agroProductSalesInfoRepository.UpdateAll(agroProductSalesInfos);
                    }

                }
           
                isSucccess = _agroProductSalesInfoRepository.Save();

            }
            else
            {
                paymentMoneyRecipt = getmoneyreciptbyid(infoDTO.PaymentMoneyReciptId);
                paymentMoneyRecipt.Status = "Rejected";
                _paymentMoneyReciptRepository.Update(paymentMoneyRecipt);


                if (_paymentMoneyReciptRepository.Save())
                {
                    List<SalesPaymentRegister> details = new List<SalesPaymentRegister>();
                    SalesPaymentRegister details1 = new SalesPaymentRegister();

                    foreach (var item in detailsDTO)
                    {
                        details1 = getSalesPaymentRegisterbyid(item.PaymentRegisterID);
                        details1.Status = "Rejected";
                        details.Add(details1);

                    }
                    _salesPaymentRegisterRepository.UpdateAll(details);



                }

                isSucccess = _salesPaymentRegisterRepository.Save();
            }

            




            return isSucccess;
        }
    }
}

