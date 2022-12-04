using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
    public interface IOrganizationAuthBusiness
    {
        IEnumerable<MainMenuDTO> GetMainMenusForOrgAuth();
        IEnumerable<MainMenuDTO> GetMainMenusForOrgAuthById(long orgId);
        IEnumerable<OrganizationAuthorization> GetOrganizationAuthorizations(long orgId);
        bool SaveOrganizationAuthMenus(OrgAuthMenusDTO dto, long userId); 
    }
}
