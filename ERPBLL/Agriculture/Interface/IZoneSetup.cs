using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IZoneSetup
    {
        IEnumerable<ZoneSetup> GetAllZoneSetup(long OrgId);
        ZoneSetup GetZoneNamebyId(long zoneId, long orgId);
        bool SaveZonetInfo(ZoneSetupDTO infoDTO, long orgId, long userId);
    }
}
