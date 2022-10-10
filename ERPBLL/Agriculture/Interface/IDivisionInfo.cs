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

        IEnumerable<DivisionInfoDTO> GetDivisionInfos(long orgId, long? divisionId);
        IEnumerable<DivisionInfo> GetAllDivisionSetup(long OrgId);

        bool SaveDivisionInfo(List<DivisionInfoDTO> infoDTO, long userId, long orgId);
    }
}
