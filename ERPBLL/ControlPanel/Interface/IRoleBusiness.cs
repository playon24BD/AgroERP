using ERPBO.Common;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel.Interface
{
   public interface IRoleBusiness
    {
        IEnumerable<Role> GetAllRoleByOrgId(long orgId);
        IEnumerable<Role> GetAllRoles();
        bool SaveRole(RoleDTO roleDTO, long userId, long orgId);
        bool IsDuplicateRoleName(string roleName, long id, long orgId);
        Role GetRoleOneById(long id, long orgId);
        IEnumerable<TechnicalServiceByRoleDTO> GetRoleByTechnicalServicesId(string roleName,long orgId,long BranchId);
        Role GetRoleById(long id);
        ExecutionStateWithText SaveAppRole(RoleDTO roleDTO, long userId, long orgId);
    }
}
