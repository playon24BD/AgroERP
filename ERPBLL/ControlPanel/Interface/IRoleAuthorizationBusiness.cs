using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPBO.ControlPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
    public interface IRoleAuthorizationBusiness
    {
        IEnumerable<UserCustomMenusDTO> GetUserRoleMenus(long roleId, long orgId);
        IEnumerable<RoleAuthorization> GetRoleAuthorizationByUserId(long roleId, long orgId);
        RoleAuthorization GetRoleAuthorizationByUserSubmenuId(long roleId, long orgId, long submenuId);
        bool SaveRoleAuthorization(List<RoleAuthorizationDTO> roleAuthorizationDTOs,long userId, long orgId);
        IEnumerable<UserAuthorizeMenusDTO> GetRoleAuthorizeMenus(long roleId, long orgId);
    }
}
