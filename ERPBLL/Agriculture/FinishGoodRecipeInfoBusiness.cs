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
    public class FinishGoodRecipeInfoBusiness : IFinishGoodRecipeInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodRecipeInfoRepository _finishGoodRecipeInfoRepository;
        private readonly IFinishGoodRecipeDetailsBusiness _finishGoodRecipeDetailsBusiness;

        public FinishGoodRecipeInfoBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodRecipeInfoRepository = new FinishGoodRecipeInfoRepository(this._AgricultureUnitOfWork);
            this._finishGoodRecipeDetailsBusiness = finishGoodRecipeDetailsBusiness;
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
            return IsSuccess;
        }
    }
}
