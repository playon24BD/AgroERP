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

        public IEnumerable<SalesReturnDTO> GetSalesReturns(long? ProductId, string name)
        {
           // return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(orgId, ProductId)).ToList();
           return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnDTO>(QueryForSalesReturn(ProductId,name)).ToList();
        }
        private string QueryForSalesReturn(long? ProductId, string name)
        {
            string query = string.Empty;
            string param = string.Empty;

         
            if (ProductId != null && ProductId > 0)
            {
                param += string.Format(@" and sr.FinishGoodProductInfoId", ProductId);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and sr.InvoiceNo like '%{0}%'", name);
            }
            query = string.Format(@"	select  sr.InvoiceNo,sr.ReturnQuanity,sr.ReturnPerUnitPrice,sr.ReturnTotalPrice,sr.Status,sr.FinishGoodProductInfoId,fpi.FinishGoodProductName,sr.MeasurementId,m.MeasurementName,sr.MeasurementSize,sr.AdjustmentDate,sr.ReturnDate FROM  
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
                        EntryUserId = userId


                    };
                    _salesReturnRepository.Insert(salesReturn);
                }
            }
            IsSuccess = _salesReturnRepository.Save();
            return IsSuccess;
        }
    }
}
