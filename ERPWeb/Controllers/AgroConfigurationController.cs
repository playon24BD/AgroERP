using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : Controller
    {
        // GET: AgroConfiguration
        public ActionResult DepotList()
        {
            //ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(des => new SelectListItem { Text = des.OrganizationName, Value = des.OrganizationId.ToString() }).ToList();


            return View();
        }
    }
}