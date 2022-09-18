
using ERPBLL.ControlPanel.Interface;
using ERPBLL.ControlPanel;

using ERPDAL.ControlPanelDAL;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ERPBLL.Agriculture;
using ERPBLL.Agriculture.Interface;

using ERPDAL.AgricultureDAL;


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
            #region Agriculture
            container.RegisterType<IRawMaterialStockInfo, RawMaterialStockInfoBusiness>();
            container.RegisterType<IRawMaterialStockDetail, RawMaterialStockDetailBusiness>();
            container.RegisterType<IRawMaterialSupplier, RawMaterialSupplierBusiness>();
            container.RegisterType<IBankSetup, BankSetupBusiness>();
            container.RegisterType<IDepotSetup, DepotSetupBusiness>();
            container.RegisterType<IRawMaterialBusiness,RawMaterialBusiness>();
            container.RegisterType<IFinishGoodProductBusiness, FinishGoodProductBusiness>();
            container.RegisterType<IFinishGoodProductSupplierBusiness, FinishGoodProductSupplierBusiness>();
            container.RegisterType<IAgricultureUnitOfWork, AgricultureUnitOfWork>();
            container.RegisterType<IMeasuremenBusiness, MeasuremenBusiness>();
            container.RegisterType<IFinishGoodRecipeInfoBusiness, FinishGoodRecipeInfoBusiness>();
            container.RegisterType<IFinishGoodRecipeDetailsBusiness, FinishGoodRecipeDetailsBusiness>();
            #endregion
            // FrontDesk Database


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}