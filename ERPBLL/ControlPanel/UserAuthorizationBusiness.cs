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
    public class UserAuthorizationBusiness : IUserAuthorizationBusiness
    {
        private readonly IControlPanelUnitOfWork _controlPanelUnitOfWork; // database
        private readonly UserAuthorizationRepository _userAuthorizationRepository;// repo
        private readonly IAppUserBusiness _appUserBusiness;
        public UserAuthorizationBusiness(IControlPanelUnitOfWork controlPanelUnitOfWork, IAppUserBusiness appUserBusiness)
        {
            this._controlPanelUnitOfWork = controlPanelUnitOfWork;
            this._appUserBusiness = appUserBusiness;
            this._userAuthorizationRepository = new UserAuthorizationRepository(this._controlPanelUnitOfWork);
        }       
        public IEnumerable<UserAuthorization> GetUserAuthorizationByUserId(long userId, long orgId)
        {
            return _userAuthorizationRepository.GetAll(u => u.UserId == userId && u.OrganizationId == orgId).ToList();
        }
        public UserAuthorization GetUserAuthorizationByUserSubmenuId(long userId, long orgId,long submenuId)
        {
            return _userAuthorizationRepository.GetOneByOrg(u => u.UserId == userId && u.OrganizationId == orgId && u.SubmenuId == submenuId);
        }

        public IEnumerable<UserAuthorizeMenusDTO> GetUserAuthorizeMenus(long userId, long orgId)
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
From tblUserAuthorization u 
Inner Join tblSubMenus s on s.SubMenuId = u.SubmenuId and u.UserId={0}
Inner Join tblOrganizationAuthorization oa on s.MMId = oa.MainmenuId
Inner Join tblMainMenus mm on oa.MainmenuId = mm.MMId
Inner Join tblModules m on oa.ModuleId = m.MId
Where oa.OrganizationId = {1} and s.IsActAsParent = 0 Order By u.TaskId", userId, orgId)).ToList();
        }

        public IEnumerable<UserCustomMenusDTO> GetUserCustomMenus(long userId, long orgId)
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
Left Join tblUserAuthorization u on s.SubMenuId = u.SubmenuId and u.UserId={0}
Where oa.OrganizationId = {1} and IsActAsParent = 0", userId,orgId)).ToList();
        }
        public bool SaveUserAuthorization(List<UserAuthorizationDTO> userAuthorizationDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;
            if(userAuthorizationDTOs.Count > 0)
            {
                var appUser = userAuthorizationDTOs.FirstOrDefault().UserId;
                var appOrgId = userAuthorizationDTOs.FirstOrDefault().OrganizationId;
                var userAuthInDb = GetUserAuthorizationByUserId(appUser, appOrgId);

                var userRoleId = _appUserBusiness.GetAppUserOneById(appUser, appOrgId).RoleId;

                var userAuthSubmenuInModel = userAuthorizationDTOs.Select(u => u.SubmenuId).ToList();
                //Delete
                _userAuthorizationRepository.DeleteAll(ua => ua.UserId == appUser && ua.OrganizationId == appOrgId && !userAuthSubmenuInModel.Contains(ua.SubmenuId));
                List<UserAuthorization> authorizations = new List<UserAuthorization>();
                foreach (var item in userAuthorizationDTOs)
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
                        existInDb.RoleId = userRoleId;
                        existInDb.UpUserId = userId;
                        existInDb.UpdateDate = DateTime.Now;
                        _userAuthorizationRepository.Update(existInDb);
                    }
                    else
                    {
                        UserAuthorization userAuthorization = new UserAuthorization
                        {
                            UserId = appUser,
                            RoleId = userRoleId,
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
                        authorizations.Add(userAuthorization);
                    }

                }
                if(authorizations.Count > 0)
                {
                    _userAuthorizationRepository.InsertAll(authorizations);
                }
                IsSuccess = _userAuthorizationRepository.Save();
            }
            return IsSuccess;
        }
    }
}
