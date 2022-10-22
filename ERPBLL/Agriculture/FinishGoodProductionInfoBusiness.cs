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
    public class FinishGoodProductionInfoBusiness :IFinishGoodProductionInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly FinishGoodProductionInfoRepository _finishGoodProductionInfoRepository;
        private readonly IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness;
        private readonly IRawMaterialIssueStockInfoBusiness _rawMaterialIssueStockInfoBusiness;
        private readonly IRawMaterialIssueStockDetailsBusiness _rawMaterialIssueStockDetailsBusiness;
        public FinishGoodProductionInfoBusiness (IAgricultureUnitOfWork agricultureUnitOfWork,IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IRawMaterialIssueStockInfoBusiness rawMaterialIssueStockInfoBusiness, IRawMaterialIssueStockDetailsBusiness rawMaterialIssueStockDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionInfoRepository = new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this.finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
        }

        public FinishGoodProductionInfo GetCheckFinishGoodQuantity(long FinishGoodProductInfoId, long orgId)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.FinishGoodProductInfoId == FinishGoodProductInfoId);
        }

        public FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FinishGoodProductionInfo> GetFinishGoodProductionInfo(long orgId)
        {
            return _finishGoodProductionInfoRepository.GetAll(a=>a.OrganizationId==orgId);
        }

        public FinishGoodProductionInfo GetProductionInfoById(long id, long orgId)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(f => f.FinishGoodProductInfoId == id);
        }

        public bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;

            var finishGoodProductionBatch = "Pro-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            if (finishGoodProductionInfoDTO.FinishGoodProductionInfoId==0)
            {
                FinishGoodProductionInfo finishGoodProductionInfo = new FinishGoodProductionInfo
                {
                    FinishGoodProductId = finishGoodProductionInfoDTO.FinishGoodProductId,
                    ReceipeBatchCode=finishGoodProductionInfoDTO.ReceipeBatchCode,
                    FinishGoodProductionBatch= finishGoodProductionBatch,
                    Quanity = finishGoodProductionInfoDTO.Quanity,
                    TargetQuantity = finishGoodProductionInfoDTO.TargetQuantity,
                    Remarks = finishGoodProductionInfoDTO.Remarks,
                    Status = finishGoodProductionInfoDTO.Status,
                    EntryDate = DateTime.Now,
                    EntryUserId=userId,
                    OrganizationId=orgId

                };
                _finishGoodProductionInfoRepository.Insert(finishGoodProductionInfo);

            }
         isSuccess= _finishGoodProductionInfoRepository.Save();
            if (isSuccess)
            {

                isSuccess = finishGoodProductionDetailsBusiness.SaveFinishGoodDetails(details, finishGoodProductionBatch, userId,orgId);

            }
            if (isSuccess)
            {
                //AbNormal
                if (details.Count() > 0)
                {

                    List<RawMaterialIssueStockDetailsDTO> rawMaterialIssueStockDetailsDTOList = new List<RawMaterialIssueStockDetailsDTO>();
                    List<RawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfoList = new List<RawMaterialIssueStockInfoDTO>();

                    foreach (var item in details)
                    {
                        //Test!

                        var rawMaterialStockInfoId = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockByMeterialId(item.RawMaterialId, orgId);

                        List<RawMaterialIssueStockDetailsDTO> issuedetails = new List<RawMaterialIssueStockDetailsDTO> { new RawMaterialIssueStockDetailsDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId ,RawMaterialId=item.RawMaterialId,OrganizationId=orgId,EntryUserId=userId,Quantity=item.RequiredQuantity,UnitId= rawMaterialStockInfoId.UnitId} };
                        rawMaterialIssueStockDetailsDTOList.AddRange(issuedetails);

                        List<RawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfos = new List<RawMaterialIssueStockInfoDTO>() { new RawMaterialIssueStockInfoDTO { RawMaterialIssueStockId= rawMaterialStockInfoId.RawMaterialIssueStockId,RawMaterialId=item.RawMaterialId,Quantity=item.RequiredQuantity,UpdateUserId=userId,OrganizationId=orgId } };
                        rawMaterialIssueStockInfoList.AddRange(rawMaterialIssueStockInfos); 

                    }


                    isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveRawMaterialIssueDetails(rawMaterialIssueStockDetailsDTOList, userId, orgId);

                    if (isSuccess)
                    {

                        isSuccess = _rawMaterialIssueStockInfoBusiness.UpdateProductIssueRawMaterialStock(rawMaterialIssueStockInfoList);

                    }

                }
            }

            return isSuccess;
        }
    }
}
