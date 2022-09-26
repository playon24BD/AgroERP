using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IFinishGoodProductionDetailsBusiness
    {
        IEnumerable<FinishGoodProductionDetails> GetFinishGoodProductionDetails(long orgId);
        FinishGoodProductionDetails GetProductionInfoById(long id, long orgId);
        FinishGoodProductionDetails GetFinishGoodProductionDetailsByAny(string any, long orgId);
        bool SaveFinishGoodDetails(List<FinishGoodProductionDetailsDTO> finishGoodProductionDetailsDTO,string finishGoodProductionBatch, long userId, long orgId);

    }
}
