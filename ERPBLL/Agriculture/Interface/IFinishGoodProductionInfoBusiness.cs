using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
   public interface IFinishGoodProductionInfoBusiness
    {
        //IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfosList(long? productId,string finishGoodProductionBatch);
        IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfos(long orgId);
        IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductionInfo(long orgId);
        //FinishGoodProductionInfo GetCheckFinishGoodQuantity(long FinishGoodProductInfoId, long orgId);
        IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId,string ProductUnitQty,long? CheckQty, long orgId);

        FinishGoodProductionInfo GetProductionInfoById(long id, long orgId);
        FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId);
        bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId);


        IEnumerable<FinishGoodProductionInfoDTO> FinishgoodproductInOutreturnStockInfos(string ReceipeBatchCode,long? productId);


        FinishGoodProductionInfo Getcheckqty(long FinishGoodProductInfoId, string ProductUnitQty);


    }
}
