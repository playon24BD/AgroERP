using ERPBLL.ControlPanel.Interface;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.DTOModels;
using ERPBO.ControlPanel.ViewModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.ControlPanel
{
    public class RoleAuthorizationBusiness : IRoleAuthorizationBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly RoleAuthorizationRepository _roleAuthorizationRepository;// repo
        private readonly IAppUserBusiness _appUserBusiness;
        public RoleAuthorizationBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork, IAppUserBusiness appUserBusiness)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            this._appUserBusiness = appUserBusiness;
            this._roleAuthorizationRepository = new RoleAuthorizationRepository(this._controlPanelUnitOfWork);
        }       
        public IEnumerable<RoleAuthorization> GetRoleAuthorizationByUserId(long roleId, long orgId)
        {
            return _roleAuthorizationRepository.GetAll(u => u.RoleId == roleId && u.OrganizationId == orgId).ToList();
        }
        public RoleAuthorization GetRoleAuthorizationByUserSubmenuId(long roleId, long orgId,long submenuId)
        {
            return _roleAuthorizationRepository.GetOneByOrg(u => u.RoleId == roleId && u.OrganizationId == orgId && u.SubmenuId == submenuId);
        }

        public IEnumerable<UserAuthorizeMenusDTO> GetRoleAuthorizeMenus(long roleId, long orgId)
        {
            return this._controlPanelUnitOfWork.Db.Database.SqlQuery<UserAuthorizeMenusDTO>(string.Format(@"Select s.SubMenuId,S.SubMenuName,s.ControllerName,s.ActionName,oa.MainmenuId,mm.MenuName 'MainmenuName',oa.ModuleId,m.ModuleName 
,ISNULL(u.TaskId,0) 'TaskId',
(Case When ISNULL(u.[Add],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Add',
(Case When ISNULL(u.[Edit],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Edit',
(Case When ISNULL(u.[Detail],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Detail',
(Case When ISNULL(u.[Delete],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Delete',
(Case When ISNULL(u.[Approval],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Approval',
(Case When ISNULL(u.[Report],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Report',s.ParentSubMenuId,(Case When s.ParentSubMenuId > 0 then (Select SubMenuName From tblSubMenus where SubMenuId = s.ParentSubMenuId)
Else '' End) 'ParentSubmenuName',s.IsViewable
From tblRoleAuthorization u 
Inner Join tblSubMenus s on s.SubMenuId = u.SubmenuId and u.RoleId={0}
Inner Join tblOrganizationAuthorization oa on s.MMId = oa.MainmenuId
Inner Join tblMainMenus mm on oa.MainmenuId = mm.MMId
Inner Join tblModules m on oa.ModuleId = m.MId
Where oa.OrganizationId = {1} and s.IsActAsParent = 0", roleId, orgId)).ToList();
        }

        public IEnumerable<UserCustomMenusDTO> GetUserRoleMenus(long roleId, long orgId)
        {
            return this._controlPanelUnitOfWork.Db.Database.SqlQuery<UserCustomMenusDTO>(string.Format(@"Select s.SubMenuId,S.SubMenuName,oa.MainmenuId,mm.MenuName 'MainmenuName',oa.ModuleId,m.ModuleName 
,ISNULL(u.TaskId,0) 'TaskId',
(Case When ISNULL(u.[Add],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Add',
(Case When ISNULL(u.[Edit],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Edit',
(Case When ISNULL(u.[Detail],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Detail',
(Case When ISNULL(u.[Delete],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Delete',
(Case When ISNULL(u.[Approval],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Approval',
(Case When ISNULL(u.[Report],0)=0 then Cast('False' as bit) else Cast('True' as bit) End) 'Report',s.ParentSubMenuId,'' ParentSubmenuName
From tblSubMenus s
Inner Join tblOrganizationAuthorization oa on s.MMId = oa.MainmenuId
Inner Join tblMainMenus mm on oa.MainmenuId = mm.MMId
Inner Join tblModules m on oa.ModuleId = m.MId
Left Join tblRoleAuthorization u on s.SubMenuId = u.SubmenuId and u.RoleId={0}
Where oa.OrganizationId = {1} and IsActAsParent = 0", roleId, orgId)).ToList();
        }
        public bool SaveRoleAuthorization(List<RoleAuthorizationDTO> roleAuthorizationDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;
            if(roleAuthorizationDTOs.Count > 0)
            {
                var appUserRole = roleAuthorizationDTOs.FirstOrDefault().RoleId;
                var appOrgId = roleAuthorizationDTOs.FirstOrDefault().OrganizationId;
                var userAuthInDb = GetRoleAuthorizationByUserId(appUserRole, appOrgId);

                var userAuthSubmenuInModel = roleAuthorizationDTOs.Select(u => u.SubmenuId).ToList();
                //Delete
                _roleAuthorizationRepository.DeleteAll(ua => ua.RoleId == appUserRole && ua.OrganizationId == appOrgId && !userAuthSubmenuInModel.Contains(ua.SubmenuId));
                List<RoleAuthorization> authorizations = new List<RoleAuthorization>();
                foreach (var item in roleAuthorizationDTOs)
                {
                    var existInDb = userAuthInDb.FirstOrDefault(s => s.SubmenuId == item.SubmenuId);
                    if(existInDb != null)
                    {
                        existInDb.Add = item.Add;
                        existInDb.Edit = item.Edit;
                        existInDb.Detail = item.Detail;
                        existInDb.Delete = item.Delete;
                        existInDb.Approval = item.Approval;
                        existInDb.Report = item.Report;
                        existInDb.RoleId = appUserRole;
                        existInDb.UpUserId = userId;
                        existInDb.UpdateDate = DateTime.Now;
                        _roleAuthorizationRepository.Update(existInDb);
                    }
                    else
                    {
                        RoleAuthorization roleAuthorization = new RoleAuthorization
                        {
                            RoleId = appUserRole,
                            SubmenuId = item.SubmenuId,
                            MainmenuId=item.MainmenuId,
                            ModuleId = item.ModuleId,
                            OrganizationId=item.OrganizationId,
                            ParentSubmenuId=item.ParentSubmenuId,
                            Add= item.Add,
                            Edit=item.Edit,
                            Delete= item.Delete,
                            Detail=item.Detail,
                            Approval=item.Approval,
                            Report=item.Report,
                            EUserId=userId,
                            EntryDate=DateTime.Now
                        };
                        authorizations.Add(roleAuthorization);
                    }

                }
                if(authorizations.Count > 0)
                {
                    _roleAuthorizationRepository.InsertAll(authorizations);
                }
                IsSuccess = _roleAuthorizationRepository.Save();
            }
            return IsSuccess;
        }
    }
}
