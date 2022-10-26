﻿using ERPBLL.Agriculture.Interface;
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
    public class FinishGoodRecipeInfoBusiness : IFinishGoodRecipeInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodRecipeInfoRepository _finishGoodRecipeInfoRepository;
        private readonly IFinishGoodRecipeDetailsBusiness _fDetail;
        //, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness
        public FinishGoodRecipeInfoBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork,IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._fDetail = finishGoodRecipeDetailsBusiness;
            this._finishGoodRecipeInfoRepository = new FinishGoodRecipeInfoRepository(this._AgricultureUnitOfWork);
   
        }
        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByOrgId(long id, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(i => i.FGRId == id && i.OrganizationId == orgId);
        }
        public bool DeletefinishGoodRecipe(long id, long userId, long orgId)
        {
            _finishGoodRecipeInfoRepository.DeleteAll(i => i.FGRId == id && i.OrganizationId == orgId);
            return _finishGoodRecipeInfoRepository.Save();
        }

        public IEnumerable<FinishGoodRecipeInfoDTO> GetFinishGoodRecipeInfos(long orgId, long? ProductId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodRecipeInfoss(orgId, ProductId)).ToList();
        }

        private string QueryForFinishGoodRecipeInfoss(long orgId, long? productId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and fgr.OrganizationId={0}", orgId);
            if (productId != null && productId > 0)
            {
                param += string.Format(@" and fgr.FinishGoodProductId={0}", productId);
            }
            query = string.Format(@"SELECT fgr.FGRId,fgr.FinishGoodProductId,fg.FinishGoodProductName,
                  fgr.FGRQty,fgr.UnitId,unit.UnitName,fgr.OrganizationId 
                 FROM [Agriculture].dbo.tblFinishGoodRecipeInfo fgr
                 INNER JOIN [Agriculture].dbo.tblFinishGoodProductInfo fg on fgr.FinishGoodProductId=fg.FinishGoodProductId 
inner join tblAgroUnitInfo unit on unit.UnitId=fgr.UnitId	
Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public bool SaveFinishGoodRecipe(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
            var ReceipeBatchCodes = "RecBC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            if (info.FGRId == 0)
            {
                FinishGoodRecipeInfo model = new FinishGoodRecipeInfo
                {
                    FinishGoodProductId = info.FinishGoodProductId,
                    ReceipeBatchCode= ReceipeBatchCodes,
                    FGRQty = info.FGRQty,
                    UnitId = info.UnitId,
                    OrganizationId = orgId,
                    Status=info.Status,
                    EUserId = userId,
                    EntryDate = DateTime.Now
                   
                };
                List<FinishGoodRecipeDetails> modelDetails = new List<FinishGoodRecipeDetails>();

                foreach (var item in details)
                {
                    FinishGoodRecipeDetails FinishGoodRecipeDetails = new FinishGoodRecipeDetails()
                    {
                        RawMaterialId = item.RawMaterialId,
                        ReceipeBatchCode= ReceipeBatchCodes,
                        FGRRawMaterQty = item.FGRRawMaterQty,
                        UnitId = item.UnitId,
                        OrganizationId = orgId,
                        EUserId = userId,
                        EntryDate = DateTime.Now
                    };
                    modelDetails.Add(FinishGoodRecipeDetails);
                }
                model.FinishGoodRecipeDetails = modelDetails;

                _finishGoodRecipeInfoRepository.Insert(model);
                IsSuccess = _finishGoodRecipeInfoRepository.Save();
            }
            else
            {

             IsSuccess=   _fDetail.updateFinishGoodRecipDetails(info,details,userId, orgId);

            }
            return IsSuccess;
        }

        public IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceif(long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(f => f.OrganizationId == orgId);
        }
        //public IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodReceif(long orgId)
        //{
        //    return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodProducts(orgId)).ToList();
        //}

        //        private string QueryForFinishGoodProducts(long orgId)
        //        {
        //            string query = string.Empty;
        //            string param = string.Empty;

        //            param += string.Format(@" and OrganizationId={0}", orgId);

        //            query = string.Format(@"SELECT fgr.FGRId,fgr.FinishGoodProductId,fg.FinishGoodProductName,
        //                  fgr.FGRQty,fgr.UnitId,unit.UnitName,fgr.OrganizationId 
        //                 FROM [Agriculture].dbo.tblFinishGoodRecipeInfo fgr
        //                 INNER JOIN [Agriculture].dbo.tblFinishGoodProductInfo fg on fgr.FinishGoodProductId=fg.FinishGoodProductId 
        //inner join tblAgroUnitInfo unit on unit.UnitId=fgr.UnitId	
        //Where 1=1 {0}", Utility.ParamChecker(param));

        //            return query;
        //        }

        public IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceipCode(long productId, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(a => a.FinishGoodProductId == productId && a.OrganizationId == orgId).ToList();
        }

        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByFGID(long id, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(a => a.FinishGoodProductId == id && a.OrganizationId == orgId);
        }

        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByBatchCode(string receipeBatchCode, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(i => i.ReceipeBatchCode == receipeBatchCode && i.OrganizationId == orgId);
        }
    }
}
