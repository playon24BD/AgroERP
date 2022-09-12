using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
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

        public ActionResult GetRawMaterial(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                return View();
            }
            return PartialView ("_GetRawMaterial");
        }
        public ActionResult SaveRawMaterial(RawMaterialViewModel model )
        {

            RawMaterialDTO rawMaterialDTO = new RawMaterialDTO();
            bool isSuccess = false;
            if (ModelState.IsValid)
            {

            }
            return Json(isSuccess);
        }


    }
}