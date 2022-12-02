using ERPBLL.Agriculture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPDAL.AgricultureDAL;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBLL.Common;
using ERPBO.Agriculture.ReportModels;

namespace ERPBLL.Agriculture
{
    public class CommissionOnProductOnSalesBusiness :ICommissionOnProductOnSalesBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductOnSalesBusinessRepository _commissionOnProductOnSalesRepository;
        private readonly ICommisionOnProductSalesDetailsBusiness _commisionOnProductSalesDetailsBusiness;
        private readonly IAgroProductSalesDetailsBusiness _agroProductSalesDetailsBusiness;

        public CommissionOnProductOnSalesBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, ICommisionOnProductSalesDetailsBusiness commisionOnProductSalesDetailsBusiness,IAgroProductSalesDetailsBusiness agroProductSalesDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductOnSalesRepository = new CommissionOnProductOnSalesBusinessRepository(this._agricultureUnitOfWork);
            this._commisionOnProductSalesDetailsBusiness = commisionOnProductSalesDetailsBusiness;
            this._agroProductSalesDetailsBusiness = agroProductSalesDetailsBusiness;
        }

        public CommissionOnProductOnSales GetCommissionOnProductById(long commissionOnProductSalesId, long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetOneByOrg(c => c.CommissionOnProductOnSalesId == commissionOnProductSalesId && c.OrganizationId == orgId);
        }
        private CommissionOnProductOnSales GetCommissionOnSalesBySalesInfoId(long commissionOnProductSalesId, long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetOneByOrg(c => c.ProductSalesInfoId == commissionOnProductSalesId && c.OrganizationId == orgId);
        }

        public IEnumerable<CommissionOnProductOnSales> GetCommissionOnProductOnSales(long orgId)
        {
            return _commissionOnProductOnSalesRepository.GetAll(a => a.OrganizationId == orgId).ToList(); ;
        }

        public IEnumerable<CommissionOnProductOnSalesDTO> GetAllCommissionOnProductOnSales(string invoice,long? stockiestId,string fdate, string tdate, long orgId)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<CommissionOnProductOnSalesDTO>(string.Format(QueryForSalesCommission(invoice,stockiestId, fdate, tdate, orgId)));
        }
        public string QueryForSalesCommission(string invoice, long? stockiestId, string fdate, string tdate, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (stockiestId > 0)
            {
                param += string.Format(@"and si.stockiestId={0}", stockiestId);
            }
            if (invoice != null && invoice != "")
            {
                param += string.Format(@" and cps.InvoiceNo ='{0}'", invoice);
            }

            if (!string.IsNullOrEmpty(fdate) && fdate.Trim() != "" && !string.IsNullOrEmpty(tdate) && tdate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fdate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fdate) && fdate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(tdate) && tdate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date)='{0}'", tDate);
            }


   query = string.Format(@"Select cps.ProductSalesInfoId,cps.CommissionOnProductOnSalesId,si.TotalAmount,cps.InvoiceNo,SUM((cpsd.price* (Case When cpsd.cash=0 Then cpsd.Credit Else cpsd.Cash End)/100 ) ) As TotalCommission,cpsd.PaymentMode,Cast (cps.EntryDate as date) As EntryDate,StockiestName
            from tblCommissionOnProductSales cps
            Inner join tblCommisionOnProductSalesDetails cpsd
            on cps.CommissionOnProductOnSalesId=cpsd.CommissionOnProductOnSalesId
            Inner join tblFinishGoodProductInfo p
            on cpsd.FinishGoodProductId=p.FinishGoodProductId
            Inner join tblProductSalesInfo si
            on si.ProductSalesInfoId=cps.ProductSalesInfoId
            Inner join tblStockiestInfo f
            on si.StockiestId=f.StockiestId
            where 1=1
     Group by cps.ProductSalesInfoId,cps.CommissionOnProductOnSalesId,cps.InvoiceNo,cpsd.PaymentMode,Cast (cps.EntryDate as date),StockiestName,si.TotalAmount
	 Order by Cast (cps.EntryDate as date) desc ,cps.CommissionOnProductOnSalesId DESC",
           Utility.ParamChecker(param));

            return query;
        }

        public bool SaveCommissionOnProductOnSales(AgroProductSalesInfo agroProductSalesInfo, long userId, long orgId)
        {
            bool isSuccess = false;
            CommissionOnProductOnSalesDTO commissionOnProductOnSalesDTO = new CommissionOnProductOnSalesDTO();
            CommissionOnProductOnSales commissionOnProductOnSales = new CommissionOnProductOnSales();
            if (commissionOnProductOnSalesDTO.CommissionOnProductOnSalesId == 0)
            {


                commissionOnProductOnSales.ProductSalesInfoId = agroProductSalesInfo.ProductSalesInfoId;
                commissionOnProductOnSales.InvoiceNo = agroProductSalesInfo.InvoiceNo;
                commissionOnProductOnSales.PaymentMode = agroProductSalesInfo.PaymentMode;
                commissionOnProductOnSales.Status = "Insert";

                commissionOnProductOnSales.EntryDate = DateTime.Now;
                commissionOnProductOnSales.OrganizationId = orgId;
                commissionOnProductOnSales.EntryUserId = userId;


                _commissionOnProductOnSalesRepository.Insert(commissionOnProductOnSales);
            }
            else
            {
                var commissionOnProductSales = this.GetCommissionOnSalesBySalesInfoId(commissionOnProductOnSalesDTO.CommissionOnProductOnSalesId, orgId);
                commissionOnProductSales.Status = commissionOnProductOnSalesDTO.Status;
                commissionOnProductSales.PaymentMode = commissionOnProductOnSalesDTO.PaymentMode;
                commissionOnProductSales.UpdateDate = DateTime.Now;
                commissionOnProductSales.UpdateUserId = userId;
                _commissionOnProductOnSalesRepository.Update(commissionOnProductSales);

            }

            isSuccess = _commissionOnProductOnSalesRepository.Save();
            if (isSuccess)
            {
                _commisionOnProductSalesDetailsBusiness.SaveCommisionOnProductSalesDetails(agroProductSalesInfo.AgroProductSalesDetails.ToList(), commissionOnProductOnSales.CommissionOnProductOnSalesId, agroProductSalesInfo.PaymentMode, userId, orgId);

            }
            return isSuccess;

        }


        public bool UpdateCommissionOnProductOnSales(AgroProductSalesInfo agroProductSalesInfo, long userId, long orgId)
        {
            bool isSuccess = false;


            var commissionOnProductSales = this.GetCommissionOnSalesBySalesInfoId(agroProductSalesInfo.ProductSalesInfoId, orgId);
            commissionOnProductSales.Status = "Update";
            commissionOnProductSales.PaymentMode = "Cash";
            commissionOnProductSales.UpdateDate = DateTime.Now;
            commissionOnProductSales.UpdateUserId = userId;
            _commissionOnProductOnSalesRepository.Update(commissionOnProductSales);

            isSuccess = _commissionOnProductOnSalesRepository.Save();
            if (isSuccess)
            {
              var productSalesDetails=  _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(agroProductSalesInfo.ProductSalesInfoId,orgId).ToList();



                _commisionOnProductSalesDetailsBusiness.UpdateCommisionOnProductSalesDetails(productSalesDetails, commissionOnProductSales.CommissionOnProductOnSalesId, agroProductSalesInfo.PaymentMode, userId, orgId);

            }
            return isSuccess;

        }

        public IEnumerable<SalesCommissionData> GetSalesCommissionDataReport(string invoiceNo, long? stockiestId, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<SalesCommissionData>(string.Format(QueryForSalesCommissionReport(invoiceNo, stockiestId, fromDate, toDate)));
        }
        public string QueryForSalesCommissionReport(string invoiceNo, long? stockiestId, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (stockiestId > 0)
            {
                param += string.Format(@"and si.StockiestId={0}", stockiestId);
            }
            if (invoiceNo != null && invoiceNo != "")
            {
                param += string.Format(@" and cps.InvoiceNo ='{0}'", invoiceNo);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(cps.EntryDate as date)='{0}'", tDate);
            }


            query = string.Format(@"Select DISTINCT toDate='" + fromDate + "', fromDate='" + toDate +"',cps.ProductSalesInfoId,cps.CommissionOnProductOnSalesId,cps.InvoiceNo,SUM(cpsd.TotalCommission) As TotalCommission,cpsd.PaymentMode,Cast (cps.EntryDate as date) As EntryDate,StockiestName from tblCommissionOnProductSales cps Inner join tblCommisionOnProductSalesDetails cpsd on cps.CommissionOnProductOnSalesId=cpsd.CommissionOnProductOnSalesId Inner join tblFinishGoodProductInfo p on cpsd.FinishGoodProductId=p.FinishGoodProductId Inner join tblProductSalesInfo si on si.ProductSalesInfoId=cps.ProductSalesInfoId Inner join tblStockiestInfo f on si.StockiestId=f.StockiestId  where 1=1 {0} Group by cps.ProductSalesInfoId,cps.CommissionOnProductOnSalesId,cps.InvoiceNo,cpsd.PaymentMode,Cast (cps.EntryDate as date),StockiestName",
                    Utility.ParamChecker(param));

            return query;
        }

        //public IEnumerable<CommissionOnProductOnSalesDTO> GetSalesCommissionListInfos(string invoiceNo, long? stockiestId, string fromDate, string toDate)
        //{
        //    return _agricultureUnitOfWork.Db.Database.SqlQuery<CommissionOnProductOnSalesDTO>(string.Format(QueryForSalesCommissionList(invoiceNo, stockiestId, fromDate, toDate)));
        //}
    }
}
