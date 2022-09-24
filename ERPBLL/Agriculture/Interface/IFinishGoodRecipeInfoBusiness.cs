using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IFinishGoodRecipeInfoBusiness
    {
        IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceif(long orgId);

        bool SaveFinishGoodRecipe(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> details, long userId, long orgId);
        IEnumerable<FinishGoodRecipeInfoDTO> GetFinishGoodRecipeInfos(long orgId, long? ProductId);
        FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByOrgId(long id, long orgId);
        bool DeletefinishGoodRecipe(long id, long userId, long orgId);
    }
}
