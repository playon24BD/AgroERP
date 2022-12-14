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
    public class RawMaterialStockDetailBusiness : IRawMaterialStockDetail
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialStockDetailRepository _rawMaterialStockDetailRepository;
        private readonly RawMaterialStockInfoRepository _rawMaterialStockInfoRepositiory;

        //private readonly IRawMaterialStockInfo _rawMaterialStockInfo;


        public RawMaterialStockDetailBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockDetailRepository = new RawMaterialStockDetailRepository(this._agricultureUnitOfWork);
            this._rawMaterialStockInfoRepositiory = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
            //this._rawMaterialStockInfo = rawMaterialStockInfo;
            //this._rawMaterialStockInfoRepositiory = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }

        public RawMaterialStockDetail GetRawMaterialStockById(long RMDetailsId, long orgId)
        {
            return _rawMaterialStockDetailRepository.GetOneByOrg(a => a.RawMaterialStockDetailId == RMDetailsId && a.OrganizationId == orgId);
        }
        public RawMaterialStockDetail GetRawMaterialStockUpdateById(long Id, long orgId)
        {
            return _rawMaterialStockDetailRepository.GetOneByOrg(a => a.RawMaterialStockDetailId == Id && a.OrganizationId == orgId);
        }

        public IEnumerable<RawMaterialStockDetail> GetRawMaterialStockDetailsById(long infoId, long orgId)
        {
            return _rawMaterialStockDetailRepository.GetAll(i => i.OrganizationId == orgId && i.RawMaterialStockId == infoId).ToList();
        }

        public bool SaverawMaterialStockDetail(long OrganizationId, long RawMaterialId, long SupplierId, double Quantity, long UnitId, DateTime? StockDate, DateTime? StockIssueDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, DateTime? ExpireDate, long? UpdateUserId, string Status, long RawMaterialStockId)
        {
            List<RawMaterialStockDetail> RawMaterialStockDetail = new List<RawMaterialStockDetail>();


            RawMaterialStockDetail stockDetails = new RawMaterialStockDetail();

            stockDetails.OrganizationId = OrganizationId;
            stockDetails.RawMaterialId = RawMaterialId;
            stockDetails.RawMaterialSupplierId = SupplierId;
            //  var RawMaterialId = item.RawMaterialId;
            stockDetails.Quantity = Quantity;
            stockDetails.UnitId = UnitId;
            stockDetails.StockDate = StockDate;
            stockDetails.EntryDate = DateTime.Now;
            stockDetails.ExpireDate = ExpireDate;
            stockDetails.EntryUserId = UpdateUserId;
            stockDetails.Status = Status;
            stockDetails.RawMaterialStockId = RawMaterialStockId;
            RawMaterialStockDetail.Add(stockDetails);

            //}

            _rawMaterialStockDetailRepository.InsertAll(RawMaterialStockDetail);
            return _rawMaterialStockDetailRepository.Save();
        }

        public bool updateRawmaterialstockdetails(long id, double UpdateRawMaterialStock, double IssueRawMaterialStockQty, long orgId, long UnitId, DateTime? EntryDate, long? EntryUserId)
        {
            bool IsSuccess = false;
            int b = 0;
            long rowMaterialId = 0;

            var rawmeterialdetailsqty = GetRawMaterialStockDetailsById(id, orgId);

            

            foreach (var itemss in rawmeterialdetailsqty)
            {
                if (itemss.Quantity>= IssueRawMaterialStockQty)
                {

                    rowMaterialId = itemss.RawMaterialId;
                   
                 
                }
                
               
               // _rawMaterialStockDetailRepository.UpdateAll(rawmeterialdetailsqty);
            }

            RawMaterialStockDetail stockDetails = new RawMaterialStockDetail();

            stockDetails.OrganizationId = orgId;
            stockDetails.RawMaterialId = rowMaterialId;
            stockDetails.RawMaterialSupplierId = 0;
            //  var RawMaterialId = item.RawMaterialId;
            stockDetails.Quantity = IssueRawMaterialStockQty;
            stockDetails.UnitId = UnitId;
            stockDetails.StockIssueDate = DateTime.Now;
            stockDetails.EntryDate = EntryDate;
            stockDetails.EntryUserId = EntryUserId;
            stockDetails.Status = "StockOut";
            stockDetails.RawMaterialStockId = id;
            _rawMaterialStockDetailRepository.Insert(stockDetails);



            IsSuccess = _rawMaterialStockDetailRepository.Save();

            //if (rawmeterialdetailsqty != null)
            //{
            //    rawmeterialdetailsqty. = rawmeterialinfoqty;

            //    _rawMaterialStockInfoRepositiory.Update(rawmeterialinfoupdateqty);

            //}
            //IsSuccess = _rawMaterialStockInfoRepository.Save();
            //if (IsSuccess)
            //{
            //    var rawMaterialStockDetailsUpdate = _rawMaterialStockDetail.updateRawmaterialstockdetails(id, UpdateRawMaterialStock, orgId);
            //}
            return IsSuccess;
        }
        public bool updateRawMaterialStockDetails(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> rawMaterialStockDetailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            double rawmeterialinfoqty = 0;
            var rawmeterialinfoupdateqty = GetRecipeById(info.RawMaterialStockId, orgId);
            if (info.RawMaterialStockId != 0)
            {
                foreach (var Items in rawMaterialStockDetailsDTO)
                {
                    rawmeterialinfoqty = Items.Quantity;
                }
                rawmeterialinfoupdateqty.Quantity = rawmeterialinfoqty;
                rawmeterialinfoupdateqty.UpdateUserId = userId;
                rawmeterialinfoupdateqty.UpdateDate = DateTime.Now;
                _rawMaterialStockInfoRepositiory.Update(rawmeterialinfoupdateqty);

            }
            IsSuccess = _rawMaterialStockInfoRepositiory.Save();



            List<RawMaterialStockDetail> rawMaterialStockDetails = new List<RawMaterialStockDetail>();

            RawMaterialStockDetail rawMaterialStockDetail = new RawMaterialStockDetail();

            foreach (var item in rawMaterialStockDetailsDTO)
            {
                rawMaterialStockDetail = GetRawMaterialStockById(item.RawMaterialStockDetailId, orgId);
                rawMaterialStockDetail.Quantity = item.Quantity;
                rawMaterialStockDetails.Add(rawMaterialStockDetail);
            }
            _rawMaterialStockDetailRepository.UpdateAll(rawMaterialStockDetails);
            IsSuccess = _rawMaterialStockDetailRepository.Save();
            return IsSuccess;
        }

        public RawMaterialStockInfo GetRecipeById(long RawMaterialStockId, long orgId)
        {
            return _rawMaterialStockInfoRepositiory.GetOneByOrg(i => i.RawMaterialStockId == RawMaterialStockId && i.OrganizationId == orgId);
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
