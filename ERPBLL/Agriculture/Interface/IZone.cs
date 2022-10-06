using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IZone
    {
        IEnumerable<Zone> GetAllZoneInfo(long OrgId);
        IEnumerable<ZoneDTO> GetZoneInfos(long orgId, long? ZoneId);
        bool SaveZoneInfo(ZoneDTO zoneInfo,List<ZoneDetailDTO> zoneDetailDTOs, long userId, long orgId);
    }
}
