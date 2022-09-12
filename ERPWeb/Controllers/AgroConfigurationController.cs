
﻿
using ERPBLL.Agriculture.Interface;



using ERPBLL.ControlPanel;
using ERPBLL.ControlPanel.Interface;

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
        private readonly IRawMaterialBusiness _rawMaterialBusiness;
        private readonly IDepotSetup _depotSetup;
        private readonly IOrganizationBusiness _organizationBusiness;

        public AgroConfigurationController(ERPBLL.ControlPanel.Interface.IOrganizationBusiness organizationBusiness,IDepotSetup depotSetup, IRawMaterialBusiness rawMaterialBusiness)
        {
            this._organizationBusiness = organizationBusiness;
            this._depotSetup = depotSetup;
            this._rawMaterialBusiness = rawMaterialBusiness;
        }
        // GET: AgroConfiguration

        //public ActionResult DepotList(string flag)
        //{

        //    if (string.IsNullOrEmpty(flag))
        //    {
        //        ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
        //        return View();
        //    }

        //    else (!string.IsNullOrEmpty(flag) && flag == "DepotSetup")
        //    {
        //        IEnumerable<DepotSetupDTO> dto = _depotSetup.GetAllDepotSetup(User.OrgId).Select(o => new DepotSetupDTO
        //        {
        //            DepotId = o.DepotId,
        //            OrganizationId = o.OrganizationId,
        //            DepotName = o.DepotName,
        //            Status = (o.Status = true ? "Active" : "Inactive"),
        //            RoleId = o.RoleId,
        //            EntryUserId = o.EntryUserId,
        //            EntryDate = o.EntryDate,
        //            UpdateUserId = o.UpdateUserId,
        //            UpdateDate = o.UpdateDate
        //        }).ToList();

        //        List<DepotSetupViewModel> viewModel = new List<DepotSetupViewModel>();
        //        AutoMapper.Mapper.Map(dto, viewModel);
        //        return PartialView("_GetDepotPartialView", viewModel);

        //    }
        //}
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
                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Select(des => new SelectListItem { Text = des.OrganizationName, Value = des.OrganizationId.ToString() }).ToList();

                return View();
            }
            IEnumerable<RawMaterialDTO> dto = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new RawMaterialDTO() {
                OrganizationName = _organizationBusiness.GetOrganizationById(a.OrganizationId).OrganizationName,
                DepotId = a.DepotId,
                RawMaterialName = a.RawMaterialName,
                ExpireDate = a.ExpireDate,


            });
            List<RawMaterialViewModel> viewModels=new List<RawMaterialViewModel>();
            AutoMapper.Mapper.Map(dto,viewModels);

            return PartialView ("_GetRawMaterial", viewModels);
        }
        public ActionResult SaveRawMaterial(RawMaterialViewModel model )
        {

            RawMaterialDTO rawMaterialDTO = new RawMaterialDTO();
            AutoMapper.Mapper.Map(model,rawMaterialDTO);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _rawMaterialBusiness.SaveRawMaterial(rawMaterialDTO,User.UserId,User.OrgId);
            }
            return Json(isSuccess);
        }


    }
}