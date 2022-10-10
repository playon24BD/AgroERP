using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
using ERPBO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : BaseController
    {
        public readonly IDivisionInfo _divisionInfo;
        public readonly IZone _zone;
        private readonly IZoneDetail _zoneDetail;
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        private readonly IRawMaterialStockDetail _rawMaterialStockDetail;
        private readonly IRawMaterialIssueStockInfoBusiness _rawMaterialIssueStockInfoBusiness;
        private readonly IRawMaterialIssueStockDetailsBusiness _rawMaterialIssueStockDetailsBusiness;

        private readonly IZoneSetup _zoneSetup;//e
        private readonly IRegionSetup _regionSetup;//e
        private readonly IAreaSetupBusiness _areaSetupBusiness;//e

        private readonly IBankSetup _bankSetup;
        private readonly IRawMaterialBusiness _rawMaterialBusiness;
        private readonly IDepotSetup _depotSetup;
        private readonly IFinishGoodProductBusiness _finishGoodProductBusiness;
        private readonly IFinishGoodProductSupplierBusiness _finishGoodProductSupplierBusiness;
        private readonly IOrganizationBusiness _organizationBusiness;
        private readonly IMeasuremenBusiness _measuremenBusiness;

        private readonly IRawMaterialSupplier _rawMaterialSupplierBusiness;
        private readonly IFinishGoodRecipeInfoBusiness _finishGoodRecipeInfoBusiness;
        private readonly IFinishGoodRecipeDetailsBusiness _finishGoodRecipeDetailsBusiness;
        private readonly IFinishGoodProductionInfoBusiness _finishGoodProductionInfoBusiness;
        private readonly IFinishGoodProductionDetailsBusiness _finishGoodProductionDetailsBusiness;


        public AgroConfigurationController(IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IZoneDetail zoneDetail, IZone zone, IOrganizationBusiness organizationBusiness, IDepotSetup depotSetup, IRawMaterialBusiness rawMaterialBusiness, IFinishGoodProductBusiness finishGoodProductBusiness, IBankSetup bankSetup, IFinishGoodProductSupplierBusiness finishGoodProductSupplierBusiness, IMeasuremenBusiness measuremenBusiness, IRawMaterialSupplier rawMaterialSupplierBusiness, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IRawMaterialStockDetail rawMaterialStockDetail, IRawMaterialIssueStockInfoBusiness rawMaterialIssueStockInfoBusiness, IRawMaterialIssueStockDetailsBusiness rawMaterialIssueStockDetailsBusiness, IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IFinishGoodProductionInfoBusiness finishGoodProductionInfoBusiness)
        {
            this._zoneSetup = zoneSetup;//e
            this._regionSetup = regionSetup;//e
            this._divisionInfo = divisionInfo;
            this._areaSetupBusiness = areaSetupBusiness;
            this._zoneDetail = zoneDetail;
            this._zone = zone;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._rawMaterialStockDetail = rawMaterialStockDetail;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._bankSetup = bankSetup;
            this._organizationBusiness = organizationBusiness;
            this._depotSetup = depotSetup;
            this._rawMaterialBusiness = rawMaterialBusiness;
            this._finishGoodProductBusiness = finishGoodProductBusiness;
            this._finishGoodProductSupplierBusiness = finishGoodProductSupplierBusiness;
            this._measuremenBusiness = measuremenBusiness;
            this._rawMaterialSupplierBusiness = rawMaterialSupplierBusiness;
            this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
            this._finishGoodRecipeDetailsBusiness = finishGoodRecipeDetailsBusiness;
            this._finishGoodProductionInfoBusiness = finishGoodProductionInfoBusiness;
            this._finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
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
                    Status = o.Status,
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
                    //ExpireDate = a.ExpireDate,
                    DepotId = a.DepotId,
                    RawMaterialId = a.RawMaterialId,
                    OrganizationId = a.OrganizationId,



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
                    //ExpireDate = a.ExpireDate,
                    DepotId = a.DepotId,
                    RawMaterialId = a.RawMaterialId,
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
                    //ExpireDate = a.ExpireDate,
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
        public ActionResult GetRawMaterialSupplier(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Search")
            {
                IEnumerable<RawMaterialSupplierDTO> dto = _rawMaterialSupplierBusiness.GetAllRawMaterialSupplierInfo(User.OrgId).Where(s => (name == "" || name == null) || (s.RawMaterialSupplierName.Contains(name)) || (s.MobileNumber.ToString().Contains(name))).Select(o => new RawMaterialSupplierDTO
                {
                    RawMaterialSupplierId = o.RawMaterialSupplierId,
                    OrganizationId = o.OrganizationId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    RawMaterialSupplierName = o.RawMaterialSupplierName,
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

                List<RawMaterialSupplierViewModel> viewModel = new List<RawMaterialSupplierViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetRawMaterialSupplierPartialView", viewModel);



            }
            return View();
        }
        public ActionResult SaveRawMaterialSupplier(RawMaterialSupplierViewModel model)
        {

            RawMaterialSupplierDTO rawMaterialSupplier = new RawMaterialSupplierDTO();
            AutoMapper.Mapper.Map(model, rawMaterialSupplier);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _rawMaterialSupplierBusiness.SaveRawMaterialSupplierName(rawMaterialSupplier, User.UserId, User.OrgId);
            }
            return Json(isSuccess);
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
                IEnumerable<FinishGoodSupplierDTO> dto = _finishGoodProductSupplierBusiness.GetAllProductSupplierInfo(User.OrgId).Where(s => (name == "" || name == null) || (s.FinishGoodSupplierName.Contains(name)) || (s.MobileNumber.ToString().Contains(name))).Select(o => new FinishGoodSupplierDTO
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
        public ActionResult GetMeasurementList(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                //ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == User.OrgId).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
                return View();
            }
            else
            {
                var measureMent = _measuremenBusiness.GetMeasurementSetups(User.OrgId);
                List<MeasurementSetupViewModel> viewModels = new List<MeasurementSetupViewModel>();
                AutoMapper.Mapper.Map(measureMent, viewModels);


                return PartialView("_GetMeasurementList", viewModels);
            }
            //return View();

        }
        public ActionResult SaveMeasurement(List<MeasurementSetupViewModel> models)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                List<MeasurementSetupDTO> measurementSetupDTOs = new List<MeasurementSetupDTO>();
                AutoMapper.Mapper.Map(models, measurementSetupDTOs);
                IsSuccess = _measuremenBusiness.SaveMeasureMent(measurementSetupDTOs, User.OrgId);


            }


            return Json(IsSuccess);
        }


        public ActionResult UpdateMeasurement(MeasurementSetupViewModel models)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                MeasurementSetupDTO measurementSetupDTO = new MeasurementSetupDTO();


                AutoMapper.Mapper.Map(models, measurementSetupDTO);
                IsSuccess = _measuremenBusiness.UpdateMeasureMent(measurementSetupDTO, User.UserId, User.OrgId);


            }


            return Json(IsSuccess);
        }




        #endregion

        #region Bank Setup
        public ActionResult GetBankList(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "BankSetup")
            {
                IEnumerable<BankSetupDTO> dto = _bankSetup.GetAllBankSetup(User.OrgId).Where(s => (name == "" || name == null) || (s.BankName.Contains(name)) || (s.AccountNumber.Contains(name)) || (s.MobileNumber.Contains(name))).Select(o => new BankSetupDTO
                {
                    BankId = o.BankId,
                    OrganizationId = o.OrganizationId,
                    Status = o.Status,
                    RoleId = o.RoleId,
                    BankName = o.BankName,
                    EntryDate = o.EntryDate,
                    UpdateDate = o.UpdateDate,
                    UpdateUserId = o.UpdateUserId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    AccountNumber = o.AccountNumber,
                    MobileNumber = o.MobileNumber,
                    Email = o.Email

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

        #region Depot/Warehouse
        public ActionResult GetRawMaterialStock(string flag, long? rawMaterialId, long? id, string Status)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetRawMaterialStock");

            if (string.IsNullOrEmpty(flag))
            {

                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _rawMaterialStockInfo.GetRawMaterialStockInfosList(User.OrgId, rawMaterialId ?? 0);


                List<RawMaterialStockInfoViewModel> viewModels = new List<RawMaterialStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialCurrentStockList", viewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                var dto = _rawMaterialStockInfo.GetRawMaterialStockInfos(User.OrgId, rawMaterialId ?? 0, Status ?? null);


                List<RawMaterialStockInfoViewModel> viewModels = new List<RawMaterialStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialStockList", viewModels);
                //var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();

                //var info = _rawMaterialStockInfo.GetRawMaterialStockById(id.Value, User.OrgId);
                //List<RawMaterialStockDetailViewModel> details = new List<RawMaterialStockDetailViewModel>();

                //if (info != null)
                //{
                //    ViewBag.Info = new RawMaterialStockInfoViewModel
                //    {
                //        RawMaterialName = RawMaterialNames.FirstOrDefault(it => it.RawMaterialId == info.RawMaterialId).RawMaterialName,

                //        Quantity = info.Quantity,
                //        Unit = info.Unit
                //    };

                //    details =
                //        _rawMaterialStockDetail.GetRawMaterialStockDetailsById(id.Value, User.OrgId).Select(i => new RawMaterialStockDetailViewModel
                //        {
                //            //RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == rawMaterialId).RawMaterialName,
                //            RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                //            Quantity = i.Quantity,
                //            Unit = i.Unit,
                //            Status=i.Status,
                //        }).ToList();
                //}
                //else
                //{
                //    ViewBag.Info = new RawMaterialStockInfoViewModel();
                //}
                //return PartialView("_GetRawMaterialStockDetail", details);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Edit)
            {
                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();

                var info = _rawMaterialStockInfo.GetRawMaterialStockById(id.Value, User.OrgId);
                List<RawMaterialStockDetailViewModel> details = new List<RawMaterialStockDetailViewModel>();

                if (info != null)
                {
                    ViewBag.Info = new RawMaterialStockInfoViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(it => it.RawMaterialId == info.RawMaterialId).RawMaterialName,

                        Quantity = info.Quantity,
                        Unit = info.Unit
                    };

                    details =
                        _rawMaterialStockDetail.GetRawMaterialStockDetailsById(id.Value, User.OrgId).Select(i => new RawMaterialStockDetailViewModel
                        {
                            //RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == rawMaterialId).RawMaterialName,
                            RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                            Quantity = i.Quantity,
                            Unit = i.Unit
                        }).ToList();
                }
                else
                {
                    ViewBag.Info = new RawMaterialStockInfoViewModel();
                }
                return PartialView("_GetRawMaterialStockEdit", details);
            }

            else
            {
                if (!string.IsNullOrEmpty(flag) && flag == Flag.Delete)
                {
                    bool IsSuccess = false;
                    if (id != null && id > 0)
                    {
                        IsSuccess = _rawMaterialStockInfo.DeleteRawMaterialStock(id.Value, User.UserId, User.OrgId);
                    }
                    return Json(IsSuccess);
                }
            }
            return View();
        }

        public ActionResult CreateRawMaterialStock(long? id)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlRawMaterialSupplierName = _rawMaterialSupplierBusiness.GetAllRawMaterialSupplierInfo(User.OrgId).Select(suplier => new SelectListItem { Text = suplier.RawMaterialSupplierName, Value = suplier.RawMaterialSupplierId.ToString() }).ToList();


            return View();
        }

        [HttpPost]
        public ActionResult SaveRawMaterialStock(RawMaterialStockInfoViewModel info, List<RawMaterialStockDetailViewModel> details)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid && details.Count > 0)
            {
                RawMaterialStockInfoDTO infoDTO = new RawMaterialStockInfoDTO();
                List<RawMaterialStockDetailDTO> detailDTOs = new List<RawMaterialStockDetailDTO>();
                AutoMapper.Mapper.Map(info, infoDTO);
                AutoMapper.Mapper.Map(details, detailDTOs);
                IsSuccess = _rawMaterialStockInfo.SaveRawMaterialStock(infoDTO, detailDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }

        public ActionResult GetFinishGoodRecipeList(string flag, long? ProductId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetFinishGoodRecipeList");

            if (string.IsNullOrEmpty(flag))
            {

                ViewBag.ddlProductName = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfos(User.OrgId, ProductId ?? 0);


                List<FinishGoodRecipeInfoViewModel> viewModels = new List<FinishGoodRecipeInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetFinishGoodRecipeList", viewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                var ProductNames = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).ToList();
                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();
                var info = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfoOneByOrgId(id.Value, User.OrgId);
                List<FinishGoodRecipeDetailsViewModel> details = new List<FinishGoodRecipeDetailsViewModel>();
                if (info != null)
                {


                    ViewBag.Info = new FinishGoodRecipeInfoViewModel
                    {
                        FinishGoodProductName = ProductNames.FirstOrDefault(it => it.FinishGoodProductId == info.FinishGoodProductId).FinishGoodProductName,

                        FGRQty = info.FGRQty,
                        FGRUnit = info.FGRUnit
                    };
                    details = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(id.Value, User.OrgId).Select(i => new FinishGoodRecipeDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        FGRRawMaterQty = i.FGRRawMaterQty,
                        FGRRawMaterUnit = i.FGRRawMaterUnit
                    }).ToList();
                }
                else
                {
                    ViewBag.Info = new FinishGoodRecipeInfoViewModel();
                }
                return PartialView("_GetFinishGoodRecipeDetail", details);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Edit)
            {
                var ProductNames = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).ToList();
                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();
                var info = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfoOneByOrgId(id.Value, User.OrgId);
                List<FinishGoodRecipeDetailsViewModel> details = new List<FinishGoodRecipeDetailsViewModel>();
                if (info != null)
                {


                    ViewBag.Info = new FinishGoodRecipeInfoViewModel
                    {
                        FinishGoodProductName = ProductNames.FirstOrDefault(it => it.FinishGoodProductId == info.FinishGoodProductId).FinishGoodProductName,
                        FGRId = info.FGRId,
                        FGRQty = info.FGRQty,
                        FGRUnit = info.FGRUnit,
                        FinishGoodProductId = info.FinishGoodProductId,
                        Status = info.Status,
                    };
                    details = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(id.Value, User.OrgId).Select(i => new FinishGoodRecipeDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        FGRRawMaterQty = i.FGRRawMaterQty,
                        FGRRawMaterUnit = i.FGRRawMaterUnit,
                        FGRDetailsId = i.FGRDetailsId,

                    }).ToList();
                }
                else
                {
                    ViewBag.Info = new FinishGoodRecipeInfoViewModel();
                }
                return PartialView("_GetFinishGoodRecipeEdit", details);
            }
            else
            {
                if (!string.IsNullOrEmpty(flag) && flag == Flag.Delete)
                {
                    bool IsSuccess = false;
                    if (id != null && id > 0)
                    {
                        IsSuccess = _finishGoodRecipeInfoBusiness.DeletefinishGoodRecipe(id.Value, User.UserId, User.OrgId);
                    }
                    return Json(IsSuccess);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateFinishGoodRecipe(long? id)
        {
            ViewBag.ddlProductName = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();
            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).Select(r => new SelectListItem { Text = r.RawMaterialName, Value = r.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlProductunit = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = UnitType.Kg,Value= UnitType.Kg },
                new SelectListItem(){ Text = UnitType.Litter,Value= UnitType.Litter },
                new SelectListItem(){ Text = UnitType.meter,Value= UnitType.meter }
            }.ToList();
            ViewBag.ddlRawMaterialunit = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = UnitType.Kg,Value= UnitType.Kg },
                new SelectListItem(){ Text = UnitType.Litter,Value= UnitType.Litter },
                new SelectListItem(){ Text = UnitType.meter,Value= UnitType.meter }
            }.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SaveFinishGoodRecipe(FinishGoodRecipeInfoViewModel info, List<FinishGoodRecipeDetailsViewModel> details)
        {
            bool IsSuccess = false;
            //var pre = UserPrivilege("Inventory", "GetItemPreparation");
            //var permission = ((pre.Edit) || (pre.Add));
            if (ModelState.IsValid)
            {
                FinishGoodRecipeInfoDTO infoDTO = new FinishGoodRecipeInfoDTO();
                List<FinishGoodRecipeDetailsDTO> detailDTOs = new List<FinishGoodRecipeDetailsDTO>();
                AutoMapper.Mapper.Map(info, infoDTO);
                AutoMapper.Mapper.Map(details, detailDTOs);
                IsSuccess = _finishGoodRecipeInfoBusiness.SaveFinishGoodRecipe(infoDTO, detailDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }

        #endregion

        #region Production
        public ActionResult GetRawMaterialIssueStock(string flag, long? rawMaterialId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetRawMaterialIssueStock");

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlRawMaterialName = _rawMaterialStockInfo.GetDepotRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();


                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockInfos(User.OrgId, rawMaterialId ?? 0);


                List<RawMaterialIssueStockInfoViewModel> viewModels = new List<RawMaterialIssueStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialIssueStockList", viewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();

                var info = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockById(id.Value, User.OrgId);
                List<RawMaterialIssueStockDetailsViewModel> details = new List<RawMaterialIssueStockDetailsViewModel>();

                if (info != null)
                {
                    ViewBag.Info = new RawMaterialIssueStockInfoViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(it => it.RawMaterialId == info.RawMaterialId).RawMaterialName,

                        Quantity = info.Quantity,
                        Unit = info.Unit
                    };

                    details =
                        _rawMaterialIssueStockDetailsBusiness.GetRawMaterialIssueStockDetailsById(id.Value, User.OrgId).Select(i => new RawMaterialIssueStockDetailsViewModel
                        {
                            //RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == rawMaterialId).RawMaterialName,
                            RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                            Status = i.Status,
                            EntryDate = i.EntryDate


                        }).ToList();
                }
                else
                {
                    ViewBag.Info = new RawMaterialIssueStockInfoViewModel();
                }
                return PartialView("_GetRawMaterialIssueStockDetail", details);
            }

            else
            {
                if (!string.IsNullOrEmpty(flag) && flag == Flag.Delete)
                {
                    bool IsSuccess = false;
                    if (id != null && id > 0)
                    {
                        IsSuccess = _rawMaterialIssueStockInfoBusiness.DeleteRawMaterialIssueStock(id.Value, User.UserId, User.OrgId);
                    }
                    return Json(IsSuccess);
                }
            }

            return View();
        }

        public ActionResult CreateRawMaterialIssueStock(long? id)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            //ViewBag.ddlProductName = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialStockInfo.GetCheckExpairDatewiseRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            return View();
        }
        public ActionResult GetRawMaterialIssueStockLoadUnit(long RawMaterialId)
        {
            var Unit = _rawMaterialStockInfo.GetRawMaterialIssueStockUnitById(RawMaterialId, User.OrgId).Unit;
            //var Unit = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockUnitById(RawMaterialId, User.OrgId).Unit;
            //var AllUnit = Regex.Replace(Unit, @"(\[|""|\])", "");
            //string AllUnit = Unit.Replace("[", "")
            //   .Replace("", )
            //   .Replace("]", "");

            return Json(Unit, JsonRequestBehavior.AllowGet);

            //, JsonRequestBehavior.AllowGet

        }

        [HttpPost]
        public ActionResult SaveProductionIssueRawMaterialStock(RawMaterialIssueStockInfoViewModel info, List<RawMaterialIssueStockDetailsViewModel> details)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid && details.Count > 0)
            {
                RawMaterialIssueStockInfoDTO infoDTO = new RawMaterialIssueStockInfoDTO();
                List<RawMaterialIssueStockDetailsDTO> detailDTOs = new List<RawMaterialIssueStockDetailsDTO>();
                AutoMapper.Mapper.Map(info, infoDTO);
                AutoMapper.Mapper.Map(details, detailDTOs);
                IsSuccess = _rawMaterialIssueStockInfoBusiness.SaveProductIssueRawMaterialStock(infoDTO, detailDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        [HttpPost]
        public ActionResult GetRawmaterialstockCheck(long RawMaterialId)
        {
            var checkRawMaterialStockValue = _rawMaterialStockInfo.GetCheckRawmeterislQuantity(RawMaterialId, User.OrgId);

            double itemStock = 0;
            string unit = "";
            if (checkRawMaterialStockValue != null)
            {
                itemStock = (checkRawMaterialStockValue.Quantity);
                unit = (checkRawMaterialStockValue.Unit);
            }
            return Json(new { RawMaterialStockQty = itemStock, RawMaterialStockUnit = unit });


        }

        public ActionResult GetProductFinishGoodList(string flag, string finishGoodProductionBatch, long? productId)
        {


            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlReceipBatchCode = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceif(User.OrgId).Where(fg => fg.ReceipeBatchCode != null).Select(f => new SelectListItem { Text = f.ReceipeBatchCode, Value = f.ReceipeBatchCode }).ToList();

                ViewBag.ddlProduct = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceif(User.OrgId).Select(d => new SelectListItem { Text = _finishGoodProductBusiness.GetFinishGoodProductById(d.FinishGoodProductId, User.OrgId).FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();


                return View();

            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Detail" && finishGoodProductionBatch != null)
            {
                IEnumerable<FinishGoodProductionDetailsDTO> dto = _finishGoodProductionDetailsBusiness.GetFinishGoodProductionDetails(finishGoodProductionBatch, User.OrgId).Select(a => new FinishGoodProductionDetailsDTO
                {
                    RawMaterialId = a.RawMaterialId,
                    RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(a.RawMaterialId, User.OrgId).RawMaterialName,
                    RequiredQuantity = a.RequiredQuantity,
                    Status = a.Status,
                    EntryDate = a.EntryDate

                }).ToList();
                List<FinishGoodProductionDetailsDTO> finishGoodProductionDetailsDTO = new List<FinishGoodProductionDetailsDTO>();
                List<FinishGoodProductionDetailViewModel> finishGoodProductionDetailViewModels = new List<FinishGoodProductionDetailViewModel>();
                AutoMapper.Mapper.Map(dto, finishGoodProductionDetailViewModels);

                return PartialView("_GetProductFinishGoodDetails", finishGoodProductionDetailViewModels);
            }
            else
            {

                IEnumerable<FinishGoodProductionInfoDTO> finishGoodProduction = _finishGoodProductionInfoBusiness.GetFinishGoodProductionInfo(User.OrgId).Select(f => new FinishGoodProductionInfoDTO
                {
                    FinishGoodProductionInfoId = f.FinishGoodProductInfoId,
                    FinishGoodProductionBatch = f.FinishGoodProductionBatch,
                    TargetQuantity = f.TargetQuantity,
                    Quanity = f.Quanity,
                    Status = f.Status,
                    ReceipeBatchCode = f.ReceipeBatchCode,
                    FinishGoodProductId = f.FinishGoodProductId,
                    FinishGoodProductName = _finishGoodProductBusiness.GetFinishGoodProductById(f.FinishGoodProductId, User.OrgId).FinishGoodProductName

                }).ToList();

                List<FinishGoodProductionInfoViewModel> viewModel = new List<FinishGoodProductionInfoViewModel>();
                AutoMapper.Mapper.Map(finishGoodProduction, viewModel);
                return PartialView("_GetProductFinishGoodList", viewModel);
            }


        }

        public ActionResult GetReceipyByProductionId(string receipeBatchCode)
        {
            try
            {

                int quantity = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfoOneByBatchCode(receipeBatchCode, User.OrgId).FGRQty;
                return Json(quantity, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetReceipyTergetQty(string receipeBatchCode)
        {
            try
            {

                //double IssueMinQty = 0;
                double issueQunatitys = 0;
                double perRecepQuantitys = 0;
                string myList = "";

                double SpliteRawMaterialIdList = 0;
                double SpliteRawMaterialIdList1 = 0;
                double SpliteRawMaterialIdList2 = 0;
                double SpliteRawMaterialIdList3 = 0;
                double SpliteRawMaterialIdList4 = 0;

                double SpliteRawMaterialMinValue1 = 0;
                double SpliteRawMaterialMinValue2 = 0;
                double SpliteRawMaterialMinValue3 = 0;
                double SpliteRawMaterialMinValue4 = 0;

                double SpliteRawMaterialMinValuefinal1 = 0;
                double SpliteRawMaterialMinValuefinal2 = 0;
                double SpliteRawMaterialMinValuefinal3 = 0;
                double SpliteRawMaterialMinValuefinalvalue1 = 0;
                double SpliteRawMaterialMinValuefinalvalue2 = 0;
                double SpliteRawMaterialMinValuefinal = 0;
                double SpliteRawMaterialRoundValue = 0;



                var recipeDetailsRawMaterials = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByBatchCode(receipeBatchCode, User.OrgId);
                foreach (var rawMaterial in recipeDetailsRawMaterials)
                {

                    issueQunatitys = _rawMaterialIssueStockInfoBusiness.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).Quantity;
                    // myList += string.Format("{0},", rawMaterial.RawMaterialId);


                    perRecepQuantitys = rawMaterial.FGRRawMaterQty;//0.3
                    double PerDividetQty = (issueQunatitys / perRecepQuantitys);//6/0.3=20
                    myList += string.Format("{0},", PerDividetQty);


                }
                string RawMaterialIdList = myList.TrimEnd(',');
                var a = recipeDetailsRawMaterials.Count();

                switch (a)
                {
                    case 1:
                        SpliteRawMaterialIdList = Convert.ToDouble(RawMaterialIdList.Split(',')[0]);
                        SpliteRawMaterialMinValuefinal = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList);
                        SpliteRawMaterialRoundValue = Math.Floor(SpliteRawMaterialMinValuefinal);
                        return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);
                    //break;
                    case 2:
                        SpliteRawMaterialIdList = Convert.ToDouble(RawMaterialIdList.Split(',')[0]);
                        SpliteRawMaterialIdList1 = Convert.ToDouble(RawMaterialIdList.Split(',')[1]);
                        SpliteRawMaterialMinValuefinal = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList1);
                        SpliteRawMaterialRoundValue = Math.Floor(SpliteRawMaterialMinValuefinal);
                        return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);
                    //break;
                    case 3:
                        SpliteRawMaterialIdList = Convert.ToDouble(RawMaterialIdList.Split(',')[0]);
                        SpliteRawMaterialIdList1 = Convert.ToDouble(RawMaterialIdList.Split(',')[1]);
                        SpliteRawMaterialIdList2 = Convert.ToDouble(RawMaterialIdList.Split(',')[2]);
                        SpliteRawMaterialMinValue1 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList1);
                        SpliteRawMaterialMinValue2 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList2);

                        SpliteRawMaterialMinValuefinal = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue2);
                        SpliteRawMaterialRoundValue = Math.Floor(SpliteRawMaterialMinValuefinal);
                        //break;
                        return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);

                    case 4:
                        SpliteRawMaterialIdList = Convert.ToDouble(RawMaterialIdList.Split(',')[0]);
                        SpliteRawMaterialIdList1 = Convert.ToDouble(RawMaterialIdList.Split(',')[1]);
                        SpliteRawMaterialIdList2 = Convert.ToDouble(RawMaterialIdList.Split(',')[2]);
                        SpliteRawMaterialIdList3 = Convert.ToDouble(RawMaterialIdList.Split(',')[3]);
                        SpliteRawMaterialMinValue1 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList1);
                        SpliteRawMaterialMinValue2 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList2);
                        SpliteRawMaterialMinValue3 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList3);

                        SpliteRawMaterialMinValuefinal1 = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue2);
                        SpliteRawMaterialMinValuefinal2 = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue3);

                        SpliteRawMaterialMinValuefinal = Math.Min(SpliteRawMaterialMinValuefinal1, SpliteRawMaterialMinValuefinal2);
                        SpliteRawMaterialRoundValue = Math.Floor(SpliteRawMaterialMinValuefinal);
                        //break;
                        return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);

                    case 5:
                        SpliteRawMaterialIdList = Convert.ToDouble(RawMaterialIdList.Split(',')[0]);
                        SpliteRawMaterialIdList1 = Convert.ToDouble(RawMaterialIdList.Split(',')[1]);
                        SpliteRawMaterialIdList2 = Convert.ToDouble(RawMaterialIdList.Split(',')[2]);
                        SpliteRawMaterialIdList3 = Convert.ToDouble(RawMaterialIdList.Split(',')[3]);
                        SpliteRawMaterialIdList4 = Convert.ToDouble(RawMaterialIdList.Split(',')[4]);
                        SpliteRawMaterialMinValue1 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList1);
                        SpliteRawMaterialMinValue2 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList2);
                        SpliteRawMaterialMinValue3 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList3);
                        SpliteRawMaterialMinValue4 = Math.Min(SpliteRawMaterialIdList, SpliteRawMaterialIdList4);

                        SpliteRawMaterialMinValuefinal1 = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue2);
                        SpliteRawMaterialMinValuefinal2 = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue3);
                        SpliteRawMaterialMinValuefinal3 = Math.Min(SpliteRawMaterialMinValue1, SpliteRawMaterialMinValue4);

                        SpliteRawMaterialMinValuefinalvalue1 = Math.Min(SpliteRawMaterialMinValuefinal1, SpliteRawMaterialMinValuefinal2);
                        SpliteRawMaterialMinValuefinalvalue2 = Math.Min(SpliteRawMaterialMinValuefinal1, SpliteRawMaterialMinValuefinal3);

                        SpliteRawMaterialMinValuefinal = Math.Min(SpliteRawMaterialMinValuefinalvalue1, SpliteRawMaterialMinValuefinalvalue2);
                        SpliteRawMaterialRoundValue = Math.Floor(SpliteRawMaterialMinValuefinal);

                        //break;
                        return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);

                }

                return Json(SpliteRawMaterialRoundValue, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetReceipyCodeByProductionId(long finishGoodProductId)
        {
            try
            {
                //var receipeBatchCode = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfoOneByOrgId(finishGoodProductId, User.OrgId).ReceipeBatchCode;
                //var ddlRecipeCode = receipeBatchCode.Select(d => new Dropdown { text=receipeBatchCode,value=d });
                var receipeBatchCode = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceipCode(finishGoodProductId, User.OrgId);

                var dropDown = receipeBatchCode.Where(a => a.ReceipeBatchCode != null).Select(s => new Dropdown { text = s.ReceipeBatchCode, value = s.ReceipeBatchCode }).ToList();


                return Json(dropDown, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Not Found", JsonRequestBehavior.AllowGet);
            }


        }


        //Not Need at this time
        public ActionResult GetReceipyDetailsByProductionId(long finishGoodProductId)
        {
            try
            {
                long fgid = _finishGoodRecipeInfoBusiness.GetFinishGoodRecipeInfoOneByFGID(finishGoodProductId, User.OrgId).FGRId;

                var AllMetarial = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(fgid, User.OrgId).Select(m => new FinishGoodRecipeDetailsDTO()
                {
                    FGRDetailsId = m.FGRDetailsId,
                    RawMaterialId = m.RawMaterialId,
                    RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(m.RawMaterialId, User.OrgId).RawMaterialName,
                    FGRId = m.FGRId,
                    FGRRawMaterQty = m.FGRRawMaterQty,


                }).ToList();
                List<FinishGoodRecipeDetailsViewModel> viewModels = new List<FinishGoodRecipeDetailsViewModel>();
                AutoMapper.Mapper.Map(AllMetarial, viewModels);
                return Json(viewModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json($"Not Found: {ex}", JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetReceipyDetailsByreceipeBatchCode(string receipeBatchCode, int targetQty)
        {
            double issueQunatity = 0;
            double perRecepQuantity = 0;
            double requirdQuantity = 0;
            bool Checked = false;

            try
            {

                var recipeDetailsRawMaterials = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByBatchCode(receipeBatchCode, User.OrgId);
                foreach (var rawMaterial in recipeDetailsRawMaterials)
                {

                    issueQunatity = _rawMaterialIssueStockInfoBusiness.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).Quantity;
                    perRecepQuantity = rawMaterial.FGRRawMaterQty;
                    requirdQuantity = (perRecepQuantity * targetQty);
                    if (requirdQuantity > issueQunatity)
                    {

                        Checked = false;
                    }
                    else
                    {
                        Checked = true;

                    }

                }

                if (Checked)
                {
                    var AllMetarial = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByBatchCode(receipeBatchCode, User.OrgId).Select(m => new FinishGoodRecipeDetailsDTO()
                    {
                        FGRDetailsId = m.FGRDetailsId,
                        RawMaterialId = m.RawMaterialId,
                        RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(m.RawMaterialId, User.OrgId).RawMaterialName,
                        FGRId = m.FGRId,
                        FGRRawMaterQty = m.FGRRawMaterQty,


                    }).ToList();




                    List<FinishGoodRecipeDetailsViewModel> viewModels = new List<FinishGoodRecipeDetailsViewModel>();
                    AutoMapper.Mapper.Map(AllMetarial, viewModels);
                    return Json(viewModels, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Issue Quantity Not Sufficient Please Check Target Quantity...", JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json($"Not Found: {ex}", JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveFinishGoodProduction(FinishGoodProductionInfoViewModel info, List<FinishGoodProductionDetailViewModel> details)
        {
            bool isSucccess = false;
            if (ModelState.IsValid && details.Count() > 0)
            {
                //AbNormal
                FinishGoodProductionInfoDTO finishGoodProductionInfoDTO = new FinishGoodProductionInfoDTO();
                List<FinishGoodProductionDetailsDTO> finishGoodProductionDetailsDTOs = new List<FinishGoodProductionDetailsDTO>();
                AutoMapper.Mapper.Map(info, finishGoodProductionInfoDTO);
                AutoMapper.Mapper.Map(details, finishGoodProductionDetailsDTOs);
                isSucccess = _finishGoodProductionInfoBusiness.SaveFinishGoodInfo(finishGoodProductionInfoDTO, finishGoodProductionDetailsDTOs, User.UserId, User.OrgId);
            }
            return Json(isSucccess);
        }



        #endregion

        #region Sales and Distribution

        public ActionResult GetZoneInfo(string flag, long? ZoneId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetZoneInfo");

            if (string.IsNullOrEmpty(flag))
            {

                ViewBag.ddlZoneName = _zone.GetAllZoneInfo(User.OrgId).Select(des => new SelectListItem { Text = des.ZoneName, Value = des.ZoneId.ToString() }).ToList();

                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _zone.GetZoneInfos(User.OrgId, ZoneId ?? 0);


                List<ZoneViewModel> viewModels = new List<ZoneViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetZonePartialViewList", viewModels);

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                var ZoneNames = _zone.GetAllZoneInfo(User.OrgId).ToList();

            }

            return View();

        }
        [HttpPost]
        public ActionResult SaveZoneInfo(ZoneViewModel viewModel, List<ZoneDetailViewModel> details)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {

                ZoneDTO dto = new ZoneDTO();

                List<ZoneDetailDTO> DetailsDTO = new List<ZoneDetailDTO>();
                AutoMapper.Mapper.Map(details, DetailsDTO);
                AutoMapper.Mapper.Map(viewModel, dto);
                isSuccess = _zone.SaveZoneInfo(dto, DetailsDTO, User.UserId, User.OrgId);

            }

            return Json(isSuccess);
        }
        //Zone
        public ActionResult Zonelist(string flag, string name)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "Zonelist");

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                IEnumerable<ZoneSetupDTO> dto = _zoneSetup.GetAllZoneSetup(User.OrgId).Where(s => (name == "" || name == null) || (s.ZoneName.Contains(name))).Select(o => new ZoneSetupDTO
                {
                    ZoneId = o.ZoneId,
                    OrganizationId = o.OrganizationId,
                    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                    ZoneName = o.ZoneName,
                    ZoneAsignName = o.ZoneAsignName,
                    MobileNumber = o.MobileNumber,
                    Remarks = o.Remarks,

                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUserId,
                    UpdateDate = o.UpdateDate,


                }).ToList();

                List<ZoneSetupViewModel> viewModels = new List<ZoneSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetZonePartialView", viewModels);
            }
            return View();
        }
        //createget
        public ActionResult CreateZonelist(long? id)
        {
            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveZonetInfo(List<ZoneSetupViewModel> details)
        {
            bool IsSuccess = false;
            if (details.Count > 0)
            {
                List<ZoneSetupDTO> detailsDTO = new List<ZoneSetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _zoneSetup.SaveZoneInfo(detailsDTO, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        //Rigion
        public ActionResult Regionlist(string flag, string name)
        {

            return View();
        }
        public ActionResult GetDivisionInfo(string flag, long? divisionId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetDivisionInfo");

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
               

                ViewBag.ddlZoneName = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
                return View();
            }

             

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _divisionInfo.GetDivisionInfos(User.OrgId, divisionId ?? 0);
                //IEnumerable<DivisionInfoDTO> dto = _divisionInfo.GetAllDivisionSetup(User.OrgId).Where(s => (name == "" || name == null) || (s.DivisionName.Contains(name))).Select(o => new DivisionInfoDTO
                //{
                //    ZoneId = o.ZoneId,
                //    OrganizationId = o.OrganizationId,
                //    OrganizationName = _organizationBusiness.GetOrganizationById(o.OrganizationId).OrganizationName,
                //    DivisionName = o.DivisionName,
                //    DivisionAssignName = o.DivisionAssignName,
                //    MobileNumber = o.MobileNumber,
                //    Remarks = o.Remarks,
                //    EntryDate = o.EntryDate,
                //    UpdateUserId = o.UpdateUserId,
                //    UpdateDate = o.UpdateDate,


                //}).ToList();

                List<DivisionInfoViewModel> viewModels = new List<DivisionInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetDivisionPartialView", viewModels);

                
            }
            return View();
        }
        public ActionResult CreateRegionlist(long? id)
        {

            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            ViewBag.ddlzonename = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(zne => new SelectListItem { Text = zne.ZoneName, Value = zne.ZoneId.ToString() }).ToList();

            return View();
        }


        public ActionResult getdiv(long id)

        {

            var divlist = _divisionInfo.GetAllDivisionSetup(User.OrgId).Where(x => x.ZoneId == id).Select(divv => new SelectListItem { Text = divv.DivisionName, Value = divv.DivisionId.ToString() }).ToList();
            if (divlist.Count > 0 && divlist != null)
            {
                return Json(new { flag = "1", msg = "Division found", data = divlist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { flag = "0", msg = "Division not found" },JsonRequestBehavior.AllowGet);
            }

        }

        //Area
        public ActionResult GetArealist(long? id)
        {
            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlZonename = _zoneSetup.GetAllZoneName().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
            ViewBag.ddlDivisionname = _divisionInfo.GetAllDivisionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();

            ViewBag.ddlRegionname = _regionSetup.GetAllRegionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.RegionName, Value = org.RegionId.ToString() }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SaveAreaInfo(List<AreaSetupViewModel> details)
        {
            bool IsSuccess = false;
            if (details.Count > 0)
            {
                List<AreaSetupDTO> detailsDTO = new List<AreaSetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _areaSetupBusiness.SaveAreaInfo(detailsDTO, User.UserId, User.OrgId);

            }
            return Json(IsSuccess);
        }

        #endregion
    }
}