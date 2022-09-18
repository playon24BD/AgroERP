using ERPBLL.Agriculture.Interface;
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
        public FinishGoodRecipeDetailsBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodRecipeDetailsRepository = new FinishGoodRecipeDetailsRepository(this._AgricultureUnitOfWork);
            //this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
        }
    }
}
