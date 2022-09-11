using ERPBLL.ControlPanel.Interface;
using ERPBO.Common;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly RoleRepository roleRepository; // repo
        public RoleBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            roleRepository = new RoleRepository(this._controlPanelUnitOfWork);
        }
        public IEnumerable<Role> GetAllRoleByOrgId(long orgId)
        {
            return roleRepository.GetAll(r => r.OrganizationId == orgId).ToList();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return roleRepository.GetAll();
        }

        public Role GetRoleById(long id)
        {
            return roleRepository.GetOneByOrg(role => role.RoleId == id);
        }

        public IEnumerable<TechnicalServiceByRoleDTO> GetRoleByTechnicalServicesId(string roleName, long orgId, long BranchId)
        {
            return _controlPanelUnitOfWork.Db.Database.SqlQuery<TechnicalServiceByRoleDTO>(string.Format(@"Select us.UserId,us.UserName,rl.RoleId,rl.RoleName,us.BranchId From [ControlPanel].dbo.tblApplicationUsers us
        Inner Join [ControlPanel].dbo.tblRoles rl on us.OrganizationId = rl.OrganizationId and us.RoleId = rl.RoleId
        Where 1=1 and us.OrganizationId= {0} and us.BranchId={1}
        and rl.RoleName='Engineer'", orgId, BranchId)).ToList();
        }

        public Role GetRoleOneById(long id, long orgId)
        {
            return roleRepository.GetOneByOrg(role => role.RoleId == id && role.OrganizationId == orgId);
        }

        public bool IsDuplicateRoleName(string roleName, long id, long orgId)
        {
            return roleRepository.GetOneByOrg(role => role.RoleName == roleName && role.RoleId != id && role.OrganizationId == orgId) != null ? true : false;
        }

        public ExecutionStateWithText SaveAppRole(RoleDTO roleDTO, long userId, long orgId)
        {
            ExecutionStateWithText execution = new ExecutionStateWithText();
            Role role = new Role();
            if (roleDTO.RoleId == 0)
            {
                role.RoleName = roleDTO.RoleName;
                role.EUserId = userId;
                role.OrganizationId = roleDTO.OrganizationId;
                role.EntryDate = DateTime.Now;
                roleRepository.Insert(role);
            }
            else
            {
                role = GetRoleOneById(roleDTO.RoleId, orgId);
                role.RoleName = roleDTO.RoleName;
                role.UpUserId = userId;
                //role.OrganizationId = roleDTO.OrganizationId;
                role.UpdateDate = DateTime.Now;
                roleRepository.Update(role);
            }
            execution.isSuccess = roleRepository.Save();
            execution.text = role.RoleId.ToString();

            return execution;
        }

        public bool SaveRole(RoleDTO roleDTO, long userId, long orgId)
        {
            Role role = new Role();
            if (roleDTO.RoleId == 0)
            {
                role.RoleName = roleDTO.RoleName;
                role.EUserId = userId;
                role.OrganizationId = roleDTO.OrganizationId;
                role.EntryDate = DateTime.Now;
                roleRepository.Insert(role);
            }
            else
            {
                role = GetRoleOneById(roleDTO.RoleId, orgId);
                role.RoleName = roleDTO.RoleName;
                role.UpUserId = userId;
                //role.OrganizationId = roleDTO.OrganizationId;
                role.UpdateDate = DateTime.Now;
                roleRepository.Update(role);
            }
            return roleRepository.Save();
        }
    }
}
