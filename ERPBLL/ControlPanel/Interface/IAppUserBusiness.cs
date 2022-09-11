using ERPBO.Common;
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
   public interface IAppUserBusiness
    {
        IEnumerable<AppUser> GetAllAppUserByOrgId(long orgId);
        bool SaveAppUser(AppUserDTO appUserDTO, long userId, long orgId);
        bool SaveSRAppUser(AppUserDTO appUserDTO, long userId, long orgId, string role,out string srUserId);
        AppUser GetAppUserOneById(long id, long orgId);
        bool IsDuplicateEmployeeId(string employeeId, long id, long orgId);
        IEnumerable<AppUser> GetAllAppUsers();
        UserDetaildDTO GetUserDetail(long userId, long orgId);
        Task<UserInformation> GetUserInformation(UserLogInViewModel loginModel);
        bool ChangePassword(ChangePasswordDTO dto, long userId, long orgId);
        AppUserDTO GetAppUserInfoById(long id, long orgId,string flag);
        ExecutionStateWithText SaveAppUser2(AppUserDTO appUserDTO, long userId, long orgId);
    }
}
