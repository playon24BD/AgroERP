
using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : BaseController
    {
        private readonly IDepotSetup _depotSetup;
        private readonly ERPBLL.ControlPanel.Interface.IOrganizationBusiness _organizationBusiness;

        public AgroConfigurationController(ERPBLL.ControlPanel.Interface.IOrganizationBusiness organizationBusiness,IDepotSetup depotSetup)
        {
            this._organizationBusiness = organizationBusiness;
            this._depotSetup = depotSetup;
        }
        // GET: AgroConfiguration
        public ActionResult DepotList(string flag)
        {
            
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "DepotSetup")
            {
                IEnumerable<DepotSetupDTO> dto = _depotSetup.GetAllDepotSetup(User.OrgId).Select(o => new DepotSetupDTO
                {
                    DepotId = o.DepotId,
                    OrganizationId = o.OrganizationId,
                    DepotName = o.DepotName,
                    Status = (o.Status = true ? "Active" : "Inactive"),
                    RoleId=o.RoleId,
                   EntryUserId=o.EntryUserId,
                   EntryDate=o.EntryDate,
                   UpdateUserId=o.UpdateUserId,
                   UpdateDate=o.UpdateDate
                }).ToList();

                List<DepotSetupViewModel> viewModel = new List<DepotSetupViewModel>();
                AutoMapper.Mapper.Map(dto,viewModel);
                return PartialView("_GetDepotPartialView", viewModel);



            }
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult SaveDepotInfo(DepotSetupViewModel viewModel)
        {
            bool isSuccess = false;
           
            if (ModelState.IsValid)
            {
                DepotSetupDTO dto = new DepotSetupDTO();
                AutoMapper.Mapper.Map(viewModel, dto);
                //isSuccess = _depotSetup.GetAllDepotSetup(dto);
            }
            return Json(isSuccess);

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