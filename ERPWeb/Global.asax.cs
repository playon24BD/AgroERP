using ERPBO.Common;
using ERPWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents(); // Dependency Injection
            AutoMapperWebProfile.Run(); // Mapping Models
        }

        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["UserDetail"] != null)
                {
                    CustomPrincipalSerializeModel model = (CustomPrincipalSerializeModel)Session["UserDetail"];
                    CustomPrincipal newUser = new CustomPrincipal(model.UserName)
                    {
                        UserId = model.UserId,
                        UserName = model.UserName,
                        FullName = model.FullName,
                        Address = model.Address,
                        Email = model.Email,
                        Designation = model.Designation,
                        EmployeeId = model.EmployeeId,
                        MobileNo = model.MobileNo,
                        LogInTime = model.LogInTime,
                        MacID = model.MacID,
                        IsRoleActive = model.IsRoleActive,
                        HeaderLogo = model.HeaderLogo,
                        IsOrgActive = model.IsOrgActive,
                        IsUserActive = model.IsUserActive,
                        LogoPaths = model.LogoPaths,
                        OrgId = model.OrgId,
                        OrgLogo = model.OrgLogo,
                        OrgName = model.OrgName,
                        RoleId = model.RoleId,
                        RoleName = model.RoleName,
                        roles = model.roles,
                        BranchId = model.BranchId,
                        BranchName = model.BranchName,
                        AppType = model.AppType,
                        ZoneId= model.ZoneId,
                        ZoneName = model.ZoneName,
                        DistrictId = model.DistrictId,
                        DistrictName = model.DistrictName,
                        DivisionId = model.DivisionId,
                        DivisionName = model.DivisionName
                    };                    
                    HttpContext.Current.User = newUser;
                }
            }
        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
