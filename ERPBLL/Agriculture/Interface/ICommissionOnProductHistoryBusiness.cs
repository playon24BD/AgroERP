using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;

namespace ERPBLL.Agriculture.Interface
{
    public interface ICommissionOnProductHistoryBusiness
    {
        bool SaveCommissionOnProductHistory(List<CommisionOnProductHistoryDTO> commisionOnProductHistoryDTOs,long userId,long orgId);
        bool SaveCommissionOnProductHistoryWhenInsert(List<CommisionOnProduct> commisionOnProductHistoryDTOs, long userId, long orgId);
    }
}
