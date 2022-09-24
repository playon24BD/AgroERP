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
   public class FinishGoodRecipeDetailsBusiness : IFinishGoodRecipeDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodRecipeDetailsRepository _finishGoodRecipeDetailsRepository;
        //private readonly IFinishGoodRecipeInfoBusiness _finishGoodRecipeInfoBusiness;
        public FinishGoodRecipeDetailsBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodRecipeDetailsRepository = new FinishGoodRecipeDetailsRepository(this._AgricultureUnitOfWork);
            //this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
        }

        public FinishGoodRecipeDetails GetFinishGoodRecipeDetailsById(long fgDetailsId,long orgId)
        {
           return _finishGoodRecipeDetailsRepository.GetOneByOrg(a=>a.FGRDetailsId==fgDetailsId && a.OrganizationId==orgId);
        }

        public IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByInfoId(long infoId, long orgId)
        {
            return _finishGoodRecipeDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.FGRId == infoId).ToList();
        }

        public bool updateFinishGoodRecipDetails(List<FinishGoodRecipeDetailsDTO> finishGoodRecipeDetailDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<FinishGoodRecipeDetails> finishGoodRecipeDetails = new List<FinishGoodRecipeDetails>();
            FinishGoodRecipeDetails finishGoodRecipeDetail = new FinishGoodRecipeDetails();
            foreach (var item in finishGoodRecipeDetailDTO)
            {
                finishGoodRecipeDetail= GetFinishGoodRecipeDetailsById(item.FGRDetailsId, orgId);
                finishGoodRecipeDetail.FGRRawMaterQty = item.FGRRawMaterQty;
                finishGoodRecipeDetail.UpdateDate = DateTime.Now;
                finishGoodRecipeDetail.UpUserId = userId;
                finishGoodRecipeDetails.Add(finishGoodRecipeDetail);
            }
            _finishGoodRecipeDetailsRepository.UpdateAll(finishGoodRecipeDetails);
            IsSuccess = _finishGoodRecipeDetailsRepository.Save();

            return IsSuccess;
        }
    }
}
