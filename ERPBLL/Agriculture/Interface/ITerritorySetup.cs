using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ITerritorySetup
    {
        IEnumerable<TerritorySetup> GetAllTerritorySetup(long OrgId);

        IEnumerable<TerritorySetupDTO> GetTerritoryInfos(long orgId, long? territoryId, long? divisionId);

        bool SaveTerritoryInfo(List<TerritorySetupDTO> detailsDTO, long userId, long orgId);

        bool SaveTerritoryInfoEdit(TerritorySetupDTO dTO, long userId, long orgId);


        TerritorySetup GetTerritoryNamebyId(long territoryId, long orgId);

    }
}
