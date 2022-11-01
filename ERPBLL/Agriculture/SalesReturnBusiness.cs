using ERPBLL.Agriculture.Interface;
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
