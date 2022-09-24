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
                  fgr.FGRQty,fgr.FGRUnit,fgr.OrganizationId 
                 FROM [Agriculture].dbo.tblFinishGoodRecipeInfo fgr
                 INNER JOIN [Agriculture].dbo.tblFinishGoodProductInfo fg on fgr.FinishGoodProductId=fg.FinishGoodProductId Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public bool SaveFinishGoodRecipe(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
      
            if (info.FGRId == 0)
            {
                FinishGoodRecipeInfo model = new FinishGoodRecipeInfo
                {
                    FinishGoodProductId = info.FinishGoodProductId,
                    FGRQty = info.FGRQty,
                    FGRUnit = info.FGRUnit,
                    OrganizationId = orgId,
                    EUserId = userId,
                    EntryDate = DateTime.Now
                   
                };
                List<FinishGoodRecipeDetails> modelDetails = new List<FinishGoodRecipeDetails>();

                foreach (var item in details)
                {
                    FinishGoodRecipeDetails FinishGoodRecipeDetails = new FinishGoodRecipeDetails()
                    {
                        RawMaterialId = item.RawMaterialId,
                        FGRRawMaterQty = item.FGRRawMaterQty,
                        FGRRawMaterUnit = item.FGRRawMaterUnit,
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

             IsSuccess=   _fDetail.updateFinishGoodRecipDetails(details, userId, orgId);

            }
            return IsSuccess;
        }
    }
}
