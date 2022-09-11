
using ERPBO.Common;
using ERPWeb.Filters;
using System.Linq;
using System.Web.Mvc;

using ERPBLL.ControlPanel.Interface;
using System.Collections.Generic;
using ERPBO.ControlPanel.ViewModels;

using ERPBLL.Common;


using System;



namespace ERPWeb.Controllers
{
    [CustomAuthorize]
    public class CommonController : BaseController
    {




        #region ControlPanel
        private readonly IAppUserBusiness _appUserBusiness;
        private readonly IRoleBusiness _roleBusiness;
        private readonly IBranchBusiness _branchBusiness;
        private readonly IOrganizationBusiness _organizationBusiness;
        private readonly IUserAuthorizationBusiness _userAuthorizationBusiness;
        private readonly IModuleBusiness _moduleBusiness;
        private readonly IManiMenuBusiness _maniMenuBusiness;
        private readonly ISubMenuBusiness _subMenuBusiness;
        #endregion

    

        public CommonController(IAppUserBusiness appUserBusiness, IRoleBusiness roleBusiness, IBranchBusiness branchBusiness, IOrganizationBusiness organizationBusiness, IUserAuthorizationBusiness userAuthorizationBusiness, IModuleBusiness moduleBusiness, IManiMenuBusiness maniMenuBusiness, ISubMenuBusiness subMenuBusiness)
        {
      

  

            #region ControlPanel
            this._appUserBusiness = appUserBusiness;
            this._roleBusiness = roleBusiness;
            this._branchBusiness = branchBusiness;
            this._organizationBusiness = organizationBusiness;
            this._userAuthorizationBusiness = userAuthorizationBusiness;
            this._moduleBusiness = moduleBusiness;
            this._maniMenuBusiness = maniMenuBusiness;
            this._subMenuBusiness = subMenuBusiness;
            #endregion

      
        }



        #region Control Panel

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsDuplicateEmployeeId(string employeeId, long id)
        {
            bool isExist = _appUserBusiness.IsDuplicateEmployeeId(employeeId, id, User.OrgId);
            return Json(isExist);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsUserExist(string userName, long id)
        {
            bool isUserExist = _appUserBusiness.GetAllAppUsers().Where(u => u.UserName.ToLower() == userName.ToLower() && u.UserId != id).FirstOrDefault() != null;

            return Json(isUserExist);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsEmailExist(string email, long id)
        {
            bool isEmailExist = _appUserBusiness.GetAllAppUsers().Where(u => u.Email.ToLower() == email.ToLower() && u.UserId != id).FirstOrDefault() != null;

            return Json(isEmailExist);
        }
        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsDuplicateRoleName(long roleId, string roleName, long orgId)
        {
            bool IsDuplicate = false;
            IsDuplicate = _roleBusiness.IsDuplicateRoleName(roleName, roleId, orgId);
            return Json(IsDuplicate);
        }

        [HttpPost]
        public ActionResult IsCurrentUserPasswordCorrect(string password)
        {
            bool IsCorrect = false;
            UserLogInViewModel loginModel = new UserLogInViewModel
            {
                UserName = User.UserName
            };
            loginModel.Password = Utility.Encrypt(password);
            var user = _appUserBusiness.GetAppUserOneById(User.UserId, User.OrgId);
            if (user != null)
            {
                IsCorrect = user.Password == Utility.Encrypt(password);
            }
            return Json(IsCorrect);
        }
        [HttpPost]
        public ActionResult GetModules()
        {
            var modules =_moduleBusiness.GetAllModules().Select(s => new Dropdown { text = s.ModuleName, value = s.MId.ToString() }).ToList();
            return Json(modules);
        }
        [HttpPost]
        public ActionResult GetMainmenus()
        {
            var mainmenus = _maniMenuBusiness.GetAllMainMenu().Select(s => new Dropdown { text = s.MenuName, value = s.MMId.ToString() }).ToList();
            return Json(mainmenus);
        }
        [HttpPost]
        public ActionResult GetSubmenus()
        {
            var submenus = _subMenuBusiness.GetAllSubMenu().Select(s => new Dropdown { text = s.SubMenuName, value = s.SubMenuId.ToString() }).ToList();
            return Json(submenus);
        }
        [HttpPost]
        public ActionResult GetParentSubmenus()
        {
            var submenus = _subMenuBusiness.GetAllSubMenu().Where(s=> s.IsActAsParent).Select(s => new Dropdown { text = s.SubMenuName, value = s.SubMenuId.ToString() }).ToList();
            return Json(submenus);
        }
        #endregion

        #region User Menus
        public ActionResult GetUserMenus()
        {
            // This is a three level menu //
            List<UserMainMenuViewModel> listOfUserMainMenuViewModel = new List<UserMainMenuViewModel>();
            if (User.UserId > 0 && User.OrgId > 0 && User.IsUserActive == true)
            {
                var userAllMenus = (List<UserAuthorizeMenusViewModels>)Session["UserAuthorizeMenus"];

                //var userAuth = _userAuthorizationBusiness.GetUserAuthorizeMenus(userInformation.UserId, userInformation.OrganizationId);
               // var userAuth = _requsitionInfoBusiness.GetRequsitionInfosByQueryCountPending(User.OrgId);
                //IEnumerable<UserAuthorizeMenusViewModels> userCustomMenus = new List<UserAuthorizeMenusViewModels>();
                //AutoMapper.Mapper.Map(userAuth, userCustomMenus);
                //Session["UserAuthorizeMenus"] = userCustomMenus;

                var menus = (from mm in userAllMenus
                             select new { MainmenuId = mm.MainmenuId, MainmenuName = mm.MainmenuName }).OrderBy(s=>s.MainmenuId).Distinct().ToList();

                foreach (var mm in menus)
                {
                    UserMainMenuViewModel userMainMenuViewModel = new UserMainMenuViewModel();
                    userMainMenuViewModel.MainmenuId = mm.MainmenuId;
                    userMainMenuViewModel.MainmenuName = mm.MainmenuName;

                    List<UserSubmenuViewModel> listOfSubmenus = new List<UserSubmenuViewModel>();
                    var submenuWithParent = (from sub in userAllMenus
                                             where sub.MainmenuId == mm.MainmenuId && sub.ParentSubMenuId > 0
                                             select new { ParentSubMenuId = sub.ParentSubMenuId, ParentSubmenuName = sub.ParentSubmenuName }).Distinct().ToList();

                    var submenuWithoutParent = (from sub in userAllMenus
                                                where sub.MainmenuId == mm.MainmenuId && sub.ParentSubMenuId == 0 && sub.IsViewable == true
                                                select new { SubMenuId = sub.SubmenuId, SubmenuName = sub.SubMenuName, ControllerName = sub.ControllerName, ActionName = sub.ActionName}).Distinct().ToList();

                    foreach (var submenu in submenuWithoutParent)
                    {
                        UserSubmenuViewModel userSubmenu = new UserSubmenuViewModel();
                        userSubmenu.SubmenuId = submenu.SubMenuId;
                        userSubmenu.SubmenuName = submenu.SubmenuName;
                        userSubmenu.ControllerName = submenu.ControllerName;
                        userSubmenu.ActionName = submenu.ActionName;
                        userSubmenu.IsParent = false;
                        userSubmenu.UserSubSubmenus = new List<UserSubSubmenuViewModel>();
                        listOfSubmenus.Add(userSubmenu);
                    }
                    foreach (var submenu in submenuWithParent)
                    {
                        UserSubmenuViewModel userSubmenu = new UserSubmenuViewModel();
                        userSubmenu.SubmenuId = submenu.ParentSubMenuId;
                        userSubmenu.SubmenuName = submenu.ParentSubmenuName;
                        userSubmenu.ControllerName = string.Empty;
                        userSubmenu.ActionName = string.Empty;
                        userSubmenu.IsParent = true;

                        // Subsubmenu
                        List<UserSubSubmenuViewModel> listOfSubSubmenu = new List<UserSubSubmenuViewModel>();
                        var subsubmenuItems = (from sub in userAllMenus
                                               where sub.ParentSubMenuId == submenu.ParentSubMenuId && sub.IsViewable == true
                                               select new { SubmenuName = sub.SubMenuName, SubmenuId = sub.SubmenuId, ControllerName = sub.ControllerName, ActionName = sub.ActionName }).ToList();

                        foreach (var item in subsubmenuItems)
                        {
                            UserSubSubmenuViewModel subSubmenuViewModel = new UserSubSubmenuViewModel();
                            subSubmenuViewModel.ControllerName = item.ControllerName;
                            subSubmenuViewModel.ActionName = item.ActionName;
                            subSubmenuViewModel.SubsubmenuId = item.SubmenuId;
                            subSubmenuViewModel.SubsubmenuName = item.SubmenuName;
                            listOfSubSubmenu.Add(subSubmenuViewModel);
                        }
                        userSubmenu.UserSubSubmenus = listOfSubSubmenu;
                        listOfSubmenus.Add(userSubmenu);
                    }

                    userMainMenuViewModel.UserSubmenus = listOfSubmenus;
                    listOfUserMainMenuViewModel.Add(userMainMenuViewModel);
                }

            }
            return PartialView("_sidebar", listOfUserMainMenuViewModel);
        }
        #endregion





 


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}