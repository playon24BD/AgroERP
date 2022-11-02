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
    public class FinishGoodProductionInfoBusiness : IFinishGoodProductionInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly FinishGoodProductionInfoRepository _finishGoodProductionInfoRepository;
        private readonly IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness;
        private readonly IMRawMaterialIssueStockInfo _rawMaterialIssueStockInfoBusiness;
        private readonly IMRawMaterialIssueStockDetails _rawMaterialIssueStockDetailsBusiness;
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        public FinishGoodProductionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IMRawMaterialIssueStockInfo rawMaterialIssueStockInfoBusiness, IMRawMaterialIssueStockDetails rawMaterialIssueStockDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionInfoRepository = new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this.finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId, long orgId)
        {
            //return _finishGoodProductionInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.FinishGoodProductId == FinishGoodProductInfoId);

            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductCheckQty(FinishGoodProductInfoId, orgId)).ToList();
        }

        private string QueryForFinishGoodProductCheckQty(long finishGoodProductInfoId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and FI.OrganizationId={0}", orgId);
            if (finishGoodProductInfoId>0)
            {
                param += string.Format(@" and FI.FinishGoodProductId={0}", finishGoodProductInfoId);
            }

            query = string.Format(@"SELECT DISTINCT FI.FinishGoodProductId,FP.FinishGoodProductName,FI.ReceipeBatchCode,SUM(FI.TargetQuantity) AS TargetQuantity

FROM FinishGoodProductionInfoes FI
INNER JOIN tblFinishGoodProductInfo FP on FI.FinishGoodProductId=FP.FinishGoodProductId
INNER JOIN tblFinishGoodRecipeInfo RI on FI.ReceipeBatchCode=RI.ReceipeBatchCode	
Where 1=1 and FI.ReceipeBatchCode=RI.ReceipeBatchCode {0} Group by FP.FinishGoodProductName,FI.ReceipeBatchCode,FI.FinishGoodProductId", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfos(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductInfoss(orgId)).ToList();
        }

        private string QueryForFinishGoodProductInfoss(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and FI.OrganizationId={0}", orgId);


            query = string.Format(@"SELECT DISTINCT FI.FinishGoodProductId,FP.FinishGoodProductName,FI.ReceipeBatchCode,SUM(FI.TargetQuantity) AS TargetQuantity

FROM FinishGoodProductionInfoes FI
INNER JOIN tblFinishGoodProductInfo FP on FI.FinishGoodProductId=FP.FinishGoodProductId
INNER JOIN tblFinishGoodRecipeInfo RI on FI.ReceipeBatchCode=RI.ReceipeBatchCode	
Where 1=1 and FI.ReceipeBatchCode=RI.ReceipeBatchCode  and FI.OrganizationId=9 Group by FP.FinishGoodProductName,FI.ReceipeBatchCode,FI.FinishGoodProductId", Utility.ParamChecker(param));

            return query;
        }


        public FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FinishGoodProductionInfo> GetFinishGoodProductionInfo(long orgId)
        {
            return _finishGoodProductionInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }

        public FinishGoodProductionInfo GetProductionInfoById(long id, long orgId)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(f => f.FinishGoodProductInfoId == id);
        }

        public bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;

            var finishGoodProductionBatch = "Pro-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            if (finishGoodProductionInfoDTO.FinishGoodProductionInfoId == 0)
            {
                FinishGoodProductionInfo finishGoodProductionInfo = new FinishGoodProductionInfo
                {
                    FinishGoodProductId = finishGoodProductionInfoDTO.FinishGoodProductId,
                    ReceipeBatchCode = finishGoodProductionInfoDTO.ReceipeBatchCode,
                    FinishGoodProductionBatch = finishGoodProductionBatch,
                    Quanity = finishGoodProductionInfoDTO.Quanity,
                    TargetQuantity = finishGoodProductionInfoDTO.TargetQuantity,
                    Remarks = finishGoodProductionInfoDTO.Remarks,
                    Status = finishGoodProductionInfoDTO.Status,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId

                };
                _finishGoodProductionInfoRepository.Insert(finishGoodProductionInfo);

            }
            isSuccess = _finishGoodProductionInfoRepository.Save();
            if (isSuccess)
            {

                isSuccess = finishGoodProductionDetailsBusiness.SaveFinishGoodDetails(details, finishGoodProductionBatch, userId, orgId);

            }
            if (isSuccess)
            {
                //AbNormal
                if (details.Count() > 0)
                {

                    List<MRawMaterialIssueStockDetailsDTO> rawMaterialIssueStockDetailsDTOList = new List<MRawMaterialIssueStockDetailsDTO>();
                    List<MRawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfoList = new List<MRawMaterialIssueStockInfoDTO>();

                    foreach (var item in details)
                    {
                        //Test!

                        var rawMaterialStockInfoId = _rawMaterialIssueStockDetailsBusiness.GetRawMaterialIssueStockByMeterialId(item.RawMaterialId, orgId);

                        var checkRawMaterialStockValue = _rawMaterialStockInfo.GetCheckRawmeterislQuantity(item.RawMaterialId, orgId);
                        //var rawMaterialStockInfoId = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockByMeterialId(item.RawMaterialId, orgId);

                        List<MRawMaterialIssueStockDetailsDTO> issuedetails = new List<MRawMaterialIssueStockDetailsDTO> { new MRawMaterialIssueStockDetailsDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId,RawMaterialId=item.RawMaterialId,Quantity=item.RequiredQuantity, UnitID =rawMaterialStockInfoId.UnitID

                        } };
                        rawMaterialIssueStockDetailsDTOList.AddRange(issuedetails);

                        List<MRawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfos = new List<MRawMaterialIssueStockInfoDTO>() { new MRawMaterialIssueStockInfoDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId, RawMaterialId = item.RawMaterialId, Quantity = item.RequiredQuantity, OrganizationId = orgId } };
                        rawMaterialIssueStockInfoList.AddRange(rawMaterialIssueStockInfos);

                    }


                    isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveRawMaterialIssueDetails(rawMaterialIssueStockDetailsDTOList, userId, orgId);

                    //if (isSuccess)
                    //{

                    //    isSuccess = _rawMaterialIssueStockInfoBusiness.UpdateProductIssueRawMaterialStock(rawMaterialIssueStockInfoList);

                    //}

                }
            }

            return isSuccess;
        }
    }
}
