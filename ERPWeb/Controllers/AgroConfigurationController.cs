using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    public class AgroConfigurationController : BaseController
    {
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        private readonly IRawMaterialStockDetail _rawMaterialStockDetail;
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
        


        public AgroConfigurationController(ERPBLL.ControlPanel.Interface.IOrganizationBusiness organizationBusiness, IDepotSetup depotSetup, IRawMaterialBusiness rawMaterialBusiness, IFinishGoodProductBusiness finishGoodProductBusiness, IBankSetup bankSetup, IFinishGoodProductSupplierBusiness finishGoodProductSupplierBusiness, IMeasuremenBusiness measuremenBusiness, IRawMaterialSupplier rawMaterialSupplierBusiness, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IRawMaterialStockDetail rawMaterialStockDetail)
        {
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._rawMaterialStockDetail = rawMaterialStockDetail;
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
                return View();

            }
            else
            {
                var measureMent = _measuremenBusiness.GetMeasurementSetups(User.OrgId);
                List<MeasurementSetupViewModel> viewModels = new List<MeasurementSetupViewModel>();
                AutoMapper.Mapper.Map(measureMent,viewModels);


                return PartialView("_GetMeasurementList", viewModels);
            }

   
        }
        public ActionResult SaveMeasurement(List< MeasurementSetupViewModel> models )
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                List<MeasurementSetupDTO> measurementSetupDTOs = new List<MeasurementSetupDTO>();
                AutoMapper.Mapper.Map(models,measurementSetupDTOs);
             IsSuccess=   _measuremenBusiness.SaveMeasureMent(measurementSetupDTOs, User.OrgId);


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
                IsSuccess = _measuremenBusiness.UpdateMeasureMent(measurementSetupDTO,User.UserId,User.OrgId);


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
        public ActionResult GetRawMaterialStock(string flag, long? rawMaterialId, long? id)
        {
            ViewBag.UserPrivilege = UserPrivilege("AgroConfiguration", "GetRawMaterialStock");

            if (string.IsNullOrEmpty(flag)) { 

                ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialBusiness.GetRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

                return View();
            }

            else if (!string.IsNullOrEmpty(flag) && flag == Flag.View)
            {
                var dto = _rawMaterialStockInfo.GetRawMaterialStockInfos(User.OrgId, rawMaterialId ?? 0);


                List<RawMaterialStockInfoViewModel> viewModels = new List<RawMaterialStockInfoViewModel>();
                AutoMapper.Mapper.Map(dto, viewModels);
                return PartialView("_GetRawMaterialStockList", viewModels);
            }
            else if (!string.IsNullOrEmpty(flag) && flag == Flag.Detail)
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
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId ==i.RawMaterialId).RawMaterialName,
                       Quantity =i.Quantity,
                       Unit=i.Unit
                    }).ToList();
                }
                else
                {
                    ViewBag.Info = new RawMaterialStockInfoViewModel();
                }
                return PartialView("_GetRawMaterialStockDetail", details);
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
                var RawMaterialNames=_rawMaterialBusiness.GetRawMaterialByOrgId(User.OrgId).ToList();
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
                        FGRRawMaterQty=i.FGRRawMaterQty,
                        FGRRawMaterUnit=i.FGRRawMaterUnit
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
                        FGRId=info.FGRId,
                        FGRQty = info.FGRQty,
                        FGRUnit = info.FGRUnit,
                        FinishGoodProductId=info.FinishGoodProductId,
                    };
                    details = _finishGoodRecipeDetailsBusiness.GetFinishGoodRecipeDetailsByInfoId(id.Value, User.OrgId).Select(i => new FinishGoodRecipeDetailsViewModel
                    {
                        RawMaterialName = RawMaterialNames.FirstOrDefault(w => w.RawMaterialId == i.RawMaterialId).RawMaterialName,
                        FGRRawMaterQty = i.FGRRawMaterQty,
                        FGRRawMaterUnit = i.FGRRawMaterUnit,
                        FGRDetailsId=i.FGRDetailsId,
                   
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
        public ActionResult GetRawMaterialIssueStock()
        {


            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialStockInfo.GetDepotRawMaterials(User.OrgId).Select(des => new SelectListItem { Text = des.text, Value = des.value.ToString() }).ToList();


            return View();
        }

        public ActionResult CreateRawMaterialIssueStock(long? id)
        {
            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            //ViewBag.ddlProductName = _finishGoodProductBusiness.GetProductNameByOrgId(User.OrgId).Select(d => new SelectListItem { Text = d.FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();

            ViewBag.ddlRawMaterialName = _rawMaterialStockInfo.GetCheckExpairDatewiseRawMaterials(User.OrgId).Select(org => new SelectListItem { Text = org.RawMaterialName, Value = org.RawMaterialId.ToString() }).ToList();

            return View();
        }

        public ActionResult GetProductFinishGoodList()
        {


            ViewBag.ddlOrganizationName = _organizationBusiness.GetAllOrganizations().Where(o => o.OrganizationId == 9).Select(org => new SelectListItem { Text = org.OrganizationName, Value = org.OrganizationId.ToString() }).ToList();

            ViewBag.ddlProduct = _finishGoodRecipeInfoBusiness.GetAllFinishGoodReceif(User.OrgId).Select(d => new SelectListItem { Text = _finishGoodProductBusiness.GetFinishGoodProductById(d.FinishGoodProductId,User.OrgId).FinishGoodProductName, Value = d.FinishGoodProductId.ToString() }).ToList();

            return View();
        }
        #endregion



    }
}