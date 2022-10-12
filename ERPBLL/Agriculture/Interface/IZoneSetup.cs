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
       
        //bool SaveZoneInfo(IEnumerable<ZoneSetupDTO> detailsDTO, ZoneSetupDTO edto, long userId, long orgId);
        IEnumerable<ZoneSetup> GetAllZoneName();
        bool SaveZoneInfo(List<ZoneSetupDTO> detailsDTO, long userId, long orgId);
        bool SaveZoneInfoEdit(ZoneSetupDTO dTO, long userId, long orgId);
    }
}
