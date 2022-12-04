using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ERPWeb.Filters
{
    public sealed class ValidateJsonAntiForgeryToken : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            AntiForgery.Validate(cookie != null ? cookie.Value : null,
                                 httpContext.Request.Headers["__RequestVerificationToken"]);
        }
    }
}