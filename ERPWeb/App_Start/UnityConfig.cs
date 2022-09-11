
using ERPBLL.ControlPanel;
using ERPBLL.ControlPanel.Interface;

using ERPDAL.ControlPanelDAL;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ERPWeb
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            // Inventory Database
   

            // Production Database
 
            // ControlPanel Database
            #region ControlPanel
            container.RegisterType<ISubMenuBusiness, SubMenuBusiness>();
            container.RegisterType<IManiMenuBusiness, ManiMenuBusiness>();
            container.RegisterType<IAppUserBusiness, AppUserBusiness>();
            container.RegisterType<IRoleBusiness, RoleBusiness>();
            container.RegisterType<IBranchBusiness, BranchBusiness>();
            container.RegisterType<IModuleBusiness, ModuleBusiness>();
            container.RegisterType<IOrganizationBusiness, OrganizationBusiness>();
            container.RegisterType<IModuleBusiness, ModuleBusiness>();
            container.RegisterType<IOrganizationAuthBusiness, OrganizationAuthBusiness>();
            container.RegisterType<IUserAuthorizationBusiness, UserAuthorizationBusiness>();
            container.RegisterType<IRoleAuthorizationBusiness, RoleAuthorizationBusiness>();
            container.RegisterType<IControlPanelUnitOfWork, ControlPanelUnitOfWork>();
            #endregion

            // Configuration Database
        
            // FrontDesk Database
         

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}