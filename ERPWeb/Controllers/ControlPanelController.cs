using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.ControlPanel.DTOModels;
using ERPBO.ControlPanel.ViewModels;
using ERPWeb.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    [CustomAuthorize]
    public class ControlPanelController : BaseController
    {
        private readonly IOrganizationBusiness _organizationBusiness;
        private readonly IBranchBusiness _branchBusiness;
        private readonly IRoleBusiness _roleBusiness;
        private readonly IAppUserBusiness _appUserBusiness;
        private readonly IModuleBusiness _moduleBusiness;
        private readonly IManiMenuBusiness _maniMenuBusiness;
        private readonly ISubMenuBusiness _subMenuBusiness;
        private readonly IOrganizationAuthBusiness _organizationAuthBusiness;
        private readonly IUserAuthorizationBusiness _userAuthorizationBusiness;
        private readonly IRoleAuthorizationBusiness _roleAuthorizationBusiness;

        public ControlPanelController(IOrganizationBusiness organizationBusiness, IBranchBusiness branchBusiness, IRoleBusiness roleBusiness, IAppUserBusiness appUserBusiness, IModuleBusiness moduleBusiness, IManiMenuBusiness maniMenuBusiness, ISubMenuBusiness subMenuBusiness, IOrganizationAuthBusiness organizationAuthBusiness, IUserAuthorizationBusiness userAuthorizationBusiness, IRoleAuthorizationBusiness roleAuthorizationBusiness)
        {
            this._organizationBusiness = organizationBusiness;
            this._branchBusiness = branchBusiness;
            this._roleBusiness = roleBusiness;
            this._appUserBusiness = appUserBusiness;
            this._moduleBusiness = moduleBusiness;
            this._maniMenuBusiness = maniMenuBusiness;
            this._subMenuBusiness = subMenuBusiness;
            this._organizationAuthBusiness = organizationAuthBusiness;
            this._userAuthorizationBusiness = userAuthorizationBusiness;
            this._roleAuthorizationBusiness = roleAuthorizationBusiness;
        }
        // GET: ControlPanel

        #region Organization
        [HttpGet]
        public ActionResult GetOrganizations()
        {
            IEnumerable<OrganizationDTO> organizationDTOs = _organizationBusiness.GetAllOrganizations().Select(org => new OrganizationDTO
            {
                OrgId = org.OrganizationId,
                OrganizationName = org.OrganizationName,
                ShortName = org.ShortName,
                Address = org.Address,
                PhoneNumber = org.PhoneNumber,
                MobileNumber = org.MobileNumber,
                Website = org.Website,
                Email = org.Email,
                Fax = org.Fax,
                OrgLogoPath = org.OrgLogoPath,
                ReportLogoPath = org.ReportLogoPath,
                IsActive = org.IsActive,
                StateStatus = org.IsActive ? "Active" : "Inactive",
                EUserId = org.EUserId,
                EntryDate = org.EntryDate,
                EntryUser = UserForEachRecord(org.EUserId.Value).UserName,
                UpdateUser = (org.UpUserId == null || org.UpUserId == 0) ? "" : UserForEachRecord(org.UpUserId.Value).UserName
            }).ToList();

            List<OrganizationViewModel> OrganizationViewModels = new List<OrganizationViewModel>();
            AutoMapper.Mapper.Map(organizationDTOs, OrganizationViewModels);
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetOrganizations");
            return View(OrganizationViewModels);
        }

        [HttpGet]
        public ActionResult CreateOrganization(long? oId)
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            string pageTitle = string.Empty;
            pageTitle = "Create New Organization";
            if (oId != null && oId > 0)
            {
                var Org = _organizationBusiness.GetOrganizationById(oId.Value);
                organizationViewModel.OrgId = Org.OrganizationId;
                organizationViewModel.OrganizationName = Org.OrganizationName;
                organizationViewModel.Address = Org.Address;
                organizationViewModel.PhoneNumber = Org.PhoneNumber;
                organizationViewModel.MobileNumber = Org.MobileNumber;
                organizationViewModel.Email = Org.Email;
                organizationViewModel.Website = Org.Website;
                organizationViewModel.IsActive = Org.IsActive;
                organizationViewModel.Fax = Org.Fax;
                organizationViewModel.ShortName = Org.ShortName;
                organizationViewModel.OrgLogoPath = Org.OrgLogoPath;
                organizationViewModel.ReportLogoPath = Org.ReportLogoPath;
                pageTitle = "Update Organization";
            }
            ViewBag.PageTitle = pageTitle;
            return View(organizationViewModel);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveOrganization(OrganizationViewModel model)
        {
            //var privilege = UserPrivilege("ControlPanel", "CreateOrganization");
            //var permission = (model.OrgId == 0 && privilege.Add) || (model.OrgId > 0 && privilege.Edit);
            bool IsSuccess = false;
            // && permission
            if (ModelState.IsValid)
            {
                OrganizationDTO dto = new OrganizationDTO();
                AutoMapper.Mapper.Map(model, dto);
                IsSuccess = _organizationBusiness.SaveOrganization(dto, User.UserId);
            }

            return Json(IsSuccess);
        }
        #endregion

        #region Branch
        public ActionResult GetBranchList(int? page)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetBranchList");
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });

            IPagedList<BranchViewModel> branchViewModels = _branchBusiness.GetBranchByOrgId(User.OrgId).Select(br => new BranchViewModel
            {
                BranchId = br.BranchId,
                BranchName = br.BranchName,
                ShortName = br.ShortName,
                MobileNo = br.MobileNo,
                Address = br.Address,
                Email = br.Email,
                PhoneNo = br.PhoneNo,
                Fax = br.Fax,
                StateStatus = (br.IsActive == true ? "Active" : "Inactive"),
                Remarks = br.Remarks,
                OrgId = br.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(User.OrgId).OrganizationName),
                EntryUser = UserForEachRecord(br.EUserId.Value).UserName,
                UpdateUser = (br.UpUserId == null || br.UpUserId == 0) ? "" : UserForEachRecord(br.UpUserId.Value).UserName
            }).OrderBy(br => br.BranchId).ToPagedList(page ?? 1, 15);
            IEnumerable<BranchViewModel> branchViewModelForPage = new List<BranchViewModel>();
            return View(branchViewModels);
        }
        public ActionResult SaveBranch(BranchViewModel branchViewModel)
        {
            //var pre = UserPrivilege("ControlPanel", "GetBranchList");
            //var permission = ((pre.Edit) || (pre.Add));
            // && permission
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                try
                {
                    BranchDTO dto = new BranchDTO();
                    AutoMapper.Mapper.Map(branchViewModel, dto);
                    isSuccess = _branchBusiness.SaveBranch(dto, User.UserId, User.OrgId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region ORG Config
        public ActionResult OrgConfiguration(string flag, long orgId = 0)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(s => new SelectListItem
                {
                    Text = s.OrganizationName,
                    Value = s.OrganizationId.ToString()
                }).ToList();

                var allmainmenus = _organizationAuthBusiness.GetMainMenusForOrgAuth();
                List<ModuleUIViewModel> viewModels = new List<ModuleUIViewModel>();
                var modules = allmainmenus.Select(s => s.MId).Distinct().ToList();
                foreach (var item in modules)
                {
                    ModuleUIViewModel module = new ModuleUIViewModel();
                    var mainmenu = allmainmenus.Where(m => m.MId == item).ToList();
                    if (mainmenu.Count > 0)
                    {
                        module.MId = item;
                        module.ModuleName = mainmenu.FirstOrDefault().ModuleName;
                        List<MainMenuUIVIewModel> mainMenuUIVIews = new List<MainMenuUIVIewModel>();
                        foreach (var menu in mainmenu)
                        {
                            MainMenuUIVIewModel m = new MainMenuUIVIewModel();
                            m.MMId = menu.MMId;
                            m.MainmenuName = menu.MenuName;
                            mainMenuUIVIews.Add(m);
                        }
                        module.MainMenus = mainMenuUIVIews;
                    }
                    viewModels.Add(module);
                }
                ViewBag.Mainmenus = viewModels;
                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Org")
            {
                IEnumerable<OrganizationDTO> organizationDTOs = _organizationBusiness.GetAllOrganizations().Select(org => new OrganizationDTO
                {
                    OrgId = org.OrganizationId,
                    OrganizationName = org.OrganizationName,
                    ShortName = org.ShortName,
                    Address = org.Address,
                    PhoneNumber = org.PhoneNumber,
                    MobileNumber = org.MobileNumber,
                    Website = org.Website,
                    Email = org.Email,
                    Fax = org.Fax,
                    OrgLogoPath = org.OrgLogoPath,
                    ReportLogoPath = org.ReportLogoPath,
                    IsActive = org.IsActive,
                    StateStatus = org.IsActive ? "Active" : "Inactive",
                    EUserId = org.EUserId,
                    EntryDate = org.EntryDate,
                    EntryUser = UserForEachRecord(org.EUserId.Value).UserName,
                    UpdateUser = (org.UpUserId == null || org.UpUserId == 0) ? "" : UserForEachRecord(org.UpUserId.Value).UserName
                }).ToList();
                List<OrganizationViewModel> OrganizationViewModels = new List<OrganizationViewModel>();
                AutoMapper.Mapper.Map(organizationDTOs, OrganizationViewModels);
                return PartialView("_GetOrg", OrganizationViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "OrgAuth")
            {
                var allmainmenusByOrg = _organizationAuthBusiness.GetMainMenusForOrgAuthById(orgId);
                //allmainmenusByOrg = allmainmenusByOrg.Count() == 0 ? new List<MainMenuDTO>() : allmainmenusByOrg;
                return Json(allmainmenusByOrg);
            }
            return View();
        }

        #endregion

        #region User Config
        // For System Admin
        public ActionResult AppUserConfiguration(string flag, long? userId, long? roleId, long? orgId)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });
                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Branch")
            {

                IEnumerable<BranchViewModel> branchViewModels = _branchBusiness.GetAllBranches().Select(br => new BranchViewModel
                {
                    BranchId = br.BranchId,
                    BranchName = br.BranchName,
                    ShortName = br.ShortName,
                    MobileNo = br.MobileNo,
                    Address = br.Address,
                    Email = br.Email,
                    PhoneNo = br.PhoneNo,
                    Fax = br.Fax,
                    StateStatus = (br.IsActive == true ? "Active" : "Inactive"),
                    Remarks = br.Remarks,
                    OrgId = br.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(br.OrganizationId).OrganizationName),
                    EntryUser = UserForEachRecord(br.EUserId.Value).UserName,
                    UpdateUser = (br.UpUserId == null || br.UpUserId == 0) ? "" : UserForEachRecord(br.UpUserId.Value).UserName
                }).OrderBy(br => br.BranchId).ToList();
                return PartialView("_GetBranch", branchViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Role")
            {
                ViewBag.OPFor = "System";
                List<RoleViewModel> roleViewModels = _roleBusiness.GetAllRoles().Select(role => new RoleViewModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    OrganizationId = role.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(role.OrganizationId).OrganizationName),
                    EntryUser = UserForEachRecord(role.EUserId.Value).UserName,
                    UpdateUser = (role.UpUserId == null || role.UpUserId == 0) ? "" : UserForEachRecord(role.UpUserId.Value).UserName
                }).OrderBy(role => role.RoleId).ToList();
                return PartialView("_GetRole", roleViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "User")
            {
                ViewBag.OPFor = "System";
                var data = _appUserBusiness.GetAllAppUsers();
                IEnumerable<AppUserViewModel> appUserViewModels = data.Select(user => new AppUserViewModel
                {
                    UserId = user.UserId,
                    EmployeeId = user.EmployeeId,
                    FullName = user.FullName,
                    MobileNo = user.MobileNo,
                    Address = user.Address,
                    Email = user.Email,
                    Desigation = user.Desigation,
                    UserName = user.UserName,
                    Password = Utility.Decrypt(user.Password),
                    StateStatus = (user.IsActive == true ? "Active" : "Inactive"),
                    StateStatusRole = (user.IsRoleActive == true ? "Active" : "Inactive"),
                    OrganizationId = user.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(user.OrganizationId).OrganizationName),
                    BranchId = user.BranchId,
                    BranchName = (_branchBusiness.GetBranchOneByOrgId(user.BranchId, user.OrganizationId).BranchName),
                    RoleId = user.RoleId,
                    RoleName = (_roleBusiness.GetRoleOneById(user.RoleId, user.OrganizationId).RoleName),
                    EntryUser = UserForEachRecord(user.EUserId.Value).UserName,
                    UpdateUser = !user.UpUserId.HasValue ? "" : UserForEachRecord(user.UpUserId.Value).UserName
                }).OrderBy(user => user.UserId).ToList();
                return PartialView("_GetUsers", appUserViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "userMenus")
            {
                var userDto = _appUserBusiness.GetUserDetail(userId.Value, orgId.Value);
                UserDetaildViewModel userDetaildViewModel = new UserDetaildViewModel();
                AutoMapper.Mapper.Map(userDto, userDetaildViewModel);
                List<VmUserModule> listVmUserModule = GetAppMenus(userId.Value, orgId.Value);
                return Json(new { userDetail = userDetaildViewModel, menuDetail = listVmUserModule });
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "roleMenus")
            {
                List<VmUserModule> listVmUserModule = new List<VmUserModule>();
                IEnumerable<UserCustomMenusDTO> userCustomMenus = _roleAuthorizationBusiness.GetUserRoleMenus(roleId.Value, orgId.Value);
                if (userCustomMenus.Count() > 0)
                {
                    var modules = (from m in userCustomMenus
                                   select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                    foreach (var item in modules)
                    {
                        VmUserModule vmUserModule = new VmUserModule();
                        vmUserModule.ModuleId = item.MId;
                        vmUserModule.ModuleName = item.ModuleName;
                        List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                        var mainmenu = (from m in userCustomMenus
                                        where m.ModuleId == item.MId
                                        select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                        foreach (var mm in mainmenu)
                        {
                            VmUserMenu vmUserMenu = new VmUserMenu();
                            vmUserMenu.MenuId = mm.MenuId;
                            vmUserMenu.MenuName = mm.MenuName;
                            vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                            {
                                SubmenuId = s.SubmenuId,
                                SubMenuName = s.SubMenuName,
                                ParentSubMenuId = s.ParentSubMenuId,
                                Add = s.Add,
                                Edit = s.Edit,
                                Detail = s.Detail,
                                Delete = s.Delete,
                                Approval = s.Approval,
                                Report = s.Report,
                                TaskId = s.TaskId
                            }).ToList();
                            vmUserMenus.Add(vmUserMenu);
                        }

                        vmUserModule.Menus = vmUserMenus;
                        listVmUserModule.Add(vmUserModule);
                    }
                }
                return Json(new { menuDetail = listVmUserModule });
            }
            return Content("");
        }

        // For App Client
        public ActionResult ClientUserConfiguration(string flag, long? userId, long? roleId, long? orgId)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Branch")
            {
                IEnumerable<BranchViewModel> branchViewModels = _branchBusiness.GetBranchByOrgId(User.OrgId).Select(br => new BranchViewModel
                {
                    BranchId = br.BranchId,
                    BranchName = br.BranchName,
                    ShortName = br.ShortName,
                    MobileNo = br.MobileNo,
                    Address = br.Address,
                    Email = br.Email,
                    PhoneNo = br.PhoneNo,
                    Fax = br.Fax,
                    StateStatus = (br.IsActive == true ? "Active" : "Inactive"),
                    Remarks = br.Remarks,
                    OrgId = br.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(br.OrganizationId).OrganizationName),
                    EntryUser = UserForEachRecord(br.EUserId.Value).UserName,
                    UpdateUser = (br.UpUserId == null || br.UpUserId == 0) ? "" : UserForEachRecord(br.UpUserId.Value).UserName
                }).OrderBy(br => br.BranchId).ToList();
                return PartialView("_GetBranch", branchViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Role")
            {
                List<RoleViewModel> roleViewModels = _roleBusiness.GetAllRoleByOrgId(User.OrgId).Select(role => new RoleViewModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    OrganizationId = role.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(User.OrgId).OrganizationName),
                    EntryUser = UserForEachRecord(role.EUserId.Value).UserName,
                    UpdateUser = (role.UpUserId == null || role.UpUserId == 0) ? "" : UserForEachRecord(role.UpUserId.Value).UserName
                }).OrderBy(role => role.RoleId).ToList();
                ViewBag.OPFor = "Client";
                return PartialView("_GetRole", roleViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "User")
            {
                ViewBag.OPFor = "Client";
                var data = _appUserBusiness.GetAllAppUserByOrgId(User.OrgId);
                IEnumerable<AppUserViewModel> appUserViewModels = data.Select(user => new AppUserViewModel
                {
                    UserId = user.UserId,
                    EmployeeId = user.EmployeeId,
                    FullName = user.FullName,
                    MobileNo = user.MobileNo,
                    Address = user.Address,
                    Email = user.Email,
                    Desigation = user.Desigation,
                    UserName = user.UserName,
                    Password = Utility.Decrypt(user.Password),
                    StateStatus = (user.IsActive == true ? "Active" : "Inactive"),
                    StateStatusRole = (user.IsRoleActive == true ? "Active" : "Inactive"),
                    OrganizationId = user.OrganizationId,
                    OrganizationName = (_organizationBusiness.GetOrganizationById(user.OrganizationId).OrganizationName),
                    BranchId = user.BranchId,
                    BranchName = (_branchBusiness.GetBranchOneByOrgId(user.BranchId, user.OrganizationId).BranchName),
                    RoleId = user.RoleId,
                    RoleName = (_roleBusiness.GetRoleOneById(user.RoleId, user.OrganizationId).RoleName),
                    EntryUser = UserForEachRecord(user.EUserId.Value).UserName,
                    UpdateUser = (user.UpUserId == null || user.UpUserId == 0) ? "" : UserForEachRecord(user.UpUserId.Value).UserName
                }).OrderBy(user => user.UserId).ToList();
                return PartialView("_GetUsers", appUserViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "userMenus")
            {
                var userDto = _appUserBusiness.GetUserDetail(userId.Value, orgId.Value);
                UserDetaildViewModel userDetaildViewModel = new UserDetaildViewModel();
                AutoMapper.Mapper.Map(userDto, userDetaildViewModel);
                List<VmUserModule> listVmUserModule = GetAppMenus(userId.Value, orgId.Value);
                return Json(new { userDetail = userDetaildViewModel, menuDetail = listVmUserModule });
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "roleMenus")
            {
                List<VmUserModule> listVmUserModule = new List<VmUserModule>();
                IEnumerable<UserCustomMenusDTO> userCustomMenus = _roleAuthorizationBusiness.GetUserRoleMenus(roleId.Value, orgId.Value);
                if (userCustomMenus.Count() > 0)
                {
                    var modules = (from m in userCustomMenus
                                   select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                    foreach (var item in modules)
                    {
                        VmUserModule vmUserModule = new VmUserModule();
                        vmUserModule.ModuleId = item.MId;
                        vmUserModule.ModuleName = item.ModuleName;
                        List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                        var mainmenu = (from m in userCustomMenus
                                        where m.ModuleId == item.MId
                                        select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                        foreach (var mm in mainmenu)
                        {
                            VmUserMenu vmUserMenu = new VmUserMenu();
                            vmUserMenu.MenuId = mm.MenuId;
                            vmUserMenu.MenuName = mm.MenuName;
                            vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                            {
                                SubmenuId = s.SubmenuId,
                                SubMenuName = s.SubMenuName,
                                ParentSubMenuId = s.ParentSubMenuId,
                                Add = s.Add,
                                Edit = s.Edit,
                                Detail = s.Detail,
                                Delete = s.Delete,
                                Approval = s.Approval,
                                Report = s.Report,
                                TaskId = s.TaskId
                            }).ToList();
                            vmUserMenus.Add(vmUserMenu);
                        }

                        vmUserModule.Menus = vmUserMenus;
                        listVmUserModule.Add(vmUserModule);
                    }
                }
                return Json(new { menuDetail = listVmUserModule });
            }
            return Content("");
        }
        #endregion

        #region App Config
        public ActionResult ApplicationConfig(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlModuleName = _moduleBusiness.GetAllModules().Select(br => new SelectListItem { Text = br.ModuleName, Value = br.MId.ToString() });

                ViewBag.ddlMainMenu = _maniMenuBusiness.GetAllMainMenu().Select(br => new SelectListItem { Text = br.MenuName, Value = br.MMId.ToString() });

                ViewBag.ddlParentSubMenu = _subMenuBusiness.GetAllSubMenu().Where(s => s.IsActAsParent == true).Select(s => new SelectListItem
                {
                    Text = s.SubMenuName + ((s.MMId > 0) ? " (" + (_maniMenuBusiness.GetMainMenuOneById(s.MMId).MenuName) + ")" : ""),
                    Value = s.SubMenuId.ToString()
                });

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Module")
            {
                var data = _moduleBusiness.GetAllModules().Select(s => new ModuleViewModel()
                {
                    MId = s.MId,
                    ModuleName = s.ModuleName,
                    IconName = s.IconName,
                    IconColor = s.IconColor,
                    EntryUser = UserForEachRecord(s.EUserId.Value).UserName,
                    UpdateUser = (s.UpUserId == null || s.UpUserId == 0) ? "" : UserForEachRecord(s.UpUserId.Value).UserName
                }).ToList();
                return PartialView("_GetModules", data);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Mainmenu")
            {
                IEnumerable<MainMenuDTO> mainMenuDTO = _maniMenuBusiness.GetAllMainMenu().Select(mainmenu => new MainMenuDTO
                {
                    MMId = mainmenu.MMId,
                    MenuName = mainmenu.MenuName,
                    MId = mainmenu.MId,
                    ModuleName = (_moduleBusiness.GetModuleOneById(mainmenu.MId).ModuleName),
                    EntryUser = UserForEachRecord(mainmenu.EUserId.Value).UserName,
                    UpdateUser = (mainmenu.UpUserId == null || mainmenu.UpUserId == 0) ? "" : UserForEachRecord(mainmenu.UpUserId.Value).UserName
                }).ToList();
                IEnumerable<MainMenuViewModel> mainMenuViewModels = new List<MainMenuViewModel>();
                AutoMapper.Mapper.Map(mainMenuDTO, mainMenuViewModels);
                return PartialView("_GetMainmenu", mainMenuViewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Submenu")
            {
                IEnumerable<SubMenuDTO> subMenuDTO = _subMenuBusiness.GetAllSubMenu().Select(sub => new SubMenuDTO
                {
                    SubMenuId = sub.SubMenuId,
                    SubMenuName = sub.SubMenuName,
                    ControllerName = sub.ControllerName,
                    ActionName = sub.ActionName,
                    IconClass = sub.IconClass,
                    IsViewable = sub.IsViewable,
                    IsActAsParent = sub.IsActAsParent,
                    IsViewableStatus = (sub.IsViewable == true ? "Yes" : "No"),
                    IsActAsParentStatus = (sub.IsActAsParent == true ? "Yes" : "No"),
                    ParentSubMenuId = sub.ParentSubMenuId,
                    ParentSubmenuName = (sub.ParentSubMenuId > 0 ? _subMenuBusiness.GetSubMenuOneById(sub.ParentSubMenuId.Value).SubMenuName : ""),
                    MMId = sub.MMId,
                    MenuName = (_maniMenuBusiness.GetMainMenuOneById(sub.MMId).MenuName),
                    EntryUser = UserForEachRecord(sub.EUserId.Value).UserName,
                    UpdateUser = (sub.UpUserId == null || sub.UpUserId == 0) ? "" : UserForEachRecord(sub.UpUserId.Value).UserName
                }).ToList();
                IEnumerable<SubMenuViewModel> subMenuViewModels = new List<SubMenuViewModel>();
                AutoMapper.Mapper.Map(subMenuDTO, subMenuViewModels);
                return PartialView("_GetSubmenu", subMenuViewModels);
            }
            return Content("");
        }
        #endregion

        #region Role
        public ActionResult GetRoleList(int? page)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetRoleList");
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });

            List<RoleViewModel> roleViewModels = _roleBusiness.GetAllRoles().Select(role => new RoleViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                OrganizationId = role.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(role.OrganizationId).OrganizationName),
                EntryUser = UserForEachRecord(role.EUserId.Value).UserName,
                UpdateUser = (role.UpUserId == null || role.UpUserId == 0) ? "" : UserForEachRecord(role.UpUserId.Value).UserName
            }).OrderBy(role => role.RoleId).ToList();

            return View(roleViewModels);
        }
        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveRole(RoleViewModel roleViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetRoleList");
            //var permission = ((pre.Edit) || (pre.Add));
            if (ModelState.IsValid)
            {
                try
                {
                    RoleDTO dto = new RoleDTO();
                    AutoMapper.Mapper.Map(roleViewModel, dto);
                    isSuccess = _roleBusiness.SaveRole(dto, User.UserId, roleViewModel.OrganizationId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        public ActionResult CreateAppRole(long id = 0)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(s => new SelectListItem
            {
                Text = s.OrganizationName,
                Value = s.OrganizationId.ToString()
            }).ToList();
            ViewBag.Id = id;
            ViewBag.PageHead = id > 0 ? "Update User Role" : "Create User Role";
            ViewBag.RoleName = id > 0 ? _roleBusiness.GetRoleById(id).RoleName : "";
            ViewBag.OrgId = id > 0 ? _roleBusiness.GetRoleById(id).OrganizationId.ToString() : "";
            return View();

        }
        public ActionResult CreateUserRole(long id = 0)
        {
            ViewBag.Id = id;
            ViewBag.PageHead = id > 0 ? "Update User Role" : "Create User Role";
            ViewBag.RoleName = id > 0 ? _roleBusiness.GetRoleById(id).RoleName : "";
            ViewBag.OrgId = id > 0 ? _roleBusiness.GetRoleById(id).OrganizationId.ToString() : "";
            return View();
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveAppRole(RoleViewModel role, List<RoleAuthorizationViewModel> models)
        {
            bool isSuccess = false;
            if (ModelState.IsValid && models.Count > 0)
            {
                RoleDTO dto = new RoleDTO();
                AutoMapper.Mapper.Map(role, dto);
                var execution = _roleBusiness.SaveAppRole(dto, User.UserId, role.OrganizationId);
                if (execution.isSuccess)
                {
                    List<RoleAuthorizationDTO> roleAuthorizationDTOs = new List<RoleAuthorizationDTO>();
                    models.FirstOrDefault().RoleId = Convert.ToInt64(execution.text);
                    AutoMapper.Mapper.Map(models, roleAuthorizationDTOs);
                    isSuccess = _roleAuthorizationBusiness.SaveRoleAuthorization(roleAuthorizationDTOs, User.UserId, User.OrgId);
                }

            }
            return Json(isSuccess);
        }
        #endregion

        #region AppUser
        public ActionResult GetAppUserList()
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetAppUserList");
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });

            var data = _appUserBusiness.GetAllAppUsers();

            IEnumerable<AppUserViewModel> appUserViewModels = data.Select(user => new AppUserViewModel
            {
                UserId = user.UserId,
                EmployeeId = user.EmployeeId,
                FullName = user.FullName,
                MobileNo = user.MobileNo,
                Address = user.Address,
                Email = user.Email,
                Desigation = user.Desigation,
                UserName = user.UserName,
                Password = Utility.Decrypt(user.Password),
                StateStatus = (user.IsActive == true ? "Active" : "Inactive"),
                StateStatusRole = (user.IsRoleActive == true ? "Active" : "Inactive"),
                OrganizationId = user.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(user.OrganizationId).OrganizationName),
                BranchId = user.BranchId,
                BranchName = (_branchBusiness.GetBranchOneByOrgId(user.BranchId, user.OrganizationId).BranchName),
                RoleId = user.RoleId,
                RoleName = (_roleBusiness.GetRoleOneById(user.RoleId, user.OrganizationId).RoleName),
                EntryUser = UserForEachRecord(user.EUserId.Value).UserName,
                UpdateUser = (user.UpUserId == null || user.UpUserId == 0) ? "" : UserForEachRecord(user.UpUserId.Value).UserName
            }).OrderBy(user => user.UserId).ToList();
            return View(appUserViewModels);
        }

        public ActionResult CreateAppUser(string flag, long id = 0)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });
                ViewBag.Id = id;
                ViewBag.PageHead = id > 0 ? "Update Application User" : "Create Application User";
                return View();
            }
            else
            {
                var dto = _appUserBusiness.GetAppUserInfoById(id, User.OrgId, "System");
                if (dto != null)
                {
                    dto.Password = Utility.Decrypt(dto.Password);
                    dto.ConfirmPassword = dto.Password;
                }
                return Json(dto);
            }
        }

        public ActionResult CreateClientUser(string flag, long id = 0)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.Id = id;
                ViewBag.PageHead = id > 0 ? "Update User Info" : "Create User";
                return View();
            }
            else
            {
                var dto = _appUserBusiness.GetAppUserInfoById(id, User.OrgId, "Client");
                if (dto != null)
                {
                    dto.Password = Utility.Decrypt(dto.Password);
                    dto.ConfirmPassword = dto.Password;
                }
                return Json(dto);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveAppUser(AppUserViewModel appUserViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetAppUserList");
            //var permission = ((pre.Edit) || (pre.Add));
            if (ModelState.IsValid)
            {
                try
                {
                    AppUserDTO dto = new AppUserDTO();
                    appUserViewModel.Password = Utility.Encrypt(appUserViewModel.Password);
                    AutoMapper.Mapper.Map(appUserViewModel, dto);
                    isSuccess = _appUserBusiness.SaveAppUser(dto, User.UserId, User.OrgId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveApplicationUser(AppUserViewModel appUser, List<UserAuthorizationViewModel> models)
        {
            bool isSuccess = false;
            if (ModelState.IsValid && models.Count > 0)
            {
                AppUserDTO dto = new AppUserDTO();
                appUser.Password = Utility.Encrypt(appUser.Password);
                appUser.ConfirmPassword = appUser.Password;
                AutoMapper.Mapper.Map(appUser, dto);
                var appExecuation = _appUserBusiness.SaveAppUser2(dto, User.UserId, User.OrgId);
                if (appExecuation.isSuccess)
                {
                    List<UserAuthorizationDTO> userAuthorizationDTOs = new List<UserAuthorizationDTO>();
                    models.FirstOrDefault().UserId = Convert.ToInt64(appExecuation.text);
                    AutoMapper.Mapper.Map(models, userAuthorizationDTOs);
                    isSuccess = _userAuthorizationBusiness.SaveUserAuthorization(userAuthorizationDTOs, User.UserId, User.OrgId);
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region Module
        public ActionResult GetModuleList()
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetModuleList");
            IEnumerable<ModuleDTO> moduleDTO = _moduleBusiness.GetAllModules().Select(m => new ModuleDTO
            {
                MId = m.MId,
                ModuleName = m.ModuleName,
                IconName = m.IconName,
                IconColor = m.IconColor,
                EntryUser = UserForEachRecord(m.EUserId.Value).UserName,
                UpdateUser = (m.UpUserId == null || m.UpUserId == 0) ? "" : UserForEachRecord(m.UpUserId.Value).UserName
            }).ToList();
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            AutoMapper.Mapper.Map(moduleDTO, moduleViewModels);
            return View(moduleViewModels);
        }

        public ActionResult SaveModule(ModuleViewModel moduleViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetModuleList");
            //var permission = ((pre.Edit) || (pre.Add));
            // && permission
            if (ModelState.IsValid)
            {
                try
                {
                    ModuleDTO dto = new ModuleDTO();
                    AutoMapper.Mapper.Map(moduleViewModel, dto);
                    isSuccess = _moduleBusiness.SaveModule(dto, User.UserId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region MainMenu
        public ActionResult GetMainMenuList()
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetMainMenuList");
            ViewBag.ddlModuleName = _moduleBusiness.GetAllModules().Select(br => new SelectListItem { Text = br.ModuleName, Value = br.MId.ToString() });

            IEnumerable<MainMenuDTO> mainMenuDTO = _maniMenuBusiness.GetAllMainMenu().Select(mainmenu => new MainMenuDTO
            {
                MMId = mainmenu.MMId,
                MenuName = mainmenu.MenuName,
                MId = mainmenu.MId,
                ModuleName = (_moduleBusiness.GetModuleOneById(mainmenu.MId).ModuleName),
                EntryUser = UserForEachRecord(mainmenu.EUserId.Value).UserName,
                UpdateUser = (mainmenu.UpUserId == null || mainmenu.UpUserId == 0) ? "" : UserForEachRecord(mainmenu.UpUserId.Value).UserName
            }).ToList();
            IEnumerable<MainMenuViewModel> mainMenuViewModels = new List<MainMenuViewModel>();
            AutoMapper.Mapper.Map(mainMenuDTO, mainMenuViewModels);
            return View(mainMenuViewModels);
        }
        public ActionResult SaveMainMenu(MainMenuViewModel mainMenuViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetMainMenuList");
            //var permission = ((pre.Edit) || (pre.Add)); // && permission
            if (ModelState.IsValid)
            {
                try
                {
                    MainMenuDTO dto = new MainMenuDTO();
                    AutoMapper.Mapper.Map(mainMenuViewModel, dto);
                    isSuccess = _maniMenuBusiness.SaveMainMenu(dto, User.UserId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region SubMenu
        public ActionResult GetSubMenuList()
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetSubMenuList");
            ViewBag.ddlMainMenu = _maniMenuBusiness.GetAllMainMenu().Select(br => new SelectListItem { Text = br.MenuName, Value = br.MMId.ToString() });

            ViewBag.ddlParentSubMenu = _subMenuBusiness.GetAllSubMenu().Where(s => s.IsActAsParent == true).Select(s => new SelectListItem
            {

                Text = s.SubMenuName + ((s.MMId > 0) ? " (" + (_maniMenuBusiness.GetMainMenuOneById(s.MMId).MenuName) + ")" : ""),
                Value = s.SubMenuId.ToString()

            });

            IEnumerable<SubMenuDTO> subMenuDTO = _subMenuBusiness.GetAllSubMenu().Select(sub => new SubMenuDTO
            {
                SubMenuId = sub.SubMenuId,
                SubMenuName = sub.SubMenuName,
                ControllerName = sub.ControllerName,
                ActionName = sub.ActionName,
                IconClass = sub.IconClass,
                IsViewable = sub.IsViewable,
                IsActAsParent = sub.IsActAsParent,
                IsViewableStatus = (sub.IsViewable == true ? "Yes" : "No"),
                IsActAsParentStatus = (sub.IsActAsParent == true ? "Yes" : "No"),
                ParentSubMenuId = sub.ParentSubMenuId,
                ParentSubmenuName = (sub.ParentSubMenuId > 0 ? _subMenuBusiness.GetSubMenuOneById(sub.ParentSubMenuId.Value).SubMenuName : ""),
                MMId = sub.MMId,
                MenuName = (_maniMenuBusiness.GetMainMenuOneById(sub.MMId).MenuName),

                EntryUser = UserForEachRecord(sub.EUserId.Value).UserName,
                UpdateUser = (sub.UpUserId == null || sub.UpUserId == 0) ? "" : UserForEachRecord(sub.UpUserId.Value).UserName
            }).ToList();
            IEnumerable<SubMenuViewModel> subMenuViewModels = new List<SubMenuViewModel>();
            AutoMapper.Mapper.Map(subMenuDTO, subMenuViewModels);
            return View(subMenuViewModels);
        }
        public ActionResult SaveSubMenu(SubMenuViewModel subMenuViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetSubMenuList");
            //var permission = ((pre.Edit) || (pre.Add));
            // && permission
            if (ModelState.IsValid)
            {
                try
                {
                    SubMenuDTO dto = new SubMenuDTO();
                    AutoMapper.Mapper.Map(subMenuViewModel, dto);
                    isSuccess = _subMenuBusiness.SaveSubMenu(dto, User.UserId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region Orgainzation Auth
        public ActionResult GetMainMenusForOrgAuth(string flag, long? orgId)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetMainMenusForOrgAuth");
            if (string.IsNullOrEmpty(flag))
            {
                var allmainmenus = _organizationAuthBusiness.GetMainMenusForOrgAuth();
                List<ModuleUIViewModel> viewModels = new List<ModuleUIViewModel>();
                var modules = allmainmenus.Select(s => s.MId).Distinct().ToList();
                foreach (var item in modules)
                {
                    ModuleUIViewModel module = new ModuleUIViewModel();
                    var mainmenu = allmainmenus.Where(m => m.MId == item).ToList();
                    if (mainmenu.Count > 0)
                    {
                        module.MId = item;
                        module.ModuleName = mainmenu.FirstOrDefault().ModuleName;
                        List<MainMenuUIVIewModel> mainMenuUIVIews = new List<MainMenuUIVIewModel>();
                        foreach (var menu in mainmenu)
                        {
                            MainMenuUIVIewModel m = new MainMenuUIVIewModel();
                            m.MMId = menu.MMId;
                            m.MainmenuName = menu.MenuName;
                            mainMenuUIVIews.Add(m);
                        }
                        module.MainMenus = mainMenuUIVIews;
                    }
                    viewModels.Add(module);
                }

                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(s => new SelectListItem
                {
                    Text = s.OrganizationName,
                    Value = s.OrganizationId.ToString()
                }).ToList();
                return View(viewModels);
            }
            else
            {
                var allmainmenusByOrg = _organizationAuthBusiness.GetMainMenusForOrgAuthById(orgId.Value);
                return Json(allmainmenusByOrg);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveOrganizationAuthMenus(OrgAuthMenusViewModels viewModels)
        {
            bool IsSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetMainMenusForOrgAuth");
            //var permission = ((pre.Edit) || (pre.Add));
            //&& permission
            if (ModelState.IsValid && viewModels.Menus.Count > 0)
            {
                OrgAuthMenusDTO dto = new OrgAuthMenusDTO();
                AutoMapper.Mapper.Map(viewModels, dto);
                IsSuccess = this._organizationAuthBusiness.SaveOrganizationAuthMenus(dto, User.UserId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region User Custom Authorization
        public ActionResult SetUserCustomAuthorization(string flag, long? userId, long? orgId)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "SetUserCustomAuthorization");
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(o => new SelectListItem
                {
                    Text = o.OrganizationName,
                    Value = o.OrganizationId.ToString()
                }).ToList();
                return View();
            }
            else
            {

                var userDto = _appUserBusiness.GetUserDetail(userId.Value, orgId.Value);
                UserDetaildViewModel userDetaildViewModel = new UserDetaildViewModel();
                AutoMapper.Mapper.Map(userDto, userDetaildViewModel);
                List<VmUserModule> listVmUserModule = GetAppMenus(userId.Value, orgId.Value);
                return Json(new { userDetail = userDetaildViewModel, menuDetail = listVmUserModule });
            }
        }

        private List<VmUserModule> GetAppMenus(long? userId, long? orgId)
        {
            List<VmUserModule> listVmUserModule = new List<VmUserModule>();
            IEnumerable<UserCustomMenusDTO> userCustomMenus = _userAuthorizationBusiness.GetUserCustomMenus(userId.Value, orgId.Value);
            if (userCustomMenus.Count() > 0)
            {
                var modules = (from m in userCustomMenus
                               select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                foreach (var item in modules)
                {
                    VmUserModule vmUserModule = new VmUserModule();
                    vmUserModule.ModuleId = item.MId;
                    vmUserModule.ModuleName = item.ModuleName;
                    List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                    var mainmenu = (from m in userCustomMenus
                                    where m.ModuleId == item.MId
                                    select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                    foreach (var mm in mainmenu)
                    {
                        VmUserMenu vmUserMenu = new VmUserMenu();
                        vmUserMenu.MenuId = mm.MenuId;
                        vmUserMenu.MenuName = mm.MenuName;
                        vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                        {
                            SubmenuId = s.SubmenuId,
                            SubMenuName = s.SubMenuName,
                            ParentSubMenuId = s.ParentSubMenuId,
                            Add = s.Add,
                            Edit = s.Edit,
                            Detail = s.Detail,
                            Delete = s.Delete,
                            Approval = s.Approval,
                            Report = s.Report,
                            TaskId = s.TaskId
                        }).ToList();
                        vmUserMenus.Add(vmUserMenu);
                    }

                    vmUserModule.Menus = vmUserMenus;
                    listVmUserModule.Add(vmUserModule);
                }
            }

            return listVmUserModule;
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveUserAuthorization(List<UserAuthorizationViewModel> models)
        {
            bool IsSuccess = false;
            ///var pre = UserPrivilege("ControlPanel", "SetUserCustomAuthorization");
            //var permission = ((pre.Edit) || (pre.Add));
            //&& permission
            if (models.Count > 0 )
            {
                List<UserAuthorizationDTO> userAuthorizationDTOs = new List<UserAuthorizationDTO>();
                AutoMapper.Mapper.Map(models, userAuthorizationDTOs);
                IsSuccess = _userAuthorizationBusiness.SaveUserAuthorization(userAuthorizationDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region User Role Authorization
        public ActionResult SetUserRoleAuthorization(string flag, long? roleId, long? orgId)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "SetUserRoleAuthorization");
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(o => new SelectListItem
                {
                    Text = o.OrganizationName,
                    Value = o.OrganizationId.ToString()
                }).ToList();
                return View();
            }
            else
            {
                List<VmUserModule> listVmUserModule = new List<VmUserModule>();
                IEnumerable<UserCustomMenusDTO> userCustomMenus = _roleAuthorizationBusiness.GetUserRoleMenus(roleId.Value, orgId.Value);
                if (userCustomMenus.Count() > 0)
                {
                    var modules = (from m in userCustomMenus
                                   select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                    foreach (var item in modules)
                    {
                        VmUserModule vmUserModule = new VmUserModule();
                        vmUserModule.ModuleId = item.MId;
                        vmUserModule.ModuleName = item.ModuleName;
                        List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                        var mainmenu = (from m in userCustomMenus
                                        where m.ModuleId == item.MId
                                        select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                        foreach (var mm in mainmenu)
                        {
                            VmUserMenu vmUserMenu = new VmUserMenu();
                            vmUserMenu.MenuId = mm.MenuId;
                            vmUserMenu.MenuName = mm.MenuName;
                            vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                            {
                                SubmenuId = s.SubmenuId,
                                SubMenuName = s.SubMenuName,
                                ParentSubMenuId = s.ParentSubMenuId,
                                Add = s.Add,
                                Edit = s.Edit,
                                Detail = s.Detail,
                                Delete = s.Delete,
                                Approval = s.Approval,
                                Report = s.Report,
                                TaskId = s.TaskId
                            }).ToList();
                            vmUserMenus.Add(vmUserMenu);
                        }

                        vmUserModule.Menus = vmUserMenus;
                        listVmUserModule.Add(vmUserModule);
                    }
                }
                return Json(new { menuDetail = listVmUserModule });
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveRoleAuthorization(List<RoleAuthorizationViewModel> models)
        {
            bool IsSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "SetUserRoleAuthorization");
            //var permission = ((pre.Edit) || (pre.Add)); && permission
            if (models.Count > 0 )
            {
                List<RoleAuthorizationDTO> roleAuthorizationDTOs = new List<RoleAuthorizationDTO>();
                AutoMapper.Mapper.Map(models, roleAuthorizationDTOs);
                IsSuccess = _roleAuthorizationBusiness.SaveRoleAuthorization(roleAuthorizationDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Used By Client

        #region User
        public ActionResult GetClientUsers()
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetClientUsers");
            IEnumerable<AppUserDTO> appUserDto = _appUserBusiness.GetAllAppUserByOrgId(User.OrgId).Select(user => new AppUserDTO
            {
                UserId = user.UserId,
                EmployeeId = user.EmployeeId,
                FullName = user.FullName,
                MobileNo = user.MobileNo,
                Address = user.Address,
                Email = user.Email,
                Desigation = user.Desigation,
                UserName = user.UserName,
                Password = Utility.Decrypt(user.Password),
                StateStatus = (user.IsActive == true ? "Active" : "Inactive"),
                StateStatusRole = (user.IsRoleActive == true ? "Active" : "Inactive"),
                OrganizationId = user.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(User.OrgId).OrganizationName),
                BranchId = user.BranchId,
                BranchName = (_branchBusiness.GetBranchOneByOrgId(user.BranchId, User.OrgId).BranchName),
                RoleId = user.RoleId,
                RoleName = (_roleBusiness.GetRoleOneById(user.RoleId, User.OrgId).RoleName),
                EntryUser = UserForEachRecord(user.EUserId.Value).UserName,
                UpdateUser = (user.UpUserId == null || user.UpUserId == 0) ? "" : UserForEachRecord(user.UpUserId.Value).UserName

            }).OrderBy(user => user.UserId).ToList();
            IEnumerable<AppUserViewModel> appUserViewModels = new List<AppUserViewModel>();
            AutoMapper.Mapper.Map(appUserDto, appUserViewModels);
            return View(appUserViewModels);
        }
        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveClientUser(AppUserViewModel appUserViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetClientUsers");
            //var permission = ((pre.Edit) || (pre.Add));
            //&& permission
            if (ModelState.IsValid)
            {
                try
                {
                    AppUserDTO dto = new AppUserDTO();
                    appUserViewModel.OrganizationId = User.OrgId;
                    appUserViewModel.Password = Utility.Encrypt(appUserViewModel.Password);
                    AutoMapper.Mapper.Map(appUserViewModel, dto);
                    isSuccess = _appUserBusiness.SaveAppUser(dto, User.UserId, User.OrgId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region User Custom Permission
        public ActionResult SetUserCustomPermission(string flag, long? userId, long? orgId)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "SetUserCustomPermission");
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlUsers = _appUserBusiness.GetAllAppUserByOrgId(User.OrgId).Select(o => new SelectListItem
                {
                    Text = o.UserName,
                    Value = o.UserId.ToString()
                }).ToList();
                return View();
            }
            else
            {
                List<VmUserModule> listVmUserModule = new List<VmUserModule>();
                IEnumerable<UserCustomMenusDTO> userCustomMenus = _userAuthorizationBusiness.GetUserCustomMenus(userId.Value, orgId.Value);
                var userDto = _appUserBusiness.GetUserDetail(userId.Value, orgId.Value);
                UserDetaildViewModel userDetaildViewModel = new UserDetaildViewModel();
                AutoMapper.Mapper.Map(userDto, userDetaildViewModel);
                if (userCustomMenus.Count() > 0)
                {
                    var modules = (from m in userCustomMenus
                                   select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                    foreach (var item in modules)
                    {
                        VmUserModule vmUserModule = new VmUserModule();
                        vmUserModule.ModuleId = item.MId;
                        vmUserModule.ModuleName = item.ModuleName;
                        List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                        var mainmenu = (from m in userCustomMenus
                                        where m.ModuleId == item.MId
                                        select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                        foreach (var mm in mainmenu)
                        {
                            VmUserMenu vmUserMenu = new VmUserMenu();
                            vmUserMenu.MenuId = mm.MenuId;
                            vmUserMenu.MenuName = mm.MenuName;
                            vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                            {
                                SubmenuId = s.SubmenuId,
                                SubMenuName = s.SubMenuName,
                                ParentSubMenuId = s.ParentSubMenuId,
                                Add = s.Add,
                                Edit = s.Edit,
                                Detail = s.Detail,
                                Delete = s.Delete,
                                Approval = s.Approval,
                                Report = s.Report,
                                TaskId = s.TaskId
                            }).ToList();
                            vmUserMenus.Add(vmUserMenu);
                        }

                        vmUserModule.Menus = vmUserMenus;
                        listVmUserModule.Add(vmUserModule);
                    }
                }
                return Json(new { userDetail = userDetaildViewModel, menuDetail = listVmUserModule });
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveUserPermission(List<UserAuthorizationViewModel> models)
        {
            bool IsSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "SetUserCustomPermission");
            //var permission = ((pre.Edit) || (pre.Add));
            if (models.Count > 0)
            {
                List<UserAuthorizationDTO> userAuthorizationDTOs = new List<UserAuthorizationDTO>();
                AutoMapper.Mapper.Map(models, userAuthorizationDTOs);
                IsSuccess = _userAuthorizationBusiness.SaveUserAuthorization(userAuthorizationDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region User Role Permission
        public ActionResult SetUserRolePermission(string flag, long? roleId, long? orgId)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "SetUserRolePermission");
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlRoles = _roleBusiness.GetAllRoleByOrgId(User.OrgId).Select(o => new SelectListItem
                {
                    Text = o.RoleName,
                    Value = o.RoleId.ToString()
                }).ToList();
                return View();
            }
            else
            {
                List<VmUserModule> listVmUserModule = new List<VmUserModule>();
                IEnumerable<UserCustomMenusDTO> userCustomMenus = _roleAuthorizationBusiness.GetUserRoleMenus(roleId.Value, orgId.Value);
                if (userCustomMenus.Count() > 0)
                {
                    var modules = (from m in userCustomMenus
                                   select new { MId = m.ModuleId, ModuleName = m.ModuleName }).Distinct().ToList();
                    foreach (var item in modules)
                    {
                        VmUserModule vmUserModule = new VmUserModule();
                        vmUserModule.ModuleId = item.MId;
                        vmUserModule.ModuleName = item.ModuleName;
                        List<VmUserMenu> vmUserMenus = new List<VmUserMenu>();
                        var mainmenu = (from m in userCustomMenus
                                        where m.ModuleId == item.MId
                                        select new { MenuId = m.MainmenuId, MenuName = m.MainmenuName }).Distinct().ToList();
                        foreach (var mm in mainmenu)
                        {
                            VmUserMenu vmUserMenu = new VmUserMenu();
                            vmUserMenu.MenuId = mm.MenuId;
                            vmUserMenu.MenuName = mm.MenuName;
                            vmUserMenu.SubMenus = userCustomMenus.Where(s => s.MainmenuId == mm.MenuId).Select(s => new VmUserSubmenu
                            {
                                SubmenuId = s.SubmenuId,
                                SubMenuName = s.SubMenuName,
                                ParentSubMenuId = s.ParentSubMenuId,
                                Add = s.Add,
                                Edit = s.Edit,
                                Detail = s.Detail,
                                Delete = s.Delete,
                                Approval = s.Approval,
                                Report = s.Report,
                                TaskId = s.TaskId
                            }).ToList();
                            vmUserMenus.Add(vmUserMenu);
                        }

                        vmUserModule.Menus = vmUserMenus;
                        listVmUserModule.Add(vmUserModule);
                    }
                }
                return Json(new { menuDetail = listVmUserModule });
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveRolePermission(List<RoleAuthorizationViewModel> models)
        {
            bool IsSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "SetUserRolePermission");
            //var permission = ((pre.Edit) || (pre.Add));
            if (models.Count > 0)
            {
                List<RoleAuthorizationDTO> roleAuthorizationDTOs = new List<RoleAuthorizationDTO>();
                AutoMapper.Mapper.Map(models, roleAuthorizationDTOs);
                IsSuccess = _roleAuthorizationBusiness.SaveRoleAuthorization(roleAuthorizationDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region User Role
        public ActionResult GetUserRoleList(int? page)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetUserRoleList");
            IPagedList<RoleViewModel> roleViewModels = _roleBusiness.GetAllRoleByOrgId(User.OrgId).Select(role => new RoleViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                OrganizationId = role.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(role.OrganizationId).OrganizationName),
                EntryUser = UserForEachRecord(role.EUserId.Value).UserName,
                UpdateUser = (role.UpUserId == null || role.UpUserId == 0) ? "" : UserForEachRecord(role.UpUserId.Value).UserName

            }).OrderBy(role => role.RoleId).ToPagedList(page ?? 1, 15);
            IEnumerable<RoleViewModel> roleViewModelForPage = new List<RoleViewModel>();
            return View(roleViewModels);
        }

        public ActionResult SaveUserRole(RoleViewModel roleViewModel)
        {
            bool isSuccess = false;
            //var pre = UserPrivilege("ControlPanel", "GetUserRoleList");
            //var permission = ((pre.Edit) || (pre.Add));
            if (ModelState.IsValid)
            {
                try
                {
                    RoleDTO dto = new RoleDTO();
                    AutoMapper.Mapper.Map(roleViewModel, dto);
                    isSuccess = _roleBusiness.SaveRole(dto, User.UserId, User.OrgId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #region Branch
        public ActionResult GetBranchListForClient(int? page)
        {
            ViewBag.UserPrivilege = UserPrivilege("ControlPanel", "GetBranchListForClient");
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Select(br => new SelectListItem { Text = br.OrganizationName, Value = br.OrganizationId.ToString() });

            IPagedList<BranchViewModel> branchViewModels = _branchBusiness.GetBranchByOrgId(User.OrgId).Select(br => new BranchViewModel
            {
                BranchId = br.BranchId,
                BranchName = br.BranchName,
                ShortName = br.ShortName,
                MobileNo = br.MobileNo,
                Email = br.Email,
                PhoneNo = br.PhoneNo,
                Fax = br.Fax,
                StateStatus = (br.IsActive == true ? "Active" : "Inactive"),
                Remarks = br.Remarks,
                OrgId = br.OrganizationId,
                OrganizationName = (_organizationBusiness.GetOrganizationById(User.OrgId).OrganizationName),
                EntryUser = UserForEachRecord(br.EUserId.Value).UserName,
                UpdateUser = (br.UpUserId == null || br.UpUserId == 0) ? "" : UserForEachRecord(br.UpUserId.Value).UserName
            }).OrderBy(br => br.BranchId).ToPagedList(page ?? 1, 15);
            IEnumerable<BranchViewModel> branchViewModelForPage = new List<BranchViewModel>();
            return View(branchViewModels);
        }
        public ActionResult SaveBranchForClient(BranchViewModel branchViewModel)
        {
            //var pre = UserPrivilege("ControlPanel", "GetBranchListForClient");
            //var permission = ((pre.Edit) || (pre.Add));
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                try
                {
                    BranchDTO dto = new BranchDTO();
                    AutoMapper.Mapper.Map(branchViewModel, dto);
                    isSuccess = _branchBusiness.SaveBranch(dto, User.UserId, User.OrgId);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }
            }
            return Json(isSuccess);
        }
        #endregion

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}