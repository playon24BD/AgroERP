
﻿
using ERPBLL.Agriculture.Interface;
using ERPBLL.ControlPanel;

using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : BaseController
    {
        private readonly IBankSetup _bankSetup;
        private readonly IRawMaterialBusiness _rawMaterialBusiness;
        private readonly IDepotSetup _depotSetup;
        private readonly IFinishGoodProductBusiness _finishGoodProductBusiness;
        private readonly IFinishGoodProductSupplierBusiness _finishGoodProductSupplierBusiness;
        private readonly IOrganizationBusiness _organizationBusiness;

        public AgroConfigurationController(ERPBLL.ControlPanel.Interface.IOrganizationBusiness organizationBusiness, IDepotSetup depotSetup, IRawMaterialBusiness rawMaterialBusiness, IFinishGoodProductBusiness finishGoodProductBusiness,IBankSetup bankSetup, IFinishGoodProductSupplierBusiness finishGoodProductSupplierBusiness)
        {
            this._bankSetup = bankSetup;
            this._organizationBusiness = organizationBusiness;
            this._depotSetup = depotSetup;
            this._rawMaterialBusiness = rawMaterialBusiness;
            this._finishGoodProductBusiness = finishGoodProductBusiness;
            this._finishGoodProductSupplierBusiness = finishGoodProductSupplierBusiness;
        }
        // GET: AgroConfiguration


        #region AgroConfiguration

        #region Depot Setup
        public ActionResult DepotList(string flag, string name)
        {

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "DepotSetup")
            {
                IEnumerable<DepotSetupDTO> dto = _depotSetup.GetAllDepotSetup(User.OrgId).Where(s => (name == "" || name == null) || (s.DepotName.Contains(name))).Select(o => new DepotSetupDTO
                {
                    DepotId = o.DepotId,
                    OrganizationId = o.OrganizationId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    DepotName = o.DepotName,
                    Status = o.Status ,
                    RoleId = o.RoleId,
                    //EntryUserId=o.EntryUserId.ToString(),
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUserId,
                    UpdateDate = o.UpdateDate
                }).ToList();

                List<DepotSetupViewModel> viewModel = new List<DepotSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetDepotPartialView", viewModel);



            }
            return View();
        }
        [HttpPost]
        public ActionResult SaveDepotInfo(DepotSetupViewModel viewModel)
        {
            bool isSuccess = false;
            DepotSetupDTO dto = new DepotSetupDTO();
            AutoMapper.Mapper.Map(viewModel, dto);
            isSuccess = _depotSetup.SaveDepotInfo(dto, User.OrgId, User.UserId);


            return Json(isSuccess);

        }
        #endregion

        #region Raw Material Setup
        public ActionResult GetRawMaterial(string flag, long? depotId, string rawmaterialName)
        {
            if (string.IsNullOrEmpty(flag))
            {
   
                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(des => new SelectListItem { Text = des.OrganizationName, Value = des.OrganizationId.ToString() }).ToList();
                ViewBag.ddlDepotName = _depotSetup.GetAllDepotSetup(User.OrgId).Select(a => new SelectListItem { Text = a.DepotName, Value = a.DepotId.ToString() });
                ViewBag.ddlRawMaterial = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialName, Value = a.RawMaterialName });

                return View();
            }

            else if (flag == "Search" && rawmaterialName != "" && depotId != 0)
            {



                var dto = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new RawMaterialDTO()
                {
                    OrganizationName = _organizationBusiness.GetOrganizationById(a.OrganizationId).OrganizationName,
                    DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,
                    RawMaterialName = a.RawMaterialName,
                    ExpireDate = a.ExpireDate,
                    DepotId = a.DepotId,
                    RawMaterialId=a.RawMaterialId,
                    OrganizationId=a.OrganizationId,
                    


                }).Where(a => (a.DepotId == depotId && a.RawMaterialName == rawmaterialName) && (a.DepotId == depotId || a.RawMaterialName == rawmaterialName)).ToList();

                //dto.Where(a =>a.DepotId == depotId).ToList();
                //dto.Where(a => (a.DepotId == depotId || a.DepotId == 0) && (a.RawMaterialName == rawmaterialName || a.RawMaterialName == "")).ToList();
                List<RawMaterialViewModel> viewModels = new List<RawMaterialViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);

                return PartialView("_GetRawMaterial", viewModels);
            }
            else if (flag == "Search" && (rawmaterialName != "" || depotId != 0))
            {
                //(rawmaterialName != "" || depotId != 0) || (rawmaterialName != "" && depotId != 0)

                
                var dto = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new RawMaterialDTO()
                {
                    OrganizationName = _organizationBusiness.GetOrganizationById(a.OrganizationId).OrganizationName,
                    DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,
                    RawMaterialName = a.RawMaterialName,
                    ExpireDate = a.ExpireDate,
                    DepotId = a.DepotId,
                    RawMaterialId=a.RawMaterialId,
                         OrganizationId = a.OrganizationId,


                }).Where(a => a.DepotId == depotId || a.RawMaterialName == rawmaterialName).ToList();

                //dto.Where(a =>a.DepotId == depotId).ToList();
                //dto.Where(a => (a.DepotId == depotId || a.DepotId == 0) && (a.RawMaterialName == rawmaterialName || a.RawMaterialName == "")).ToList();
                List<RawMaterialViewModel> viewModels = new List<RawMaterialViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);

                return PartialView("_GetRawMaterial", viewModels);

            }
            else
            {
                IEnumerable<RawMaterialDTO> dto1 = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new RawMaterialDTO()
                {
                    OrganizationName = _organizationBusiness.GetOrganizationById(a.OrganizationId).OrganizationName,
                    DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,
                    RawMaterialName = a.RawMaterialName,
                    ExpireDate = a.ExpireDate,
                    DepotId = a.DepotId,
                    RawMaterialId = a.RawMaterialId,
                         OrganizationId = a.OrganizationId,


                }).ToList();

                List<RawMaterialViewModel> viewModels1 = new List<RawMaterialViewModel>();
                AutoMapper.Mapper.Map(dto1, viewModels1);

                return PartialView("_GetRawMaterial", viewModels1);


            }

        }
        public ActionResult SaveRawMaterial(RawMaterialViewModel model)
        {

            RawMaterialDTO rawMaterialDTO = new RawMaterialDTO();
            AutoMapper.Mapper.Map(model, rawMaterialDTO);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _rawMaterialBusiness.SaveRawMaterial(rawMaterialDTO, User.UserId, User.OrgId);
            }
            return Json(isSuccess);
        }
        #endregion

        #region Raw Material Supplier Setup
        public ActionResult GetRawMaterialSupplier(string flag)
        {
            return null;
        }
        #endregion

        #region Finish Good Product Setup
        public ActionResult GetFinishGoodProduct(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Search")
            {
                IEnumerable<FinishGoodProductDTO> dto = _finishGoodProductBusiness.GetAllProductInfo(User.OrgId).Where(s => (name == "" || name == null) || (s.FinishGoodProductName.Contains(name))).Select(o => new FinishGoodProductDTO
                {
                    FinishGoodProductId = o.FinishGoodProductId,
                    OrganizationId = o.OrganizationId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    FinishGoodProductName = o.FinishGoodProductName,
                    Status = o.Status,
                    RoleId = o.RoleId,
                    //EntryUserId=o.EntryUserId.ToString(),
                    UserName = UserForEachRecord(o.EntryUser.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUser,
                    UpdateDate = o.UpdateDate
                }).ToList();

                List<FinishGoodProductViewModel> viewModel = new List<FinishGoodProductViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetFinishGoodProductPartialView", viewModel);



            }
            return View();
        }
        public ActionResult SaveFinishGoodProduct(FinishGoodProductViewModel model)
        {

            FinishGoodProductDTO finishGoodProduct = new FinishGoodProductDTO();
            AutoMapper.Mapper.Map(model, finishGoodProduct);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _finishGoodProductBusiness.SaveFinishGoodProductName(finishGoodProduct, User.UserId, User.OrgId);
            }
            return Json(isSuccess);
        }
        #endregion

        #region Finish Good Product Supplier
        public ActionResult GetFinishGoodProductSupplierList(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Search")
            {
                IEnumerable<FinishGoodSupplierDTO> dto = _finishGoodProductSupplierBusiness.GetAllProductSupplierInfo(User.OrgId).Where(s => (name == "" || name == null) || (s.FinishGoodSupplierName.Contains(name))|| (s.MobileNumber.ToString().Contains(name))).Select(o => new FinishGoodSupplierDTO
                {
                    FinishGoodSupplierId = o.FinishGoodSupplierId,
                    OrganizationId = o.OrganizationId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    FinishGoodSupplierName = o.FinishGoodSupplierName,
                    MobileNumber = o.MobileNumber,
                    Address = o.Address,
                    Status = o.Status,
                    RoleId = o.RoleId,
                    //EntryUserId=o.EntryUserId.ToString(),
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUserId,
                    UpdateDate = o.UpdateDate
                }).ToList();

                List<FinishGoodSupplierViewModel> viewModel = new List<FinishGoodSupplierViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetFinishGoodProductSupplierPartialView", viewModel);



            }
            return View();

            //return View();
        }

        public ActionResult SaveFinishGoodProductSupplier(FinishGoodSupplierViewModel model)
        {

            FinishGoodSupplierDTO finishGoodProductSupplier = new FinishGoodSupplierDTO();
            AutoMapper.Mapper.Map(model, finishGoodProductSupplier);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _finishGoodProductSupplierBusiness.SaveFinishGoodProductSupplierName(finishGoodProductSupplier, User.UserId, User.OrgId);
            }
            return Json(isSuccess);
        }
        #endregion

        #region Measurement Setup
        public ActionResult GetMeasurementList(string flag, string name)
        {

            return View();
        }
        #endregion

        #region Bank Setup
        public ActionResult GetBankList(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if(!string.IsNullOrEmpty(flag) && flag == "BankSetup")
            {
                IEnumerable<BankSetupDTO> dto = _bankSetup.GetAllBankSetup(User.OrgId).Where(s => (name == "" || name == null) || (s.BankName.Contains(name)|| s.AccountNumber.Contains(name))).Select(o => new BankSetupDTO
                {
                    BankId=o.BankId,
                    OrganizationId=o.OrganizationId,
                    Status=o.Status,
                    RoleId=o.RoleId,
                    BankName=o.BankName,
                    EntryDate=o.EntryDate,
                    UpdateDate=o.UpdateDate,
                    UpdateUserId=o.UpdateUserId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    AccountNumber=o.AccountNumber,
                    MobileNumber=o.MobileNumber,
                    Email=o.Email

                }).ToList();
                List<BankSetupViewModel> viewModel = new List<BankSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetBankPartialView", viewModel);
            }

            return View();
        }

        public ActionResult SaveBankInfo(BankSetupViewModel viewModel)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                BankSetupDTO dto = new BankSetupDTO();
                AutoMapper.Mapper.Map(viewModel, dto);
                isSuccess = _bankSetup.SaveBankInfo(dto, User.UserId, User.OrgId);

            }

            return Json(isSuccess);
        }

        #endregion

        
        #endregion


    }
}