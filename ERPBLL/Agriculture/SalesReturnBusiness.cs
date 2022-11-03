using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
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


        public SalesReturnBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesReturnRepository = new SalesReturnRepository(this._agricultureUnitOfWork);
           
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
            query = string.Format(@"	select  sr.InvoiceNo,sr.ReturnQuanity,sr.ReturnPerUnitPrice,sr.ReturnTotalPrice,sr.Status,sr.FinishGoodProductInfoId,fpi.FinishGoodProductName,sr.MeasurementId,m.MeasurementName,sr.MeasurementSize,sr.AdjustmentDate,CONVERT(date,sr.ReturnDate) AS ReturnDate FROM  
tblSalesReturn sr 
INNER JOIN tblProductSalesInfo si on sr.ProductSalesInfoId = si.ProductSalesInfoId
inner join tblFinishGoodProductInfo fpi on sr.FinishGoodProductInfoId = fpi.FinishGoodProductId
inner join tblMeasurement m on sr.MeasurementId = m.MeasurementId

Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }


        public bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId)
        {
            bool IsSuccess = false;
            List<SalesReturn> salesReturns = new List<SalesReturn>();
            foreach (var item in detailsDTO)
            {
                if (item.ReturnPerUnitPrice > 0 && item.ReturnQuanity > 0)
                {
                    var Returncode = "RC-" + DateTime.Now.ToString("ss") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("yy");

                    SalesReturn salesReturn = new SalesReturn()
                    {
                        ReturnCode = Returncode,
                        InvoiceNo = item.InvoiceNo,
                        MeasurementSize = item.MeasurementSize,
                        ReturnQuanity = item.ReturnQuanity,
                        ReturnPerUnitPrice = item.ReturnPerUnitPrice,
                        Status = "NOTADJUST",
                        FinishGoodProductInfoId = item.FinishGoodProductInfoId,
                        MeasurementId = item.MeasurementId,
                        ProductSalesInfoId = item.ProductSalesInfoId,
                        ReturnDate = DateTime.Now,
                        EntryUserId = userId,
                        ReturnTotalPrice = (item.ReturnQuanity * item.ReturnPerUnitPrice)


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
    }
}
