using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
using ERPBO.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : BaseController
    {
        private readonly IReturnRawMaterialBusiness _returnRawMaterialBusiness;
        private readonly IAgroProductSalesInfoBusiness _agroProductSalesInfoBusiness;
        private readonly IAgroProductSalesDetailsBusiness _agroProductSalesDetailsBusiness;

        private readonly ISalesReturn _salesReturn;//e
        private readonly ISalesPaymentRegister _salesPaymentRegister;//e

        private readonly IRawMaterialTrack _rawMaterialTrack;//e
        private readonly IMRawMaterialIssueStockInfo _mRawMaterialIssueStockInfo;//e
        private readonly IMRawMaterialIssueStockDetails _mRawMaterialIssueStockDetails;//e

        private readonly IPRawMaterialStockInfo _pRawMaterialStockInfo;//e
        private readonly IPRawMaterialStockIDetails _pRawMaterialStockIDetails;//e

        private readonly ITerritorySetup _territorySetup;//e

        private readonly IAgroUnitInfo _agroUnitInfo;
        private readonly IUserInfo _userInfo;
        private readonly IStockiestInfo _stockiestInfo;
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
        private readonly IAppUserBusiness _appUserBusiness;
        private readonly IRawMaterialRequisitionInfoBusiness _rawMaterialRequisitionInfoBusiness;
        private readonly IRawMaterialRequisitionDetailsBusiness _rawMaterialRequisitionDetailsBusiness;




        public AgroConfigurationController(ISalesReturn salesReturn, IReturnRawMaterialBusiness returnRawMaterialBusiness, ISalesPaymentRegister salesPaymentRegister, IRawMaterialTrack rawMaterialTrack, IMRawMaterialIssueStockInfo mRawMaterialIssueStockInfo, IMRawMaterialIssueStockDetails mRawMaterialIssueStockDetails, IPRawMaterialStockInfo pRawMaterialStockInfo, IPRawMaterialStockIDetails pRawMaterialStockIDetails, IAgroUnitInfo agroUnitInfo, IUserInfo userInfo, IStockiestInfo stockiestInfo, ITerritorySetup territorySetup, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IZoneDetail zoneDetail, IZone zone, IOrganizationBusiness organizationBusiness, IDepotSetup depotSetup, IRawMaterialBusiness rawMaterialBusiness, IFinishGoodProductBusiness finishGoodProductBusiness, IBankSetup bankSetup, IFinishGoodProductSupplierBusiness finishGoodProductSupplierBusiness, IMeasuremenBusiness measuremenBusiness, IRawMaterialSupplier rawMaterialSupplierBusiness, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IRawMaterialStockDetail rawMaterialStockDetail, IRawMaterialIssueStockInfoBusiness rawMaterialIssueStockInfoBusiness, IRawMaterialIssueStockDetailsBusiness rawMaterialIssueStockDetailsBusiness, IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IFinishGoodProductionInfoBusiness finishGoodProductionInfoBusiness, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness, IAgroProductSalesDetailsBusiness agroProductSalesDetailsBusiness, IAppUserBusiness appUserBusiness, IRawMaterialRequisitionInfoBusiness rawMaterialRequisitionInfoBusiness, IRawMaterialRequisitionDetailsBusiness rawMaterialRequisitionDetailsBusiness)

        {
            this._salesReturn = salesReturn;//e
            this._returnRawMaterialBusiness = returnRawMaterialBusiness;//e
            this._salesPaymentRegister = salesPaymentRegister;//e

            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
            this._agroProductSalesDetailsBusiness = agroProductSalesDetailsBusiness;
            this._agroUnitInfo = agroUnitInfo;
            this._userInfo = userInfo;
            this._stockiestInfo = stockiestInfo;

            this._mRawMaterialIssueStockInfo = mRawMaterialIssueStockInfo;//e
            this._mRawMaterialIssueStockDetails = mRawMaterialIssueStockDetails;//e
            this._rawMaterialTrack = rawMaterialTrack;//e

            this._pRawMaterialStockInfo = pRawMaterialStockInfo;//e
            this._pRawMaterialStockIDetails = pRawMaterialStockIDetails;//e

            this._zoneSetup = zoneSetup;//e
            this._territorySetup = territorySetup;//e
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
            this._appUserBusiness = appUserBusiness;
            this._rawMaterialRequisitionInfoBusiness = rawMaterialRequisitionInfoBusiness;
            this._rawMaterialRequisitionDetailsBusiness = rawMaterialRequisitionDetailsBusiness;
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

                ViewBag.ddlOrganization = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == User.OrgId).Select(des => new SelectListItem { Text = des.OrganizationName, Value = des.OrganizationId.ToString() }).ToList();
                ViewBag.ddlDepotName = _depotSetup.GetAllDepotSetup(User.OrgId).Select(a => new SelectListItem { Text = a.DepotName, Value = a.DepotId.ToString() });
                ViewBag.ddlRawMaterial = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialName, Value = a.RawMaterialName });

                ViewBag.ddlUnit = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Select(a => new SelectListItem { Text = a.UnitName, Value = a.UnitId.ToString() }).ToList();

                return View();
            }

            else if (flag == "Search" && rawmaterialName != "")
            {

                IEnumerable<RawMaterialDTO> dto = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new RawMaterialDTO()
                {
                    //OrganizationName = _organizationBusiness.GetOrganizationById(a.OrganizationId).OrganizationName,
                    // DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,
                    RawMaterialName = a.RawMaterialName,
                    Status = a.Status,
                    //ExpireDate = a.ExpireDate,
                    //DepotId = a.DepotId,
                    RawMaterialId = a.RawMaterialId,
                    UnitId = a.UnitId,

                    // OrganizationId = a.OrganizationId,
                    //UserName = UserForEachRecord(a.EntryUserId).UserName



                }).Where(a => (a.RawMaterialName == rawmaterialName) && (a.RawMaterialName == rawmaterialName)).ToList();

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
                    Status = a.Status,
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

                    //DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,

                    //DepotName = _depotSetup.GetDepotNamebyId(a.DepotId, User.OrgId).DepotName,

                    RawMaterialName = a.RawMaterialName,
                    Status = a.Status,
                    //ExpireDate = a.ExpireDate,
                    // DepotId = a.DepotId,
                    RawMaterialId = a.RawMaterialId,
                    OrganizationId = a.OrganizationId,
                    UnitId = a.UnitId,
                    UnitName = _agroUnitInfo.GetAgroInfoById(a.UnitId, User.OrgId).UnitName,
                    //UserName = UserForEachRecord(a.EntryUserId).UserName


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

                    TradeLicense = o.TradeLicense,
                    TIN = o.TIN,
                    BIN = o.BIN,

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
        public ActionResult GetMeasurementList(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                //ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == User.OrgId).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlUnit = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Select(a => new SelectListItem { Text = a.UnitName, Value = a.UnitId.ToString() }).ToList();

                ViewBag.ddlUnits = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Select(a => new SelectListItem { Text = a.UnitName, Value = a.UnitId.ToString() }).ToList();

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "list")
            {
                //var measureMent = _measuremenBusiness.GetMeasurementSetups(User.OrgId);

                IEnumerable<MeasurementSetupDTO> dto = _measuremenBusiness.GetMeasurementSetups(User.OrgId).Where(s => (name == "" || name == null) || (s.MeasurementName.Contains(name))).Select(o => new MeasurementSetupDTO
                {
                    MeasurementId = o.MeasurementId,
                    OrganizationId = o.OrganizationId,
                    MeasurementName = o.MeasurementName,
                    Status = o.Status,
                    RoleId = o.RoleId,
                    UnitId = o.UnitId,
                    InnerBox = o.InnerBox,
                    MasterCarton = o.MasterCarton,
                    PackSize = o.PackSize,
                    //EntryUserId=o.EntryUserId.ToString(),
                    //UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUserId,
                    UpdateDate = o.UpdateDate,
                    UnitName = _agroUnitInfo.GetAgroInfoById(o.UnitId, User.OrgId).UnitName,
                }).ToList();


                List<MeasurementSetupViewModel> viewModels = new List<MeasurementSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);


                return PartialView("_GetMeasurementList", viewModels);
            }
            return View();

        }
        public ActionResult SaveMeasurement(List<MeasurementSetupViewModel> models)
        {
            bool IsSuccess = false;


            List<MeasurementSetupDTO> measurementSetupDTOs = new List<MeasurementSetupDTO>();
            AutoMapper.Mapper.Map(models, measurementSetupDTOs);
            IsSuccess = _measuremenBusiness.SaveMeasureMent(measurementSetupDTOs, User.UserId, User.OrgId);





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

        #region AgroUnit Setup

        public ActionResult GetAgroUnitList(string flag, string name, long? unitId)
        {
            if (string.IsNullOrEmpty(flag))
            {

            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Search")
            {

                // var dto = _agroUnitInfo.GetAgroUnitInfos(unitId ?? 0,User.OrgId);
                IEnumerable<AgroUnitInfoDTO> dto = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Where(s => (name == "" || name == null) || (s.UnitName.Contains(name)) || (s.Status.Contains(name))).Select(o => new AgroUnitInfoDTO
                {
                    UnitId = o.UnitId,
                    OrganizationId = o.OrganizationId,
                    UnitName = o.UnitName,
                    Status = o.Status,
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,
                    EntryDate = o.EntryDate,
                    UpdateUserId = o.UpdateUserId,
                    UpdateDate = o.UpdateDate
                }).ToList();

                List<AgroUnitInfoViewModel> viewModels = new List<AgroUnitInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetAgroUnitPartial", viewModels);


            }

            return View();
        }
        [HttpPost]
        public ActionResult SaveAgroUnitList(AgroUnitInfoViewModel model)
        {
            AgroUnitInfoDTO agroUnit = new AgroUnitInfoDTO();
            AutoMapper.Mapper.Map(model, agroUnit);
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                isSuccess = _agroUnitInfo.SaveAgroUnitList(agroUnit, User.UserId, User.OrgId);
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

                ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Where(o => o.Status == "Active").Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

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
                        UnitId = info.UnitId
                    };

                    details =
                        _rawMaterialStockDetail.GetRawMaterialStockDetailsById(id.Value, User.OrgId).Select(i => new RawMaterialStockDetailViewModel
                        {
                            //RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == rawMaterialId).RawMaterialName,
                            RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                            Quantity = i.Quantity,
                            UnitId = i.UnitId
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

            //ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Where(o => o.Status == "Active").Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

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

        public ActionResult GetRawMaterialStockLoadUnitName(long RawMaterialId)
        {
            var Unit = _rawMaterialBusiness.GetRawMaterialById(RawMaterialId, User.OrgId).UnitId;
            var unitname = _agroUnitInfo.GetAgroInfoById(Unit, User.OrgId).UnitName;

            // var divlist = _divisionInfo.GetAllDivisionSetup(User.OrgId).Where(x => x.ZoneId == id).Select(divv => new SelectListItem { Text = divv.DivisionName, Value = divv.DivisionId.ToString() }).ToList();

            return Json(unitname, JsonRequestBehavior.AllowGet);

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
                        UnitId = info.UnitId,
                        UnitName = _agroUnitInfo.GetAgroInfoById(info.UnitId, User.OrgId).UnitName
                    };
                    details = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(id.Value, User.OrgId).Select(i => new FinishGoodRecipeDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        FGRRawMaterQty = i.FGRRawMaterQty,
                        UnitId = i.UnitId,
                        UnitName = _agroUnitInfo.GetAgroInfoById(i.UnitId, User.OrgId).UnitName
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
                        UnitId = info.UnitId,
                        UnitName = _agroUnitInfo.GetAgroInfoById(info.UnitId, User.OrgId).UnitName,
                        FinishGoodProductId = info.FinishGoodProductId,
                        Status = info.Status,
                    };
                    details = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(id.Value, User.OrgId).Select(i => new FinishGoodRecipeDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        FGRRawMaterQty = i.FGRRawMaterQty,
                        UnitId = i.UnitId,
                        UnitName = _agroUnitInfo.GetAgroInfoById(i.UnitId, User.OrgId).UnitName,
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
            //ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).Select(r => new SelectListItem { Text = r.RawMaterialName, Value = r.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Where(o => o.Status == "Active").Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlUnit1 = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Select(a => new SelectListItem { Text = a.UnitName, Value = a.UnitId.ToString() }).ToList();

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

        public ActionResult GetRawMaterialFinishGoodRecipeLoadProductUnit(long RawMaterialId)
        {
            var Unit = _rawMaterialBusiness.GetRawMaterialById(RawMaterialId, User.OrgId).UnitId;


            return Json(Unit, JsonRequestBehavior.AllowGet);

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
                        UnitId = info.UnitId
                    };

                    details =
                        _rawMaterialIssueStockDetailsBusiness.GetRawMaterialIssueStockDetailsById(id.Value, User.OrgId).Select(i => new RawMaterialIssueStockDetailsViewModel
                        {
                            //RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == rawMaterialId).RawMaterialName,
                            RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                            Quantity = i.Quantity,
                            UnitId = i.UnitId,
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

            //ViewBag.ddlRawMaterialName = _rawMaterialStockInfo.GetCheckExpairDatewiseRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            //ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Where(o => o.Status == "Active").Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            return View();
        }
        public ActionResult GetRawMaterialIssueStockLoadUnit(long RawMaterialId)
        {
            var Unit = _rawMaterialBusiness.GetRawMaterialById(RawMaterialId, User.OrgId).UnitId;
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
            long unit = 0;
            if (checkRawMaterialStockValue != null)
            {
                itemStock = (checkRawMaterialStockValue.Quantity);
                unit = (checkRawMaterialStockValue.UnitId);
            }
            return Json(new { RawMaterialStockQty = itemStock, RawMaterialStockUnit = unit });


        }

        public ActionResult GetProductFinishGoodList(string flag, string finishGoodProductionBatch, long? productId)
        {


            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlReceipBatchCode = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceif(User.OrgId).Where(fg => fg.ReceipeBatchCode != null).Select(f => new SelectListItem { Text = f.ReceipeBatchCode, Value = f.ReceipeBatchCode }).ToList();

                //ViewBag.ddlProduct = _finishGoodProductBusiness.GetAllProductInfo(User.OrgId).Select(d => new SelectListItem { Text = _finishGoodProductBusiness.GetFinishGoodProductById(d.FinishGoodProductId, User.OrgId).FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();
                ViewBag.ddlProduct = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();


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

                    //issueQunatitys = _rawMaterialIssueStockInfoBusiness.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).Quantity;
                    var StocInkqty = _mRawMaterialIssueStockDetails.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).ToList();
                    var SumStockinQty = StocInkqty.Sum(c => c.Quantity);


                    var StockOutqty = _mRawMaterialIssueStockDetails.RawMaterialStockIssueInfobyRawMaterialidOut(rawMaterial.RawMaterialId, User.OrgId).ToList();
                    var SumStockOutQty = StockOutqty.Sum(d => d.Quantity);

                    issueQunatitys = SumStockinQty - SumStockOutQty;
                    //myList += string.Format("{0},", rawMaterial.RawMaterialId);


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

                    //issueQunatity = _rawMaterialIssueStockInfoBusiness.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).Quantity;

                    var StocInkqty = _mRawMaterialIssueStockDetails.RawMaterialStockIssueInfobyRawMaterialid(rawMaterial.RawMaterialId, User.OrgId).ToList();
                    var SumStockinQty = StocInkqty.Sum(c => c.Quantity);


                    var StockOutqty = _mRawMaterialIssueStockDetails.RawMaterialStockIssueInfobyRawMaterialidOut(rawMaterial.RawMaterialId, User.OrgId).ToList();
                    var SumStockOutQty = StockOutqty.Sum(d => d.Quantity);

                    issueQunatity = SumStockinQty - SumStockOutQty;


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


        #region Zone list
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
                    Status = o.Status,

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
        public ActionResult CreateZonelist(long? id)
        {
            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveZonetInfo(List<ZoneSetupViewModel> details, ZoneSetupViewModel edetails)
        {
            bool IsSuccess = false;

            if (details == null && edetails != null)
            {
                ZoneSetupDTO dTO = new ZoneSetupDTO();
                AutoMapper.Mapper.Map(edetails, dTO);
                IsSuccess = _zoneSetup.SaveZoneInfoEdit(dTO, User.UserId, User.OrgId);
            }
            else if (details.Count > 0)
            {
                List<ZoneSetupDTO> detailsDTO = new List<ZoneSetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _zoneSetup.SaveZoneInfo(detailsDTO, User.UserId, User.OrgId);
            }

            return Json(IsSuccess);
        }
        #endregion

        #region Division List
        public ActionResult GetDivisionInfo(string flag, long? divisionId, long? zoneId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetDivisionInfo");

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();


                ViewBag.ddlZoneName = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();

                ViewBag.ddlDivisionName = _divisionInfo.GetAllDivisionSetup(User.OrgId).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();

                return View();


            }



            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _divisionInfo.GetDivisionInfos(divisionId ?? 0, zoneId ?? 0, User.OrgId);

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
        public ActionResult CreateDivisionInfo(long? id)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            //ViewBag.ddlZoneName = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
            ViewBag.ddlZonename = _zoneSetup.GetAllZoneName().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SaveDivisionInfo(List<DivisionInfoViewModel> details, DivisionInfoViewModel update)
        {
            bool IsSuccess = false;
            if (details.Count > 0)
            {
                List<DivisionInfoDTO> dto = new List<DivisionInfoDTO>();

                AutoMapper.Mapper.Map(details, dto);
                IsSuccess = _divisionInfo.SaveDivisionInfo(dto, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }

        public ActionResult UpdateDivision(DivisionInfoViewModel update)
        {
            bool IsSuccess = false;
            DivisionInfoDTO updateDTO = new DivisionInfoDTO();
            AutoMapper.Mapper.Map(update, updateDTO);
            IsSuccess = _divisionInfo.UpdateDivision(updateDTO, User.UserId, User.OrgId);

            return Json(IsSuccess);
        }


        #endregion

        #region Region List
        public ActionResult Regionlist(string flag, string name, long? divisionId, long? regionId)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
                ViewBag.ddldivname = _divisionInfo.GetAllDivisionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();
                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _regionSetup.GetRegionInfos(User.OrgId, name ?? null, regionId ?? 0, divisionId ?? 0);



                List<RegionSetupViewModel> viewModels = new List<RegionSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRegionPartialView", viewModels);
            }

            return View();
        }
        public ActionResult CreateRegionlist(long? id)
        {

            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddldivname = _divisionInfo.GetAllDivisionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();
            return View();
        }
        public ActionResult SaveRegionInfo(List<RegionSetupViewModel> details, RegionSetupViewModel edetails)
        {
            bool IsSuccess = false;

            if (details == null && edetails != null)
            {
                RegionSetupDTO dTO = new RegionSetupDTO();
                AutoMapper.Mapper.Map(edetails, dTO);
                IsSuccess = _regionSetup.SaveRegionInfoEdit(dTO, User.UserId, User.OrgId);
            }

            else if (details.Count > 0)
            {
                List<RegionSetupDTO> detailsDTO = new List<RegionSetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _regionSetup.SaveRegionInfo(detailsDTO, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Area List

        public ActionResult AreaList(string flag, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                //ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlRegionname = _regionSetup.GetAllRegionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.RegionName, Value = org.RegionId.ToString() }).ToList();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "AreaSetup")
            {
                var dto = _areaSetupBusiness.GetAllAreaSetup(User.OrgId, name ?? null);

                List<AreaSetupViewModel> viewModel = new List<AreaSetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModel);
                return PartialView("_GetAreaPartialView", viewModel);
            }
            return View();
        }
        public ActionResult GetArealist(long? id)
        {
            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            //ViewBag.ddlZonename = _zoneSetup.GetAllZoneName().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
            //ViewBag.ddlDivisionname = _divisionInfo.GetAllDivisionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();

            ViewBag.ddlRegionname = _regionSetup.GetAllRegionSetup(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.RegionName, Value = org.RegionId.ToString() }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SaveAreaInfo(List<AreaSetupViewModel> details, AreaSetupViewModel dto)
        {
            bool IsSuccess = false;
            if (details != null && details.Count > 0 && dto.AreaId == 0)
            {
                List<AreaSetupDTO> detailsDTO = new List<AreaSetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _areaSetupBusiness.SaveAreaInfo(detailsDTO, User.UserId, User.OrgId);

            }
            else if (details == null && dto != null)
            {
                AreaSetupDTO detailsDTO = new AreaSetupDTO();
                AutoMapper.Mapper.Map(dto, detailsDTO);
                IsSuccess = _areaSetupBusiness.SaveAreaInfoUpdate(detailsDTO, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Teritory List

        public ActionResult Territorylist(string flag, string name, long? areaId, long? territoryId)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
                ViewBag.ddlareaname = _areaSetupBusiness.GetAllAreaSetupV(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.AreaName, Value = org.AreaId.ToString() }).ToList();

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _territorySetup.GetTerritoryInfos(User.OrgId, name ?? null, territoryId ?? 0, areaId ?? 0);


                List<TerritorySetupViewModel> viewModels = new List<TerritorySetupViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetTerritoryPartialView", viewModels);
            }

            return View();
        }

        public ActionResult CreateTerritorylist(long? id)
        {

            ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlareaname = _areaSetupBusiness.GetAllAreaSetupV(9).Where(x => x.OrganizationId == 9).Select(org => new SelectListItem { Text = org.AreaName, Value = org.AreaId.ToString() }).ToList();
            return View();
        }

        public ActionResult SaveTerritoryInfo(List<TerritorySetupViewModel> details, TerritorySetupViewModel edetails)
        {
            bool IsSuccess = false;

            if (details == null && edetails != null)
            {
                TerritorySetupDTO dTO = new TerritorySetupDTO();
                AutoMapper.Mapper.Map(edetails, dTO);
                IsSuccess = _territorySetup.SaveTerritoryInfoEdit(dTO, User.UserId, User.OrgId);
            }

            else if (details.Count > 0)
            {
                List<TerritorySetupDTO> detailsDTO = new List<TerritorySetupDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _territorySetup.SaveTerritoryInfo(detailsDTO, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Extra Code
        //public ActionResult getdiv(long id)

        //{

        //    var divlist = _divisionInfo.GetAllDivisionSetup(User.OrgId).Where(x => x.ZoneId == id).Select(divv => new SelectListItem { Text = divv.DivisionName, Value = divv.DivisionId.ToString() }).ToList();
        //    if (divlist.Count > 0 && divlist != null)
        //    {
        //        return Json(new { flag = "1", msg = "Division found", data = divlist }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { flag = "0", msg = "Division not found" },JsonRequestBehavior.AllowGet);
        //    }

        //}

        //Area
        #endregion

        #region Stockiest List

        public ActionResult GetStockiestList(string flag, long? stockiestId, long? territoryId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetStockiestList");

            if (string.IsNullOrEmpty(flag))
            {

                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                ViewBag.ddlStockiestName = _stockiestInfo.GetAllStockiestSetup(User.OrgId).Select(stock => new SelectListItem { Text = stock.StockiestName, Value = stock.StockiestId.ToString() }).ToList();

                ViewBag.ddlTerritoryName = _territorySetup.GetAllTerritorySetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.TerritoryName, Value = terr.TerritoryId.ToString() }).ToList();

                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _stockiestInfo.GetStockiestInfos(stockiestId ?? 0, territoryId ?? 0, User.OrgId);

                List<StockiestInfoViewModel> viewModels = new List<StockiestInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetStockiestPartialView", viewModels);


            }

            return View();
        }

        public ActionResult CreateStockiestList(long? id)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlTerritoryName = _territorySetup.GetAllTerritorySetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.TerritoryName, Value = terr.TerritoryId.ToString() }).ToList();

            return View();
        }

        public ActionResult SaveStockiestList(List<StockiestInfoViewModel> details)
        {
            bool IsSuccess = false;
            if (details.Count > 0)
            {
                List<StockiestInfoDTO> dto = new List<StockiestInfoDTO>();

                AutoMapper.Mapper.Map(details, dto);
                IsSuccess = _stockiestInfo.SaveStockiestList(dto, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);

        }

        public ActionResult UpdateStockiestList(StockiestInfoViewModel update)
        {
            bool IsSuccess = false;
            StockiestInfoDTO updateDTO = new StockiestInfoDTO();
            AutoMapper.Mapper.Map(update, updateDTO);
            IsSuccess = _stockiestInfo.UpdateStockiestList(updateDTO, User.UserId, User.OrgId);

            return Json(IsSuccess);
        }


        #endregion


        #region ExtraCode

        public ActionResult GetUserInfoList(string flag, long? userId, string departmentName, string designation, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetUserInfoList");

            if (string.IsNullOrEmpty(flag))
            {

                ViewBag.ddlUserName = _userInfo.GetAllUserInfo(User.OrgId).Select(user => new SelectListItem { Text = user.UserName, Value = user.UserId.ToString() }).ToList();

                ViewBag.ddlDepartMentName = _userInfo.GetAllUserInfo(User.OrgId).Select(user => new SelectListItem { Text = user.DepartmentName, Value = user.UserId.ToString() }).ToList();

                ViewBag.ddlDesignationName = _userInfo.GetAllUserInfo(User.OrgId).Select(user => new SelectListItem { Text = user.Designation, Value = user.UserId.ToString() }).ToList();

                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _userInfo.GetUserInfos(userId ?? 0, departmentName, designation, User.OrgId);

                List<UserInfoViewModel> viewModels = new List<UserInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetUserInfoPartialView", viewModels);


            }


            return View();
        }

        public ActionResult CreateUserInfoList(long? id)
        {
            return View();
        }

        public ActionResult SaveUserInfoList(List<UserInfoViewModel> details)
        {
            bool IsSuccess = false;
            if (details.Count > 0)
            {
                List<UserInfoDTO> dto = new List<UserInfoDTO>();

                AutoMapper.Mapper.Map(details, dto);
                IsSuccess = _userInfo.SaveUserInfoList(dto, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }

        public ActionResult UpdateUserInfoList(UserInfoViewModel update)
        {
            bool IsSuccess = false;
            UserInfoDTO updateDTO = new UserInfoDTO();
            AutoMapper.Mapper.Map(update, updateDTO);
            IsSuccess = _userInfo.UpdateUserInfoList(updateDTO, User.UserId, User.OrgId);

            return Json(IsSuccess);
        }

        #endregion

        #region user

        public ActionResult GetUserAssignInformation()
        {
            ViewBag.ddlZoneName = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
            ViewBag.ddlDivisionName = _divisionInfo.GetAllDivisionSetup(User.OrgId).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();
            ViewBag.ddlRegionName = _regionSetup.GetAllRegionSetup(User.OrgId).Select(org => new SelectListItem { Text = org.RegionName, Value = org.RegionId.ToString() }).ToList();
            ViewBag.ddlAreaName = _areaSetupBusiness.GetAllAreaSetupV(User.OrgId).Select(org => new SelectListItem { Text = org.AreaName, Value = org.AreaId.ToString() }).ToList();
            ViewBag.ddlTerritoryName = _territorySetup.GetAllTerritorySetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.TerritoryName, Value = terr.TerritoryId.ToString() }).ToList();
            ViewBag.ddlStockiestName = _stockiestInfo.GetAllStockiestSetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.StockiestName, Value = terr.StockiestId.ToString() }).ToList();

            return View();
        }
        public ActionResult SaveUserAssignInformation()
        {

            ViewBag.ddlZoneName = _zoneSetup.GetAllZoneSetup(User.OrgId).Select(org => new SelectListItem { Text = org.ZoneName, Value = org.ZoneId.ToString() }).ToList();
            ViewBag.ddlDivisionName = _divisionInfo.GetAllDivisionSetup(User.OrgId).Select(org => new SelectListItem { Text = org.DivisionName, Value = org.DivisionId.ToString() }).ToList();
            ViewBag.ddlRegionName = _regionSetup.GetAllRegionSetup(User.OrgId).Select(org => new SelectListItem { Text = org.RegionName, Value = org.RegionId.ToString() }).ToList();
            ViewBag.ddlAreaName = _areaSetupBusiness.GetAllAreaSetupV(User.OrgId).Select(org => new SelectListItem { Text = org.AreaName, Value = org.AreaId.ToString() }).ToList();
            ViewBag.ddlTerritoryName = _territorySetup.GetAllTerritorySetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.TerritoryName, Value = terr.TerritoryId.ToString() }).ToList();
            ViewBag.ddlUserName = _userInfo.GetAllUserInfo(User.OrgId).Select(u => new SelectListItem { Text = u.UserName, Value = u.UserId.ToString() });
            ViewBag.ddlStockiestName = _stockiestInfo.GetAllStockiestSetup(User.OrgId).Select(terr => new SelectListItem { Text = terr.StockiestName, Value = terr.StockiestId.ToString() }).ToList();

            return View();
        }
        #endregion
        #endregion

        #region Sales Invoice


        public ActionResult GetAgroSalesProductList(string flag, long? ProductId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetAgroSalesProductList");
            if (string.IsNullOrEmpty(flag))
            {

                //ViewBag.ddlProductName = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();




                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _agroProductSalesInfoBusiness.GetAgroSalesInfos(User.OrgId, ProductId ?? 0);


                List<AgroProductSalesInfoViewModel> viewModels = new List<AgroProductSalesInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetAgroSalesProductList", viewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                var FinishGoodProductName = _agroProductSalesDetailsBusiness.GetAllAgroSalesDetailsInfos(User.OrgId).ToList();
                var StockiestName = _stockiestInfo.GetAllStockiestSetup(User.OrgId).ToList();

                var info = _agroProductSalesInfoBusiness.GetAgroProductionInfoById(id.Value, User.OrgId);
                List<AgroProductSalesDetailsViewModel> details = new List<AgroProductSalesDetailsViewModel>();
                if (info != null)
                {


                    ViewBag.Info = new AgroProductSalesInfoViewModel
                    {
                        StockiestName = StockiestName.FirstOrDefault(it => it.StockiestId == info.StockiestId).StockiestName,
                        InvoiceNo = info.InvoiceNo,
                        InvoiceDate = info.InvoiceDate
                        //StockiestName = _stockiestInfo.GetAllStockiestSetup(info.StockiestId, User.OrgId).StockiestName
                    };

                    details = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(id.Value, User.OrgId).Select(i => new AgroProductSalesDetailsViewModel
                    {
                        MeasurementSize = i.MeasurementSize,
                        //FinishGoodProductName =_agroProductSalesDetailsBusiness.GetAllAgroSalesDetailsInfos(User.OrgId),

                        FinishGoodProductName = FinishGoodProductName.FirstOrDefault(it => it.FinishGoodProductInfoId == i.FinishGoodProductInfoId).FinishGoodProductName,


                        Price = i.Price,
                        Discount = i.Discount,
                        Quanity = i.Quanity,




                    }).ToList();

                }
                else
                {
                    ViewBag.Info = new AgroProductSalesInfoViewModel();
                }
                return PartialView("_GetAgroSalesProductDetails", details);
            }


            return View();
        }

        [HttpGet]
        public ActionResult CreateAgroSalesProduct(long? id, long? finishGoodProductId)
        {


            ViewBag.ddlClientName = _appUserBusiness.GetAllAppUserByOrgId(User.OrgId).Where(o => o.RoleId == 34).Select(d => new SelectListItem { Text = d.FullName, Value = d.UserId.ToString() }).ToList();

            //ViewBag.ddlProductName = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceif(User.OrgId).Select(d => new SelectListItem { Text = _finishGoodProductBusiness.GetFinishGoodProductById(d.FinishGoodProductId, User.OrgId).FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();

            //var targetQuantity=_finishGoodProductionInfoBusiness.GetFinishGoodProductionInfo(User)

            ViewBag.ddlProductName = _finishGoodProductionInfoBusiness.GetFinishGoodProductInfos(User.OrgId).Select(f => new SelectListItem { Text = f.FinishGoodProductName + "(" + f.ReceipeBatchCode + ")"+"-"+f.TargetQuantity, Value = f.FinishGoodProductId.ToString() }).ToList();


            ViewBag.ddlMeasurementName = _measuremenBusiness.GetMeasurementSetups(User.OrgId).Select(d => new SelectListItem { Text = d.MeasurementName, Value = d.MeasurementId.ToString() }).ToList();

            ViewBag.ddlMeasurementSize = _measuremenBusiness.GetMeasurementSetups(User.OrgId).Select(d => new SelectListItem { Text = d.MasterCarton.ToString() + "*" + d.InnerBox.ToString() + "*" + d.PackSize.ToString() + "(" + d.UnitId + ")", Value = d.MeasurementId.ToString() }).ToList();



            return View();
        }

        public ActionResult GetMeasurementIdWiseSize(long MeasurementId)
        {
            var MasterCarton = _measuremenBusiness.GetMeasurementById(MeasurementId, User.OrgId).MasterCarton;
            var InnerBox = _measuremenBusiness.GetMeasurementById(MeasurementId, User.OrgId).InnerBox;
            var PackSize = _measuremenBusiness.GetMeasurementById(MeasurementId, User.OrgId).PackSize;
            var Unit = _measuremenBusiness.GetMeasurementById(MeasurementId, User.OrgId).UnitId;
            var UnitName = _agroUnitInfo.GetAgroInfoById(Unit, User.OrgId).UnitName;

            var MeasurementSize = MasterCarton + "*" + InnerBox + "*" + PackSize + "(" + UnitName + ")";

            return Json(MeasurementSize, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetFinishGoodstockCheck(long FinishGoodProductInfoId)
        {
            var checkFinishGoodStockValue = _finishGoodProductionInfoBusiness.GetCheckFinishGoodQuantity(FinishGoodProductInfoId, User.OrgId);

            double itemStock = 0;

            if (checkFinishGoodStockValue != null)
            {
                itemStock = (checkFinishGoodStockValue.FirstOrDefault().TargetQuantity);

            }
            return Json(new { FinishGoodStockQty = itemStock, JsonRequestBehavior.AllowGet });
            //return Json(itemStock, JsonRequestBehavior.AllowGet );


        }

        public ActionResult SaveAgroProductSalesInfo(AgroProductSalesInfoViewModel info, List<AgroProductSalesDetailsViewModel> details)
        {

            bool isSucccess = false;
            //if (ModelState.IsValid && details.Count() > 0)
            //{
            AgroProductSalesInfoDTO agroSalesInfoDTO = new AgroProductSalesInfoDTO();
            List<AgroProductSalesDetailsDTO> agroSalesDetailsDTOs = new List<AgroProductSalesDetailsDTO>();
            AutoMapper.Mapper.Map(info, agroSalesInfoDTO);
            AutoMapper.Mapper.Map(details, agroSalesDetailsDTOs);
            isSucccess = _agroProductSalesInfoBusiness.SaveAgroProductSalesInfo(agroSalesInfoDTO, agroSalesDetailsDTOs, User.UserId, User.OrgId);
            //}
            return Json(isSucccess);
        }

        public ActionResult AgroProductSalesReport(long ProductSalesInfoId)
        {
            var InvoiceNo = _agroProductSalesInfoBusiness.GetInvoiceProductionInfoById(ProductSalesInfoId).InvoiceNo;
            var data = _agroProductSalesInfoBusiness.GetProductSalesData(InvoiceNo);

            LocalReport localReport = new LocalReport();


            string reportPath = Server.MapPath("~/Reports/ERPRpt/Agriculture/rptAgroProductSalesReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
            }

            ReportDataSource dataSource1 = new ReportDataSource("dsAgroSalesReport", data);
            localReport.DataSources.Add(dataSource1);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>8.27in</PageWidth>" +
                    "<PageHeight>11.69in</PageHeight>" +
                    "<MarginTop>0.25in</MarginTop>" +
                    "<MarginLeft>0.25in</MarginLeft>" +
                    "<MarginRight>0.25in</MarginRight>" +
                    "<MarginBottom>0.25in</MarginBottom>" +
                    "</DeviceInfo>";

            var renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
                );
            return File(renderedBytes, mimeType);
        }



        public ActionResult AgroProductSalesChallanReport(long ProductSalesInfoId)
        {
            var ChallanNo = _agroProductSalesInfoBusiness.GetChallanProductionInfoById(ProductSalesInfoId).ChallanNo;
            var data = _agroProductSalesInfoBusiness.GetProductSalesChallanData(ChallanNo);

            LocalReport localReport = new LocalReport();


            string reportPath = Server.MapPath("~/Reports/ERPRpt/Agriculture/rptAgroSalesChallanReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
            }

            ReportDataSource dataSource1 = new ReportDataSource("dsAgroSalesChallanReport", data);
            localReport.DataSources.Add(dataSource1);

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>8.27in</PageWidth>" +
                    "<PageHeight>11.69in</PageHeight>" +
                    "<MarginTop>0.25in</MarginTop>" +
                    "<MarginLeft>0.25in</MarginLeft>" +
                    "<MarginRight>0.25in</MarginRight>" +
                    "<MarginBottom>0.25in</MarginBottom>" +
                    "</DeviceInfo>";

            var renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
                );
            return File(renderedBytes, mimeType);
        }

        #endregion

        #region Payment

        public ActionResult SalesPaymentList(string flag, string name, long? id)
        {
            if (string.IsNullOrEmpty(flag))
            {
                // ViewBag.ddlRawmaterialName = _returnRawMaterialBusiness.GetIssueRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();
                ViewBag.ddlorgname = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                IEnumerable<AgroProductSalesInfoDTO> dto = _agroProductSalesInfoBusiness.GetAllDueSalesInvoice().Where(s => (name == "" || name == null) || (s.InvoiceNo.Contains(name))).Select(o => new AgroProductSalesInfoDTO
                {
                    ProductSalesInfoId = o.ProductSalesInfoId,
                    InvoiceNo = o.InvoiceNo,
                    InvoiceDate = o.InvoiceDate,
                    TotalAmount = o.TotalAmount,
                    PaidAmount = o.PaidAmount,
                    DueAmount = o.DueAmount,

                }).ToList();

                List<AgroProductSalesInfoViewModel> viewModels = new List<AgroProductSalesInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetDueInvoicePartialView", viewModels);
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {

                var info = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(id.Value);
                List<SalesPaymentRegisterViewModel> details = new List<SalesPaymentRegisterViewModel>();

                if (info != null)
                {

                    ViewBag.Info = new AgroProductSalesInfoViewModel
                    {
                        InvoiceNo = info.InvoiceNo,
                        InvoiceDate = info.InvoiceDate,
                        TotalAmount = info.TotalAmount,
                        PaidAmount = info.PaidAmount,
                        DueAmount = info.PaidAmount

                    };
                    details = _salesPaymentRegister.GetPaymentDetailsByInvoiceId(id.Value).Select(i => new SalesPaymentRegisterViewModel
                    {
                        PaymentDate = i.PaymentDate,
                        Remarks = i.Remarks,
                        PaymentAmount = i.PaymentAmount
                    }).ToList();
                }
                else
                {
                    ViewBag.Info = new AgroProductSalesInfoViewModel();
                }
                return PartialView("_PaymentHistoryDetails", details);

            }


            return View();
        }


        public ActionResult SaveSalesPayment(SalesPaymentRegisterViewModel info)
        {
            //

            bool isSucccess = false;
            var salesID = info.ProductSalesInfoId;


            SalesPaymentRegisterDTO salesPaymentRegisterDTO = new SalesPaymentRegisterDTO();

            AutoMapper.Mapper.Map(info, salesPaymentRegisterDTO);
            isSucccess = _salesPaymentRegister.SaveSalesPayment(salesPaymentRegisterDTO, User.UserId);

            //isSucccess = _agroProductSalesInfoBusiness.SaveAgroProductSalesInfo(agroSalesInfoDTO, agroSalesDetailsDTOs, User.UserId, User.OrgId);

            return Json(isSucccess);
        }


        #endregion

        #region Purchase & RawmaterialStock

        public ActionResult GetPRawmaterialStockList(string flag, string name, long? rsupid, long? id)
        {

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
                ViewBag.ddlRawMaterial = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialName, Value = a.RawMaterialId.ToString() });
                //ViewBag.ddlDepotName = _depotSetup.GetAllDepotSetup(User.OrgId).Select(a => new SelectListItem { Text = a.DepotName, Value = a.DepotId.ToString() });
                //ViewBag.ddlUnitName = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).Select(a => new SelectListItem { Text = a.UnitName, Value = a.UnitId.ToString() });
                ViewBag.ddlSupplierName = _rawMaterialSupplierBusiness.GetAllRawMaterialSupplierInfo(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialSupplierName, Value = a.RawMaterialSupplierId.ToString() });
                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _pRawMaterialStockInfo.GetAllPRawMaterialStockInfo(User.OrgId, name ?? null, rsupid ?? 0);

                List<PRawMaterialStockInfoViewModel> viewModels = new List<PRawMaterialStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialStockpurchaseList", viewModels);
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Direct)
            {
                var dto = _rawMaterialTrack.GetMainStockInOutInfos(name ?? null);

                List<RawMaterialTrackViewModel> viewModels = new List<RawMaterialTrackViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialMainView", viewModels);

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {

                var SupplierName = _rawMaterialSupplierBusiness.GetAllRawMaterialSupplierInfo(User.OrgId).ToList();
                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();
                var Unitsname = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).ToList();

                var info = _pRawMaterialStockInfo.GetRawmaterialPuschaseInfoOneById(id.Value, User.OrgId);
                List<PRawMaterialStockIDetailsViewModel> details = new List<PRawMaterialStockIDetailsViewModel>();

                if (info != null)
                {
                    ViewBag.Info = new PRawMaterialStockInfoViewModel
                    {
                        RawMaterialSupplierName = SupplierName.FirstOrDefault(x => x.RawMaterialSupplierId == info.RawMaterialSupplierId).RawMaterialSupplierName,
                        BatchCode = info.BatchCode,
                        EntryDate = info.EntryDate,
                        TotalAmount = info.TotalAmount,
                    };

                    details = _pRawMaterialStockIDetails.GetRawMatwrialPurchaseDetailsByInfoId(id.Value).Select(i => new PRawMaterialStockIDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(x => x.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        UnitName = Unitsname.FirstOrDefault(x => x.UnitId == i.UnitID).UnitName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        SubTotal = i.SubTotal,
                        StockDate = i.StockDate

                    }).ToList();
                }
                else
                {
                    ViewBag.Info = new PRawMaterialStockInfoViewModel();
                }
                return PartialView("_RawmatiralPurchaseDetails", details);

            }

            return View();

        }




        [HttpPost]
        public ActionResult SaveRawmaterialPurchaseStock(PRawMaterialStockInfoViewModel info, List<PRawMaterialStockIDetailsViewModel> details)
        {
            bool IsSuccess = false;

            PRawMaterialStockInfoDTO infoDTO = new PRawMaterialStockInfoDTO();
            List<PRawMaterialStockIDetailsDTO> detailDTOs = new List<PRawMaterialStockIDetailsDTO>();
            AutoMapper.Mapper.Map(info, infoDTO);
            AutoMapper.Mapper.Map(details, detailDTOs);
            IsSuccess = _pRawMaterialStockInfo.SaveRawMaterialPurchaseStock(infoDTO, detailDTOs, User.UserId, User.OrgId);



            return Json(IsSuccess);
        }


        public ActionResult RawMaterialStockLoadUnitName(long RawMaterialId)
        {
            var Unit = _rawMaterialBusiness.GetRawMaterialById(RawMaterialId, User.OrgId).UnitId;
            var unitname = _agroUnitInfo.GetAgroInfoById(Unit, User.OrgId).UnitName;
            return Json(unitname, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RawMaterialStockLoadUnitID(long RawMaterialId)
        {
            var UnitIDd = _rawMaterialBusiness.GetRawMaterialById(RawMaterialId, User.OrgId).UnitId;
            return Json(UnitIDd, JsonRequestBehavior.AllowGet);

        }



        #endregion

        #region RawmaterialIssue

        public ActionResult GetMRawmaterialIssueList(string flag, string name, long? id)
        {

            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();
                ViewBag.ddlRawMaterial = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialName, Value = a.RawMaterialId.ToString() });
                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                IEnumerable<MRawMaterialIssueStockInfoDTO> dto = _mRawMaterialIssueStockInfo.GetAllRawMaterialIssue(User.OrgId).Where(s => (name == "" || name == null) || (s.ProductBatchCode.Contains(name))).Select(o => new MRawMaterialIssueStockInfoDTO
                {
                    RawMaterialIssueStockId = o.RawMaterialIssueStockId,
                    ProductBatchCode = o.ProductBatchCode,
                    EntryDate = o.EntryDate,
                    Status = o.Status,
                    UserName = UserForEachRecord(o.EntryUserId.Value).UserName,


                }).ToList();

                List<MRawMaterialIssueStockInfoViewModel> viewModels = new List<MRawMaterialIssueStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialIssuePartialView", viewModels);
            }


            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Direct)
            {
                var dto = _mRawMaterialIssueStockDetails.GetIssueInOutInfos(name ?? null);

                List<MRawMaterialIssueStockDetailsViewModel> viewModels = new List<MRawMaterialIssueStockDetailsViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialIssueView", viewModels);

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {

                var RawMaterialNames = _rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();
                var Unitsname = _agroUnitInfo.GetAllAgroUnitInfo(User.OrgId).ToList();
                string Status = "StockIn";
                var info = _mRawMaterialIssueStockInfo.GetRawmaterialIssueInfoOneById(id.Value, User.OrgId);

                List<MRawMaterialIssueStockDetailsViewModel> details = new List<MRawMaterialIssueStockDetailsViewModel>();

                if (info != null)
                {
                    ViewBag.Info = new MRawMaterialIssueStockInfoViewModel
                    {

                        ProductBatchCode = info.ProductBatchCode,
                        EntryDate = info.EntryDate,
                        Status = info.Status,


                    };

                    details = _mRawMaterialIssueStockDetails.GetRawMatwrialissueDetailsByInfoId(id.Value, Status).Select(i => new MRawMaterialIssueStockDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(x => x.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        UnitName = Unitsname.FirstOrDefault(x => x.UnitId == i.UnitID).UnitName,
                        Quantity = i.Quantity,
                        IssueStatus = i.IssueStatus,
                        EntryDate = i.EntryDate
                    }).ToList();

                }
                else
                {
                    ViewBag.Info = new MRawMaterialIssueStockInfoViewModel();
                }
                return PartialView("_RawmatiralIssueDetails", details);

            }

            return View();

        }


        public ActionResult GetStockQty(long RawMaterialId)
        {
            var StockInRMID = _rawMaterialTrack.GetAllRawMaterialTruck().Where(x => x.RawMaterialId == RawMaterialId && x.IssueStatus == "StockIn").ToList();
            var Stockinqty = StockInRMID.Sum(c => c.Quantity);

            var StockoutRMID = _rawMaterialTrack.GetAllRawMaterialTruck().Where(w => w.RawMaterialId == RawMaterialId && w.IssueStatus == "StockOut").ToList();
            var Stockoutqty = StockoutRMID.Sum(d => d.Quantity);

            var ReturnRMID = _returnRawMaterialBusiness.GetAllReturnRawMaterial().Where(g=> g.RawMaterialId == RawMaterialId && g.ReturnType == "Good").ToList();
            var ReturnRMGoodQty = ReturnRMID.Sum(g => g.Quantity);//good return qty

            var stkrmnqty = Stockinqty - Stockoutqty;//stock

            var rmnqty = stkrmnqty + ReturnRMGoodQty;

            return Json(rmnqty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveRawmaterialPIssueStock(MRawMaterialIssueStockInfoViewModel info, List<MRawMaterialIssueStockDetailsViewModel> details)
        {
            bool IsSuccess = false;

            MRawMaterialIssueStockInfoDTO infoDTO = new MRawMaterialIssueStockInfoDTO();
            List<MRawMaterialIssueStockDetailsDTO> detailDTOs = new List<MRawMaterialIssueStockDetailsDTO>();
            AutoMapper.Mapper.Map(info, infoDTO);
            AutoMapper.Mapper.Map(details, detailDTOs);
            IsSuccess = _mRawMaterialIssueStockInfo.SaveRawMaterialIssueStock(infoDTO, detailDTOs, User.UserId, User.OrgId);



            return Json(IsSuccess);
        }

        #endregion

        #region  InternalReturn
        public ActionResult RawMaterialReturnList(string flag, long? id, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlRawmaterialName = _returnRawMaterialBusiness.GetIssueRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();

                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _returnRawMaterialBusiness.GetReturnRawMaterialInfos(name ?? null);
                List<ReturnRawMaterialViewModel> viewModels = new List<ReturnRawMaterialViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetReturnRawMaterial", viewModels);


            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
            {
                string Status = "Pending";
                string ReturnType = "Damage";

                List<ReturnRawMaterialViewModel> details = new List<ReturnRawMaterialViewModel>();

                details = _returnRawMaterialBusiness.GetReturnRawMaterialBYRMId(id.Value, ReturnType, Status).Select(i => new ReturnRawMaterialViewModel {
                    EntryDate = i.EntryDate,
                    Quantity = i.Quantity,
                    ReturnRawMaterialId = i.ReturnRawMaterialId

                }).ToList();
                
                return PartialView("_ReturnDetails", details);
            }



            return View();
        }


        public ActionResult RawMaterialReturnAllList(string flag, long? id, string name)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlRawmaterialName = _returnRawMaterialBusiness.GetIssueRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();

                return View();

            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _returnRawMaterialBusiness.GetallReturns(name ?? null);

                List<ReturnRawMaterialViewModel> viewModels = new List<ReturnRawMaterialViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_ALLRawMaterialReturnListview", viewModels);

            }

            return View();
        }


        public ActionResult CreateRawMaterialReturnList(long? id)
        {
            ViewBag.ddlRawmaterialName = _returnRawMaterialBusiness.GetIssueRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();
            return View();
        }


        public ActionResult GetReturnStockQty(long RawMaterialId)
        {

            var IssueStockInRMID = _mRawMaterialIssueStockDetails.GetAllRawMaterialIssueStock().Where(x => x.RawMaterialId == RawMaterialId && x.IssueStatus == "StockIn").ToList();
            var IssueStockinqty = IssueStockInRMID.Sum(c => c.Quantity);

            var IssueStockoutRMID = _mRawMaterialIssueStockDetails.GetAllRawMaterialIssueStock().Where(x => x.RawMaterialId == RawMaterialId && x.IssueStatus == "StockOut").ToList();
            var IssueStockoutqty = IssueStockoutRMID.Sum(d => d.Quantity);

            var IssueStockrmnqty = IssueStockinqty - IssueStockoutqty;


            return Json(IssueStockrmnqty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRawMaterialReturnInfo(List<ReturnRawMaterialViewModel> details)
        {
            bool IsSuccess = false;


            if (details.Count > 0)
            {
                List<ReturnRawMaterialDTO> detailsDTO = new List<ReturnRawMaterialDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _returnRawMaterialBusiness.SaveRawMaterialReturnInfo(detailsDTO, User.UserId, User.OrgId);

            }

            return Json(IsSuccess);
        }

        [HttpPost]
        public ActionResult AcceptRawMaterialReturnInfo( List<ReturnRawMaterialViewModel> details)
        {
            bool IsSuccess = false;

            if (ModelState.IsValid)
            {
           
                List<ReturnRawMaterialDTO> detailDTOs = new List<ReturnRawMaterialDTO>();
                AutoMapper.Mapper.Map(details, detailDTOs);
                IsSuccess = _returnRawMaterialBusiness.updateReturnStatus(detailDTOs, User.UserId, User.OrgId);
            }
            return Json(IsSuccess);
        }
        #endregion

        #region Requisition

        public ActionResult GetRequisition( string flag, string RequisitonCode, long? infoId,  string status, string fdate, string tdate)

        //public ActionResult GetRequisition(string flag)

        {
            if (string.IsNullOrEmpty(flag))
            {

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag=="Details" && infoId>0 )
            {
                var dto = _rawMaterialRequisitionDetailsBusiness.GetRawMaterialRequisitionDetailsbyInfo(infoId.Value, User.OrgId).Select(d => new RawMaterialRequisitionDetailsDTO
                {
                    RawMaterialId = d.RawMaterialId,
                    RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(d.RawMaterialId,User.OrgId).RawMaterialName,
                    RawMaterialRequisitionInfoId=d.RawMaterialRequisitionInfoId,
                    RequisitionQuantity=d.RequisitionQuantity,
                    IssueQuantity=d.IssueQuantity,
                    Status=d.Status,

                    Remarks=d.Remarks,
                    //EntryDate=d.EntryDate,
                    UnitID=d.UnitID,
                    UnitName=_agroUnitInfo.GetAgroInfoById(d.UnitID,User.OrgId).UnitName

                }).ToList();

                List<RawMaterialRequisitionDetailsViewModel> rawMaterialRequisitionDetailsViewModel = new List<RawMaterialRequisitionDetailsViewModel>();
                AutoMapper.Mapper.Map(dto, rawMaterialRequisitionDetailsViewModel);
                return PartialView("_GetRequisitionDetails", rawMaterialRequisitionDetailsViewModel);


            }
            
            IEnumerable<RawMaterialRequisitionInfoDTO> rawMaterialRequisitionInfoDTO = _rawMaterialRequisitionInfoBusiness.GetAllRawMaterialRequisitionInfos(RequisitonCode, status, fdate, tdate, User.OrgId).Select(r => new RawMaterialRequisitionInfoDTO {
                RawMaterialRequisitionInfoId = r.RawMaterialRequisitionInfoId,
                RawMaterialRequisitionCode = r.RawMaterialRequisitionCode,
                Status = r.Status,
                Remarks = r.Remarks,
                EntryDate = r.EntryDate,
                EntryUserId = r.EntryUserId,
                FullName =_appUserBusiness.GetAppUserOneById(r.EntryUserId.Value,User.OrgId).FullName,
                
                UpdateDate = r.UpdateDate,
                UpdateUserId = r.UpdateUserId,
 
            }).ToList();
  
            List<RawMaterialRequisitionInfoViewModel> rawMaterialRequisitionInfoViewModels = new List<RawMaterialRequisitionInfoViewModel>();
            AutoMapper.Mapper.Map(rawMaterialRequisitionInfoDTO, rawMaterialRequisitionInfoViewModels);
            return PartialView("_GetRequisition", rawMaterialRequisitionInfoViewModels);
        }
        [HttpGet]
        public ActionResult GetRequistionDetailsbyInfoId(long infoId)
        {
            var dto = _rawMaterialRequisitionDetailsBusiness.GetRawMaterialRequisitionDetailsbyInfo(infoId, User.OrgId).Select(d => new RawMaterialRequisitionDetailsDTO
            {
                RawMaterialRequisitionDetailsId=d.RawMaterialRequisitionDetailsId,
                RawMaterialId = d.RawMaterialId,
                RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(d.RawMaterialId, User.OrgId).RawMaterialName,
                RawMaterialRequisitionInfoId = d.RawMaterialRequisitionInfoId,
                RequisitionQuantity = d.RequisitionQuantity,
                IssueQuantity = d.IssueQuantity,
                AvailableQty = (_rawMaterialTrack.GetAllRawMaterialTruck().Where(x => x.RawMaterialId == d.RawMaterialId && x.IssueStatus == "StockIn").Sum(c => c.Quantity)- _rawMaterialTrack.GetAllRawMaterialTruck().Where(w => w.RawMaterialId == d.RawMaterialId && w.IssueStatus == "StockOut").Sum(o=>o.Quantity))+ _returnRawMaterialBusiness.GetAllReturnRawMaterial().Where(g => g.RawMaterialId == d.RawMaterialId && g.ReturnType == "Good").Sum(g => g.Quantity),
               
                Status = d.Status,
                Remarks = d.Remarks,
                //EntryDate=d.EntryDate,
                UnitID = d.UnitID,
                UnitName = _agroUnitInfo.GetAgroInfoById(d.UnitID, User.OrgId).UnitName

            }).ToList();

            List<RawMaterialRequisitionDetailsViewModel> rawMaterialRequisitionDetailsViewModel = new List<RawMaterialRequisitionDetailsViewModel>();
            AutoMapper.Mapper.Map(dto, rawMaterialRequisitionDetailsViewModel);
            return PartialView("_GetRequistionDetailsbyInfoId", rawMaterialRequisitionDetailsViewModel);
        }

        public ActionResult UpdateRequistion(string flag,long infoId)
        {

            var rawMaterialRequisitionInfo = _rawMaterialRequisitionInfoBusiness.GetRawMaterialRequisitionInfobyId(infoId, User.OrgId);

            RawMaterialRequisitionInfoViewModel rawMaterialRequisitionInfoViewModels = new RawMaterialRequisitionInfoViewModel()
            {

                RawMaterialRequisitionInfoId = rawMaterialRequisitionInfo.RawMaterialRequisitionInfoId,
                RawMaterialRequisitionCode = rawMaterialRequisitionInfo.RawMaterialRequisitionCode,
                Status = rawMaterialRequisitionInfo.Status,
                Type = rawMaterialRequisitionInfo.Type,
                Remarks = rawMaterialRequisitionInfo.Remarks,
                EntryDate = rawMaterialRequisitionInfo.EntryDate,
                EntryUserId = rawMaterialRequisitionInfo.EntryUserId,
                FullName = _appUserBusiness.GetAppUserOneById(rawMaterialRequisitionInfo.EntryUserId.Value,User.OrgId).FullName,
                UpdateDate = rawMaterialRequisitionInfo.UpdateDate,
                UpdateUserId = rawMaterialRequisitionInfo.UpdateUserId,

            };
     
            return View(rawMaterialRequisitionInfoViewModels);
        }



        //Production & WareHouse
        [HttpPost]
        public ActionResult SaveRequisition(RawMaterialRequisitionInfoViewModel info)
        {


            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO = new RawMaterialRequisitionInfoDTO();
                AutoMapper.Mapper.Map(info, rawMaterialRequisitionInfoDTO);
              IsSuccess=  _rawMaterialRequisitionInfoBusiness.SaveRawMaterialRequisition(rawMaterialRequisitionInfoDTO,User.UserId,User.OrgId);

            }

            return Json(IsSuccess);
        }

        //Production
        [HttpGet]
        public ActionResult SaveRequisition(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlRawMaterial = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(a => new SelectListItem { Text = a.RawMaterialName, Value = a.RawMaterialId.ToString() });
                return View();

            }
            return View();
        }
        [HttpGet]
        public ActionResult GetRequistionDetailsbyInfoIdForProduction(long infoId)
        {
            var dto = _rawMaterialRequisitionDetailsBusiness.GetRawMaterialRequisitionDetailsbyInfo(infoId, User.OrgId).Select(d => new RawMaterialRequisitionDetailsDTO
            {
                RawMaterialRequisitionDetailsId = d.RawMaterialRequisitionDetailsId,
                RawMaterialId = d.RawMaterialId,
                RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(d.RawMaterialId, User.OrgId).RawMaterialName,
                RawMaterialRequisitionInfoId = d.RawMaterialRequisitionInfoId,
                RequisitionQuantity = d.RequisitionQuantity,
                IssueQuantity = d.IssueQuantity,
                Status = d.Status,
                Remarks = d.Remarks,
                //EntryDate=d.EntryDate,
                UnitID = d.UnitID,
                UnitName = _agroUnitInfo.GetAgroInfoById(d.UnitID, User.OrgId).UnitName

            }).ToList();

            List<RawMaterialRequisitionDetailsViewModel> rawMaterialRequisitionDetailsViewModel = new List<RawMaterialRequisitionDetailsViewModel>();
            AutoMapper.Mapper.Map(dto, rawMaterialRequisitionDetailsViewModel);
            return PartialView("_GetRequistionDetailsbyInfoIdForProduction", rawMaterialRequisitionDetailsViewModel);
        }

        public ActionResult ReceivedOrCanelIssuedRawMaterial(string flag, long infoId)
        {

            var rawMaterialRequisitionInfo = _rawMaterialRequisitionInfoBusiness.GetRawMaterialRequisitionInfobyId(infoId, User.OrgId);

            RawMaterialRequisitionInfoViewModel rawMaterialRequisitionInfoViewModels = new RawMaterialRequisitionInfoViewModel()
            {

                RawMaterialRequisitionInfoId = rawMaterialRequisitionInfo.RawMaterialRequisitionInfoId,
                RawMaterialRequisitionCode = rawMaterialRequisitionInfo.RawMaterialRequisitionCode,
                Status = rawMaterialRequisitionInfo.Status,
                Type = rawMaterialRequisitionInfo.Type,
                Remarks = rawMaterialRequisitionInfo.Remarks,
                EntryDate = rawMaterialRequisitionInfo.EntryDate,
                EntryUserId = rawMaterialRequisitionInfo.EntryUserId,
                FullName = _appUserBusiness.GetAppUserOneById(rawMaterialRequisitionInfo.EntryUserId.Value, User.OrgId).FullName,
                UpdateDate = rawMaterialRequisitionInfo.UpdateDate,
                UpdateUserId = rawMaterialRequisitionInfo.UpdateUserId,

            };

            return View(rawMaterialRequisitionInfoViewModels);
        }


        //[HttpPost]
        //public ActionResult SaveRequisition(RawMaterialRequisitionInfoViewModel info)
        //{


        //    bool IsSuccess = false;
        //    if (ModelState.IsValid)
        //    {
        //        RawMaterialRequisitionInfoDTO rawMaterialRequisitionInfoDTO = new RawMaterialRequisitionInfoDTO();

        //        AutoMapper.Mapper.Map(info, rawMaterialRequisitionInfoDTO);
        //      IsSuccess=  _rawMaterialRequisitionInfoBusiness.SaveRawMaterialRequisition(rawMaterialRequisitionInfoDTO,User.UserId,User.OrgId);

        //        AutoMapper.Mapper.Map(info, rawMaterialRequisitionInfoDTO);
        //        IsSuccess = _rawMaterialRequisitionInfoBusiness.SaveRawMaterialRequisition(rawMaterialRequisitionInfoDTO, User.UserId, User.OrgId);


        //    }

        //    return Json(IsSuccess);
        //}


        public ActionResult GetRequisitionProduction(string flag, string RequisitonCode, long? infoId, string status, string fdate, string tdate)
        {
            if (string.IsNullOrEmpty(flag))
            {

                return View();
            }
            else if (!string.IsNullOrEmpty(flag) && flag == "Details" && infoId > 0)
            {
                var dto = _rawMaterialRequisitionDetailsBusiness.GetRawMaterialRequisitionDetailsbyInfo(infoId.Value, User.OrgId).Select(d => new RawMaterialRequisitionDetailsDTO
                {
                    RawMaterialId = d.RawMaterialId,
                    RawMaterialName = _rawMaterialBusiness.GetRawMaterialById(d.RawMaterialId, User.OrgId).RawMaterialName,
                    RawMaterialRequisitionInfoId = d.RawMaterialRequisitionInfoId,
                    RequisitionQuantity = d.RequisitionQuantity,
                    IssueQuantity = d.IssueQuantity,
                    Status = d.Status,
                    Remarks = d.Remarks,
                    //EntryDate=d.EntryDate,
                    UnitID = d.UnitID,
                    UnitName = _agroUnitInfo.GetAgroInfoById(d.UnitID, User.OrgId).UnitName

                }).ToList();

                List<RawMaterialRequisitionDetailsViewModel> rawMaterialRequisitionDetailsViewModel = new List<RawMaterialRequisitionDetailsViewModel>();
                AutoMapper.Mapper.Map(dto, rawMaterialRequisitionDetailsViewModel);
                return PartialView("_GetRequisitionDetails", rawMaterialRequisitionDetailsViewModel);


            }

            IEnumerable<RawMaterialRequisitionInfoDTO> rawMaterialRequisitionInfoDTO = _rawMaterialRequisitionInfoBusiness.GetAllRawMaterialRequisitionInfos(RequisitonCode, status, fdate, tdate, User.OrgId).Select(r => new RawMaterialRequisitionInfoDTO
            {
                RawMaterialRequisitionInfoId = r.RawMaterialRequisitionInfoId,
                RawMaterialRequisitionCode = r.RawMaterialRequisitionCode,
                Status = r.Status,
                Remarks = r.Remarks,
                EntryDate = r.EntryDate,
                EntryUserId = r.EntryUserId,
                FullName =_appUserBusiness.GetAppUserOneById( r.EntryUserId.Value,User.OrgId).FullName,
                UpdateDate = r.UpdateDate,
                UpdateUserId = r.UpdateUserId,

            }).ToList();

            List<RawMaterialRequisitionInfoViewModel> rawMaterialRequisitionInfoViewModels = new List<RawMaterialRequisitionInfoViewModel>();
            AutoMapper.Mapper.Map(rawMaterialRequisitionInfoDTO, rawMaterialRequisitionInfoViewModels);
            return PartialView("_GetRequisitionProduction", rawMaterialRequisitionInfoViewModels);
        }


        #endregion

        #region AgroReport
        public ActionResult GetProductwisesalesReport()
        {


            return View();
        }

        public ActionResult GetInvoiceWiseSales()
        {
            return View();
        }

        public ActionResult GetInvoiceWiseSalesReport(string fromDate, string toDate, string rptType)
        {
            var data = _agroProductSalesInfoBusiness.GetInvoiceWiseSalesReport(fromDate, toDate);

            LocalReport localReport = new LocalReport();

            string reportPath = Server.MapPath("~/Reports/ERPRpt/Agriculture/rptInvoiceWiseSalesReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
            }

            ReportDataSource dataSource1 = new ReportDataSource("dsInvoiceWiseCollectionSalesReport", data);
            localReport.DataSources.Add(dataSource1);

            string reportType = rptType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>8.27in</PageWidth>" +
                    "<PageHeight>11.69in</PageHeight>" +
                    "<MarginTop>0.25in</MarginTop>" +
                    "<MarginLeft>0.25in</MarginLeft>" +
                    "<MarginRight>0.25in</MarginRight>" +
                    "<MarginBottom>0.25in</MarginBottom>" +
                    "</DeviceInfo>";

            var renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
                );
            return File(renderedBytes, mimeType);
        }



        public ActionResult GetProductwisesalesReportDownload( string fromDate, string toDate, string rptType)
        {
            var data = _agroProductSalesInfoBusiness.GetProductwisesalesReportDownloadRpt(fromDate, toDate);
            LocalReport localReport = new LocalReport();
            string reportPath = Server.MapPath("~/Reports/ERPRpt/Agriculture/ProductwisesalesReportDownloadsReport.rdlc");
            if (System.IO.File.Exists(reportPath))
            {
                localReport.ReportPath = reportPath;
                ReportDataSource dataSource1 = new ReportDataSource("dsProductwisesalesReportDownloadReport", data);
                localReport.DataSources.Clear();
                localReport.DataSources.Add(dataSource1);
                localReport.Refresh();
                localReport.DisplayName = "Product Wise Sales Statement";

                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>PDF</OutputFormat>" +
                    "<PageWidth>8.27in</PageWidth>" +
                    "<PageHeight>11.69in</PageHeight>" +
                    "<MarginTop>0.25in</MarginTop>" +
                    "<MarginLeft>0.25in</MarginLeft>" +
                    "<MarginRight>0.25in</MarginRight>" +
                    "<MarginBottom>0.25in</MarginBottom>" +
                    "</DeviceInfo>";

                renderedBytes = localReport.Render(
                    rptType,
                     deviceInfo,
                    out mimeType,
                   
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            return new EmptyResult();
        }

        #endregion

        #region  SalesReturn

        public ActionResult SalesReturnList(string flag, long? id, string name, long? ProductId)
        {
            if (string.IsNullOrEmpty(flag))
            {
                ViewBag.ddlRawmaterialName = _returnRawMaterialBusiness.GetIssueRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();

                return View();

            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {

                var dto = _salesReturn.GetSalesReturns( ProductId ?? 0, name ?? null);

                List<SalesReturnViewModel> viewModels = new List<SalesReturnViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_SalesReturnListview", viewModels);

            }



            return View();
        }



        public ActionResult SalesReturn(long? id)
        {

            ViewBag.ddlInvoiceNo = _agroProductSalesInfoBusiness.GetAgroProductionSalesInfo(User.OrgId).Select(inv => new SelectListItem { Text = inv.InvoiceNo, Value = inv.ProductSalesInfoId.ToString() });
            return View();
        }

        public ActionResult SalesReturncreate(long? id)
        {
            var StockiestName = _stockiestInfo.GetAllStockiestSetup(User.OrgId).ToList();

            var info = _agroProductSalesInfoBusiness.GetAgroProductionInfoById(id.Value, User.OrgId);
            List<AgroProductSalesDetailsViewModel> details = new List<AgroProductSalesDetailsViewModel>();
            if (info != null)
            {


                ViewBag.Info = new AgroProductSalesInfoViewModel
                {
                    ProductSalesInfoId= info.ProductSalesInfoId,
                    StockiestName = StockiestName.FirstOrDefault(it => it.StockiestId == info.StockiestId).StockiestName,
                    InvoiceNo = info.InvoiceNo,
                    InvoiceDate = info.InvoiceDate,
                    PaidAmount = info.PaidAmount,
                    TotalAmount = info.TotalAmount,
                    DueAmount= info.DueAmount  
                   
                };

                details = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(id.Value, User.OrgId).Select(i => new AgroProductSalesDetailsViewModel
                {
                    ProductSalesDetailsId= i.ProductSalesDetailsId,
                    ProductSalesInfoId = i.ProductSalesInfoId,
                    Price = i.Price,
                    Discount = i.Discount,
                    Quanity = i.Quanity,
                    FinishGoodProductInfoId = i.FinishGoodProductInfoId,
                    FinishGoodProductName = _finishGoodProductBusiness.GetFinishGoodProductById(i.FinishGoodProductInfoId, User.OrgId).FinishGoodProductName,
                    MeasurementId = i.MeasurementId,
                    MeasurementName = _measuremenBusiness.GetMeasurementById(i.MeasurementId, User.OrgId).MeasurementName,
                    MeasurementSize = i.MeasurementSize,
                    
                }).ToList();

            }
            else
            {
                ViewBag.Info = new AgroProductSalesInfoViewModel();
            }
            return PartialView("_GetAgroSalesReturn", details);
        }


        public ActionResult SaveSalesReturn(List<SalesReturnViewModel> details)
        {
            bool IsSuccess = false;


            if (details.Count > 0)
            {
                List<SalesReturnDTO> detailsDTO = new List<SalesReturnDTO>();
                AutoMapper.Mapper.Map(details, detailsDTO);
                IsSuccess = _salesReturn.SaveSalesReturn(detailsDTO, User.UserId);

            }

            return Json(IsSuccess);
        }


        #region  extracode
        //public ActionResult getproduct(long id)

        //{
        //    var product = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(id, User.OrgId).Select(a => new AgroProductSalesDetailsViewModel
        //    {

        //        FinishGoodProductInfoId = a.FinishGoodProductInfoId,
        //        FinishGoodProductName = _finishGoodProductBusiness.GetFinishGoodProductById(a.FinishGoodProductInfoId, User.OrgId).FinishGoodProductName

        //    }).ToList();

        //    var ddlProductList = product.GroupBy(t => t.FinishGoodProductInfoId).Select(g => g.First()).Select(p => new SelectListItem { Text = p.FinishGoodProductName, Value = p.FinishGoodProductInfoId.ToString() }).ToList();

        //    if (ddlProductList.Count > 0 && ddlProductList != null)
        //    {
        //        return Json(new { flag = "1", msg = "Product found", data = ddlProductList }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { flag = "0", msg = "Product not found" }, JsonRequestBehavior.AllowGet);
        //    }

        //}


        //public ActionResult GetMeasurment(long Invoiceid, long Productid)

        //{
        //    var ExactsalesInfodetails = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(Invoiceid, User.OrgId).Select(b => new AgroProductSalesDetailsViewModel
        //    {
        //        MeasurementId = b.MeasurementId,
        //        MeasurementName = _measuremenBusiness.GetMeasurementById(b.MeasurementId, User.OrgId).MeasurementName,
        //        FinishGoodProductInfoId = b.FinishGoodProductInfoId
        //    }).ToList();

        //    //var ExactsalesInfodetails = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(Invoiceid, User.OrgId).ToList();

        //    var ExactProduct = ExactsalesInfodetails.Where(x => x.FinishGoodProductInfoId == Productid).ToList();

        //    var MeasurmentList = ExactProduct.Select(m => new SelectListItem { Text = m.MeasurementName, Value = m.MeasurementId.ToString() }).ToList();

        //    if (MeasurmentList.Count > 0 && MeasurmentList != null)
        //    {
        //        return Json(new { flag = "1", msg = "Product found", data = MeasurmentList }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { flag = "0", msg = "Product not found" }, JsonRequestBehavior.AllowGet);
        //    }


        //}

        #endregion



        #endregion

        #region  SalesReturnAdjust
        public ActionResult SalesReturnAdjustCreate(long? id)
        {

            ViewBag.ddlstokiestname = _stockiestInfo.GetAllStockiestSetup(User.OrgId).Select(stk => new SelectListItem { Text = stk.StockiestName, Value = stk.StockiestId.ToString() });
            return View();
        }

        public ActionResult getinvoice(long id)

        {

            var invoicelist = _agroProductSalesInfoBusiness.GetAgroSalesinfoByStokiestId(id).Select(a => new SelectListItem { Text = a.InvoiceNo, Value = a.ProductSalesInfoId.ToString() }).ToList();

            if (invoicelist.Count > 0 && invoicelist != null)
            {
                return Json(new { flag = "1", msg = "Invoice found", data = invoicelist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { flag = "0", msg = "Invoice not found" }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult GetStokistTotalbill(long id)
        {
            var stokiestid = _agroProductSalesInfoBusiness.GetAgroSalesinfoByStokiestId(id).Where(x => x.StockiestId == id).ToList();
            var totalbill = stokiestid.Sum(y=> y.TotalAmount);
            return Json(totalbill, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetStokistTotalPaid(long id)
        {
            var stokiestid = _agroProductSalesInfoBusiness.GetAgroSalesinfoByStokiestId(id).Where(p => p.StockiestId == id).ToList();
            var totalpaid = stokiestid.Sum(pa => pa.PaidAmount);
            return Json(totalpaid, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetStokistTotalDue(long id)
        {
            var stokiestid = _agroProductSalesInfoBusiness.GetAgroSalesinfoByStokiestId(id).Where(d => d.StockiestId == id).ToList();
            var totaldue = stokiestid.Sum(du => du.DueAmount);
            return Json(totaldue, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SalesReturnAdjustCreateTbl(long? ProductSalesInfoId)
        {
           

            List<SalesReturnViewModel> details = new List<SalesReturnViewModel>();
            details = _salesReturn.GetSalesSalesReturnByInfoIdNotAdjust(ProductSalesInfoId).Select(i => new SalesReturnViewModel

            {
                SalesReturnId = i.SalesReturnId,
               ReturnQuanity = i.ReturnQuanity,
               ReturnPerUnitPrice = i.ReturnPerUnitPrice,
               ReturnTotalPrice = i.ReturnTotalPrice,
               FinishGoodProductInfoId = i.FinishGoodProductInfoId,
               FinishGoodProductName = _finishGoodProductBusiness.GetFinishGoodProductById(i.FinishGoodProductInfoId, User.OrgId).FinishGoodProductName,
                MeasurementId = i.MeasurementId,
                MeasurementName = _measuremenBusiness.GetMeasurementById(i.MeasurementId, User.OrgId).MeasurementName,
                MeasurementSize = i.MeasurementSize,
                ReturnDate = i.ReturnDate

            }).ToList();


            return PartialView("_GetAgroSalesReturnAdjust", details);
        }

        [HttpPost]
        public ActionResult ADJUSTSalesReturn(List<SalesReturnViewModel> details)
        {
            bool IsSuccess = false;

            if (ModelState.IsValid)
            {

                List<SalesReturnDTO> detailDTOs = new List<SalesReturnDTO>();
                AutoMapper.Mapper.Map(details, detailDTOs);
                IsSuccess = _salesReturn.updateadjustsales(detailDTOs);
               
            }
            return Json(IsSuccess);
        }
        #endregion
        #region Commision
        public ActionResult ProductCommision()
        {
            return View();
        }
        #endregion


    }
}