using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class TerritorySetupBusiness : ITerritorySetup
    {
        public IEnumerable<TerritorySetup> GetAllTerritorySetup(long OrgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TerritorySetupDTO> GetTerritoryInfos(long orgId, long? territoryId, long? divisionId)
        {
            throw new NotImplementedException();
        }

        public TerritorySetup GetTerritoryNamebyId(long territoryId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveTerritoryInfo(List<TerritorySetupDTO> detailsDTO, long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveTerritoryInfoEdit(TerritorySetupDTO dTO, long userId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
