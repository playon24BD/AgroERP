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
    public interface IUserAuthorizationBusiness
    {
        IEnumerable<UserCustomMenusDTO> GetUserCustomMenus(long userId, long orgId);
        IEnumerable<UserAuthorization> GetUserAuthorizationByUserId(long userId, long orgId);
        UserAuthorization GetUserAuthorizationByUserSubmenuId(long userId, long orgId, long submenuId);
        bool SaveUserAuthorization(List<UserAuthorizationDTO> userAuthorizationDTOs,long userId, long orgId);
        IEnumerable<UserAuthorizeMenusDTO> GetUserAuthorizeMenus(long userId, long orgId);
    }
}
