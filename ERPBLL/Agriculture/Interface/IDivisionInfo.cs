using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IDivisionInfo
    {
        DivisionInfo GetDivisionInfoById(long divisionId, long orgId);
        IEnumerable<DivisionInfoDTO> GetDivisionInfos(long orgId, long? divisionId,long? zoneId);
        IEnumerable<DivisionInfo> GetAllDivisionSetup(long OrgId);
        IEnumerable<DivisionInfoDTO> GetAllDivisionDetails(long ZoneId, long orgId);

        bool SaveDivisionInfo(List<DivisionInfoDTO> infoDTO, long userId, long orgId);
    }
}
