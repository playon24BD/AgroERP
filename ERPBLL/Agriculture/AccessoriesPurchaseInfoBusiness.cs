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
    public class AccessoriesPurchaseInfoBusiness : IAccessoriesPurchaseInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AccessoriesInfoRepository _accessoriesInfoRepository;
        private readonly AccessoriesPurchaseDetailsRepository _accessoriesPurchaseDetailsRepository;
        private readonly AccessoriesTrackInfoRepository _accessoriesTrackInfoRepository;
        private readonly AccessoriesPurchaseInfoRepository _accessoriesPurchaseInfoRepository;


        public AccessoriesPurchaseInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._accessoriesInfoRepository = new AccessoriesInfoRepository(this._agricultureUnitOfWork);
            this._accessoriesPurchaseInfoRepository = new AccessoriesPurchaseInfoRepository(this._agricultureUnitOfWork);
            this._accessoriesTrackInfoRepository = new AccessoriesTrackInfoRepository(this._agricultureUnitOfWork);
            this._accessoriesPurchaseDetailsRepository = new AccessoriesPurchaseDetailsRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<AccessoriesPurchaseInfoDTO> GetAccessoriesStockList(string invoiceNo)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<AccessoriesPurchaseInfoDTO>(QueryForAccessoriesStockList(invoiceNo));
        }

        private string QueryForAccessoriesStockList(string invoiceNo)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    param += string.Format(@"and a.InvoiceNo like '%{0}%'", invoiceNo);
                }

                
                query = string.Format(@"

select a.AccessoriesPurchaseInfoId, a.InvoiceNo,a.InvoiceDate,a.TotalAmount,info.RawMaterialSupplierName from tblAccessoriesPurchaseInfo a
inner join tblRawMaterialSupplierInfo info on a.RawMaterialSupplierId=info.RawMaterialSupplierId 
where 1=1 {0} order by a.AccessoriesPurchaseInfoId desc

", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public bool SaveAccessoriesPurchaseStock(AccessoriesPurchaseInfoDTO info, List<AccessoriesPurchaseDetailsDTO> details, long userId)
        {
            bool IsSuccess = false;
            var Pinumber = "BPI-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");


            if (info.AccessoriesPurchaseInfoId == 0)
            {
                AccessoriesPurchaseInfo model = new AccessoriesPurchaseInfo
                {
                    AccessoriesPurchaseInfoId= info.AccessoriesPurchaseInfoId,
                    BatchCode = Pinumber,

                    InvoiceNo = info.InvoiceNo,
                    InvoiceDate = info.InvoiceDate,
                    RawMaterialSupplierId = info.RawMaterialSupplierId,
                    EntryUserId = userId,
                    EntryDate = DateTime.Now,
                    TotalAmount = info.TotalAmount

                };
                _accessoriesPurchaseInfoRepository.Insert(model);
                IsSuccess = _accessoriesInfoRepository.Save();

                List<AccessoriesPurchaseDetails> modelDetails = new List<AccessoriesPurchaseDetails>();
                List<AccessoriesTrackInfo> modeltrk = new List<AccessoriesTrackInfo>();

                foreach (var item in details)
                {
                    AccessoriesPurchaseDetails accessoriesPurchaseDetails = new AccessoriesPurchaseDetails()
                    {

 
                        Quantity = item.Quantity,

                        UnitPrice = item.UnitPrice,
                        SubTotal = item.SubTotal,
                        AccessoriesId = item.AccessoriesId,
                        Status = "StockIn",
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        AccessoriesPurchaseInfoId = model.AccessoriesPurchaseInfoId


                    };
                    modelDetails.Add(accessoriesPurchaseDetails);


                    //RawMaterialTrackInfo table update
                    AccessoriesTrackInfo accessoriesTrackInfo = new AccessoriesTrackInfo()
                    {
              
                        Quantity = item.Quantity,
                        AccessoriesId = item.AccessoriesId,
                        EntryDate = DateTime.Now,
                        IssueStatus = "StockIn",
                        EntryUserId = userId,
                        AccessoriesPurchaseInfoId= model.AccessoriesPurchaseInfoId
                    };
                    modeltrk.Add(accessoriesTrackInfo);

                }


                _accessoriesPurchaseDetailsRepository.InsertAll(modelDetails);
                IsSuccess = _accessoriesPurchaseDetailsRepository.Save();

                _accessoriesTrackInfoRepository.InsertAll(modeltrk);
                IsSuccess = _accessoriesTrackInfoRepository.Save();



            }

            return IsSuccess;
        }
    }
}
