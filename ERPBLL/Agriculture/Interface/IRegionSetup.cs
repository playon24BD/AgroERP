using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IRegionSetup
    {
        IEnumerable<RegionSetup> GetAllRegionSetup(long OrgId);

        IEnumerable<RegionSetupDTO> GetRegionInfos(long orgId, long? regionId, long? divisionId);
       

        bool SaveRegionInfo(List<RegionSetupDTO> detailsDTO, long userId, long orgId);

        
    }
}
