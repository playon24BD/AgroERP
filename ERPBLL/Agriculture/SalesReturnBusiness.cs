using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class SalesReturnBusiness : ISalesReturn
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly SalesReturnRepository _salesReturnRepository;
        private readonly IMeasuremenBusiness _measuremenBusiness;



        public SalesReturnBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IMeasuremenBusiness measuremenBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesReturnRepository = new SalesReturnRepository(this._agricultureUnitOfWork);
            this._measuremenBusiness = measuremenBusiness;
           
        }

        public IEnumerable<SalesReturnDTO> GetSalesReturns(long? ProductId, string name,string status)
        {
           // return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(orgId, ProductId)).ToList();
           return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnDTO>(QueryForSalesReturn(ProductId,name,status)).ToList();
        }
        private string QueryForSalesReturn(long? ProductId, string name,string status)
        {
            string query = string.Empty;
            string param = string.Empty;

         
            if (ProductId != null && ProductId > 0)
            {
                param += string.Format(@" and sr.FinishGoodProductInfoId like '%{0}%'", ProductId);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and sr.InvoiceNo like '%{0}%'", name);
            }

            if (status != null && status != "")
            {
                param += string.Format(@" and sr.Status= '{0}'", status);
            }
            //query = string.Format(@"	select  sr.InvoiceNo,sr.ReturnQuanity,sr.ReturnPerUnitPrice,sr.ReturnTotalPrice,sr.Status,sr.FinishGoodProductInfoId,fpi.FinishGoodProductName,sr.MeasurementId,m.MeasurementName,sr.MeasurementSize,sr.AdjustmentDate,CONVERT(date,sr.ReturnDate) AS ReturnDate FROM  

            query = string.Format(@"select sr.BoxQuanity, sr.QtyKG,sr.InvoiceNo,sr.ReturnQuanity,sr.ReturnPerUnitPrice,sr.ReturnTotalPrice,sr.Status,sr.FinishGoodProductInfoId,fpi.FinishGoodProductName,sr.MeasurementId,m.MeasurementName,sr.MeasurementSize,sr.AdjustmentDate,sr.ReturnDate FROM tblSalesReturn sr INNER JOIN tblProductSalesInfo si on sr.ProductSalesInfoId = si.ProductSalesInfoId inner join tblFinishGoodProductInfo fpi on sr.FinishGoodProductInfoId = fpi.FinishGoodProductId inner join tblMeasurement m on sr.MeasurementId = m.MeasurementId
Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }


        public bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId , long orgid)
        {
            bool IsSuccess = false;
            List<SalesReturn> salesReturns = new List<SalesReturn>();
            foreach (var item in detailsDTO)
            {
                double TotalreturnproductQty = 0;
               
                double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId,orgid).MasterCarton;
                double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgid).InnerBox;
                double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgid).PackSize;


                if (MasterCartonMasurement != 0)
                {
                    TotalreturnproductQty = MasterCartonMasurement * InnerBoxMasurement * item.ReturnQuanity ;

                }
                else
                {
                    TotalreturnproductQty = InnerBoxMasurement * item.ReturnQuanity;
                }
                //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;

                if (item.ReturnPerUnitPrice > 0 && item.ReturnQuanity > 0)
                {


                    var Returncode = "RC-" + DateTime.Now.ToString("ss") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("yy");

                    SalesReturn salesReturn = new SalesReturn()
                    {
                        ReturnCode = Returncode,
                        InvoiceNo = item.InvoiceNo,
                        MeasurementSize = item.MeasurementSize,
                        MeasurementId = item.MeasurementId,

                        ReturnQuanity = TotalreturnproductQty,
                        BoxQuanity = item.ReturnQuanity,
                        ReturnPerUnitPrice = item.ReturnPerUnitPrice,
                        Status = "NOTADJUST",
                        FinishGoodProductInfoId = item.FinishGoodProductInfoId,
                       
                        ProductSalesInfoId = item.ProductSalesInfoId,
                        ReturnDate = DateTime.Now,
                        EntryUserId = userId,
                        ReturnTotalPrice = (TotalreturnproductQty * item.ReturnPerUnitPrice),
                        FGRId = item.FGRId,
                        QtyKG = item.QtyKG,
                        StockiestId = item.StockiestId,


                    };
                    _salesReturnRepository.Insert(salesReturn);
                }
            }
            IsSuccess = _salesReturnRepository.Save();
            return IsSuccess;
        }





        public IEnumerable<SalesReturn> GetSalesSalesReturnByInfoIdNotAdjust(long? ProductSalesInfoId)
        {
            return _salesReturnRepository.GetAll(i => i.ProductSalesInfoId == ProductSalesInfoId && i.Status == "NOTADJUST").ToList();
        }

        public bool updateadjustsales(List<SalesReturnDTO> salesReturnDTOs)
        {
            bool IsSuccess = false;

            List<SalesReturn> salesReturns = new List<SalesReturn>();
            SalesReturn salesReturn = new SalesReturn();
            foreach(var item in salesReturnDTOs)
            {
                if (item.ISActive == true)
                {
                    salesReturn = GetSalesReturnsById(item.SalesReturnId);
                    salesReturn.Status = "ADJUST";
                    salesReturn.AdjustmentDate = DateTime.Now;

                }
            }
            _salesReturnRepository.UpdateAll(salesReturns);
            IsSuccess = _salesReturnRepository.Save();

            return IsSuccess;
        }

        public SalesReturn GetSalesReturnsById(long SalesReturnId)
        {
            return _salesReturnRepository.GetOneByOrg(a => a.SalesReturnId == SalesReturnId);
        }


        public IEnumerable<SalesReturn> GetSalesReturnsAdjustById(long id, long orgId)
        {
            //return _salesReturnRepository.GetOneByOrg(f => f.SalesReturnId == id);
            return _salesReturnRepository.GetAll(i => i.ProductSalesInfoId == id).ToList();
        }

        public bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReturn> GetAgroSalesreturnByStokiestId(long StockiestId, string status)
        {
            return _salesReturnRepository.GetAll(a => a.StockiestId == StockiestId && a.Status== status);

        }

        public IEnumerable<SalesReturn> GetAgroSalesreturnByFGRandproductId(long FGRId, long FinishGoodProductInfoId)
        {
            return _salesReturnRepository.GetAll(v => v.FGRId == FGRId && v.FinishGoodProductInfoId == FinishGoodProductInfoId && v.Status == "ADJUST");
        }

        public IEnumerable<SalesReturnDTO> GetSalesReturnReportList( long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnDTO>(QueryForSalesReturnReportList(productId,stockiestId,invoiceNo, status, fromDate, toDate)).ToList();
        }

        private string QueryForSalesReturnReportList(long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (productId != null && productId > 0)
            {
                param += string.Format(@" and f.FinishGoodProductId ={0}", productId);
            }

            else if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and s.StockiestId ={0}", stockiestId);
            }

            else if (invoiceNo != null && invoiceNo != "")
            {
                param += string.Format(@" and sr.InvoiceNo like '%{0}%'", invoiceNo);
            }

            else if (status != null && status != "")
            {
                param += string.Format(@" and sr.Status= '{0}'", status);
            }

             if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date)='{0}'", tDate);
            }
            query = string.Format(@"

            

select convert (date,sr.ReturnDate) as ReturnDate,s.StockiestId,f.FinishGoodProductId,InvoiceNo,f.FinishGoodProductName,s.StockiestName,sr.MeasurementSize,sr.ReturnQuanity,sr.QtyKG,sr.BoxQuanity,sr.ReturnPerUnitPrice,sr.Status,sr.ReturnTotalPrice

from [Agriculture].[dbo]. tblSalesReturn sr
inner join [Agriculture].[dbo]. tblFinishGoodProductInfo f on sr.FinishGoodProductInfoId=f.FinishGoodProductId
inner join [Agriculture].[dbo]. tblStockiestInfo s on sr.StockiestId=s.StockiestId


Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<SalesReturnReportData> GetSalesReturnReportData(long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnReportData>(QueryForSalesReturnReportData(productId,stockiestId,invoiceNo, status, fromDate, toDate)).ToList();
        }

        private string QueryForSalesReturnReportData(long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (productId != null && productId > 0)
            {
                param += string.Format(@" and f.FinishGoodProductId ={0}", productId);
            }
            else if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and s.StockiestId ={0}", stockiestId);
            }

            else if (invoiceNo != null && invoiceNo != "")
            {
                param += string.Format(@" and sr.InvoiceNo like '%{0}%'", invoiceNo);
            }

            else if (status != null && status != "")
            {
                param += string.Format(@" and sr.Status= '{0}'", status);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sr.ReturnDate as date)='{0}'", tDate);
            }
            query = string.Format(@"

            

select convert (date,sr.ReturnDate) as ReturnDate,s.StockiestId,f.FinishGoodProductId,InvoiceNo,f.FinishGoodProductName,s.StockiestName,sr.MeasurementSize,sr.ReturnQuanity,sr.QtyKG,sr.BoxQuanity,sr.ReturnPerUnitPrice,sr.Status,sr.ReturnTotalPrice

from [Agriculture].[dbo]. tblSalesReturn sr
inner join [Agriculture].[dbo]. tblFinishGoodProductInfo f on sr.FinishGoodProductInfoId=f.FinishGoodProductId
inner join [Agriculture].[dbo]. tblStockiestInfo s on sr.StockiestId=s.StockiestId


Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<SalesReturnReportData> GetSalesReturnReportSave(string InvoiceNo)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnReportData>(QueryForSalesReturnReportDataSave(InvoiceNo)).ToList();
        }

        private string QueryForSalesReturnReportDataSave(string InvoiceNo)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(InvoiceNo))
            {
                param += string.Format(@"and sr.InvoiceNo ='{0}'", InvoiceNo);
            }

            query = string.Format(@"

select convert (date,sr.ReturnDate) as ReturnDate,sr.InvoiceNo,f.FinishGoodProductName,s.StockiestName,sr.MeasurementSize,sr.ReturnQuanity,sr.QtyKG,sr.BoxQuanity,sr.ReturnPerUnitPrice,sr.Status,sr.ReturnTotalPrice

from [Agriculture].[dbo]. tblSalesReturn sr
inner join [Agriculture].[dbo]. tblFinishGoodProductInfo f on sr.FinishGoodProductInfoId=f.FinishGoodProductId
inner join [Agriculture].[dbo]. tblStockiestInfo s on sr.StockiestId=s.StockiestId



Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

    }
}
