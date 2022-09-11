using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Caching;
using ERPWeb.Infrastructure;
using ERPBO.Common;
using ERPBLL.Common;
using ERPBO.ControlPanel.DomainModels;
using ERPBO.ControlPanel.ViewModels;

namespace ERPWeb.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UserDetail"] != null)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    CustomPrincipalSerializeModel User = (CustomPrincipalSerializeModel)HttpContext.Current.Session["UserDetail"];
                    string action = filterContext.RouteData.Values["action"].ToString();
                    string controller = filterContext.RouteData.Values["controller"].ToString();
                   
                    // Dashboard //
                    if ((action == "Index" && controller == "Admin") || (action == "Index" && controller == "User"))
                    {
                        if (action == "Index" && controller == "Admin")
                        {
                            if (User.RoleName == UserType.SystemAdmin)
                            {
                                //
                            }
                            else
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                        }
                        else 
                        {
                            if (User.RoleName == UserType.SystemAdmin)
                            {
                                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                            
                        }
                    }
                    
                    else if (controller == "Common" || controller == "Error" || controller == "Common2") //User.RoleName == UserType.SystemAdmin
                    {
                        // Nothing to do...
                    }
                    //else if (User.IsRoleActive == true)
                    //{
                    //    var submenuList = (List<SubMenu>)HttpContext.Current.Session["AllSubmenus"];
                    //    var submenu = submenuList.FirstOrDefault(sb => sb.ActionName == action && sb.ControllerName == controller);

                    //    if (submenu != null)
                    //    {
                    //        var roleMenu = (List<UserAuthorizeMenusViewModels>)HttpContext.Current.Session["UserAuthorizeMenus"];
                    //        var authSubmenu = roleMenu.FirstOrDefault(rm => rm.SubmenuId == submenu.SubMenuId);
                    //        if (authSubmenu == null)
                    //        {
                    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    //        }
                    //    }
                    //}
                    else
                    {
                        var submenuList = (List<SubMenu>)HttpContext.Current.Session["AllSubmenus"];
                        var submenu = submenuList.FirstOrDefault(sb => sb.ActionName == action && sb.ControllerName == controller);
                        if (submenu != null)
                        {
                            var userMenu = (List<UserAuthorizeMenusViewModels>)HttpContext.Current.Session["UserAuthorizeMenus"];
                            var authSubmenu = userMenu.FirstOrDefault(ua => ua.SubmenuId == submenu.SubMenuId);
                            if (authSubmenu == null)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Access", action = "LogIn" }));
            }

        }
    }
}