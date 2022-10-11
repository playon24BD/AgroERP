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
       

        IEnumerable<RegionSetupDTO> GetAllRegionDetails(long DivisionId, long orgId);

        bool SaveRegionInfo(List<RegionSetupDTO> detailsDTO, long userId, long orgId);

        bool SaveRegionInfoEdit(RegionSetupDTO dTO, long userId, long orgId);


        RegionSetup GetRegionNamebyId(long regionId, long orgId);

    }
}
