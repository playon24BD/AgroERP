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
            return View();
        }
    }
}