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
        IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceipCode(long productId,long orgId);

        bool SaveFinishGoodRecipe(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> details, long userId, long orgId);
        IEnumerable<FinishGoodRecipeInfoDTO> GetFinishGoodRecipeInfos(long orgId, long? ProductId);
        FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByOrgId(long id, long orgId);
        FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByFGID(long id, long orgId);
        FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByBatchCode(string receipeBatchCode, long orgId);
        bool DeletefinishGoodRecipe(long id, long userId, long orgId);
        FinishGoodRecipeInfo GetReceipId(long ProductId, int ProductUnitQty, long UnitId);
        IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodUnitQty(long orgId);
        IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodReceipUnitQty(long finishGoodProductId, long orgId);


        IEnumerable<FinishGoodRecipeInfo> GetCheckDupliketReceipeProduct(long FinishGoodProductId,int FGRQty, long UnitId);



        FinishGoodRecipeInfo GetReceipbachcodeid( string ReceipeBatchCode);
        FinishGoodRecipeInfo GetUnitId(string ReceipeBatchCode,double UnitId);


        IEnumerable<MeasurementSetupDTO> GetAllMEarusmentUnitQty(long fgrid);


    }
}
