using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAreaSetupBusiness
    {
        bool SaveAreaInfo(List<AreaSetupDTO> detailsDTO, long userId, long orgId);
        bool SaveAreaInfoUpdate(AreaSetupDTO detailsDTO, long userId, long orgId);
        IEnumerable<AreaSetupDTO> GetAllAreaSetup(long OrgId,string name);
    }
}
