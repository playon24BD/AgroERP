using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IDepotSetup
    {
        IEnumerable<DepotSetup> GetAllDepotSetup(long OrgId);
        DepotSetup GetDepotNamebyId(long depotId, long orgId);
        bool SaveDepotInfo(DepotSetupDTO infoDTO, long orgId, long userId);


    }
}
