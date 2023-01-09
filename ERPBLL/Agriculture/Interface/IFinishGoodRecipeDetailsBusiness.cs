using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IFinishGoodRecipeDetailsBusiness
    {
        IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByInfoId(long infoId, long orgId);
        IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByBatchCode(string receipeBatchCode, long orgId);

        FinishGoodRecipeDetails GetFinishGoodRecipeDetailsById(long fgDetailsId, long orgId);
        //bool updateFinishGoodRecipDetails(List<FinishGoodRecipeDetailsDTO> finishGoodRecipeDetail,long userId, long orgId);
        bool updateFinishGoodRecipDetails(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> finishGoodRecipeDetail, long userId, long orgId);

        IEnumerable<FinishGoodRecipeDetailsDTO> FinishGoodRecipeMinQty(string RawMaterialIdList, long orgId);

        IEnumerable<FinishGoodRecipeDetailsDTO> GetAgroReciprDetailsByInfoIdRMPrice(long FinishGoodProductId);

        IEnumerable<FinishGoodRecipeDetailsDTO> GetFGProductAmount(long FinishGoodProductId);
    }
}
