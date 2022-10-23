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
    public class MRawMaterialIssueStockInfoBusisness : IMRawMaterialIssueStockInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly MRawMaterialIssueStockInfoRepository _mRawMaterialIssueStockInfoRepository;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;


        //contractor
        public MRawMaterialIssueStockInfoBusisness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._mRawMaterialIssueStockInfoRepository = new MRawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
        }

        public bool SaveRawMaterialIssueStock(MRawMaterialIssueStockInfoDTO info, List<MRawMaterialIssueStockDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
            var BatchCode = "BPIS-" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");


            if (info.RawMaterialIssueStockId == 0)
            {
                MRawMaterialIssueStockInfo model = new MRawMaterialIssueStockInfo
                {
                    ProductBatchCode = BatchCode,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId =orgId,
                    Status = "Active",

                };
                List<MRawMaterialIssueStockDetails> modelDetails = new List<MRawMaterialIssueStockDetails>();
                List<RawMaterialTrack> modeltrk = new List<RawMaterialTrack>();//RawMaterialTrackInfo table update

                foreach (var item in details)
                {
                    MRawMaterialIssueStockDetails materialIssueStockDetails = new MRawMaterialIssueStockDetails()
                    {

                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        UnitID = item.UnitID,
                        IssueStatus = "StockOut",
                        EntryDate = DateTime.Now,

                    };
                    modelDetails.Add(materialIssueStockDetails);


                    //RawMaterialTrackInfo table update
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack()
                    {
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        IssueDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        IssueStatus = "StockOut",
                        EntryUserId = userId
                    };
                    modeltrk.Add(rawMaterialTrack);

                }

                model.MRawMaterialIssueStockDetails = modelDetails;

                _mRawMaterialIssueStockInfoRepository.Insert(model);
                IsSuccess = _mRawMaterialIssueStockInfoRepository.Save();

                _rawMaterialTrackInfoRepository.InsertAll(modeltrk);
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

            }
            //else
            //{

            //    IsSuccess = _fDetail.updateFinishGoodRecipDetails(info, details, userId, orgId);

            //}
            return IsSuccess;
        }
    }
}
