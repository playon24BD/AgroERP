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
    public class RawMaterialStockDetailBusiness:IRawMaterialStockDetail
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialStockDetailRepository _rawMaterialStockDetailRepository;
        //private readonly RawMaterialStockInfoRepository _rawMaterialStockInfoRepositiory;

        //private readonly IRawMaterialStockInfo _rawMaterialStockInfo;


        public RawMaterialStockDetailBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockDetailRepository = new RawMaterialStockDetailRepository(this._agricultureUnitOfWork);
            //this._rawMaterialStockInfo = rawMaterialStockInfo;
            //this._rawMaterialStockInfoRepositiory = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialStockDetail> GetRawMaterialStockDetailsById(long infoId, long orgId)
        {
            return _rawMaterialStockDetailRepository.GetAll(i => i.OrganizationId == orgId && i.RawMaterialStockId == infoId).ToList();
        }

        public bool SaverawMaterialStockDetail(long OrganizationId, long RawMaterialId, int Quantity, string Unit, DateTime? StockDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, long? UpdateUserId, string Status, long RawMaterialStockId)
        {
            List<RawMaterialStockDetail> RawMaterialStockDetail = new List<RawMaterialStockDetail>();

        
                RawMaterialStockDetail stockDetails = new RawMaterialStockDetail();

                stockDetails.OrganizationId = OrganizationId;
                stockDetails.RawMaterialId = RawMaterialId;
                //  var RawMaterialId = item.RawMaterialId;
                stockDetails.Quantity = Quantity;
                stockDetails.Unit = Unit;
                stockDetails.StockDate = StockDate;
                stockDetails.UpdateDate = DateTime.Now;
                stockDetails.UpdateUserId = UpdateUserId;
                stockDetails.Status = Status;
                stockDetails.RawMaterialStockId = RawMaterialStockId;
                RawMaterialStockDetail.Add(stockDetails);

            //}

            _rawMaterialStockDetailRepository.InsertAll(RawMaterialStockDetail);
            return _rawMaterialStockDetailRepository.Save();
        }

        //public bool SaverawMaterialStockDetail(List<RawMaterialStockDetail> details, string BatchCodes, long userId, long orgId)
        //{
        //    List<RawMaterialStockDetail> RawMaterialStockDetail = new List<RawMaterialStockDetail>();
        //    RawMaterialStockDetail stockDetails = new RawMaterialStockDetail();
        //    foreach (var item in details)
        //    {


        //        stockDetails.OrganizationId = item.OrganizationId;
        //        stockDetails.RawMaterialId = item.RawMaterialId;
        //        var RawMaterialId = item.RawMaterialId;
        //        stockDetails.Quantity = item.Quantity;
        //        stockDetails.Unit = item.Unit;
        //        stockDetails.StockDate = item.StockDate;
        //        stockDetails.UpdateDate = DateTime.Now;
        //        stockDetails.UpdateUserId = item.UpdateUserId;
        //        stockDetails.Status = item.Status;
        //        var Getid = _rawMaterialStockInfo.RawMaterialStockInfoIdGet(BatchCodes, item.RawMaterialId);

        //        RawMaterialStockDetail.Add(stockDetails);

        //    }

        //    _rawMaterialStockDetailRepository.InsertAll(RawMaterialStockDetail);
        //    return _rawMaterialStockDetailRepository.Save();
        //}

        //public bool SaverawMaterialStockDetail(List<RawMaterialStockDetail> details, List<RawMaterialStockInfo> RawMaterialStockInfoid, long userId, long orgId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
