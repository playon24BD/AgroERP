using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPDAL.AgricultureDAL;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class AgroProductSalesInfoBusiness : IAgroProductSalesInfoBusiness
    {

        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AgroProductSalesInfoRepository _agroProductSalesInfoRepository;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;

        //private readonly AppUserRepository appUserRepository; // repo
        private readonly IAppUserBusiness _appUserBusiness;
        private readonly IStockiestInfo _stockiestInfo;
        private readonly ITerritorySetup _territorySetup;
        private readonly IAreaSetupBusiness _areaSetupBusiness;
        private readonly IDivisionInfo _divisionInfo;
        private readonly IRegionSetup _regionSetup;
        private readonly IZoneSetup _zoneSetup;
        private readonly IUserAssignBussiness _userAssignBussiness;
        private readonly IUserInfo _userInfo;
        private readonly IFinishGoodRecipeInfoBusiness _finishGoodRecipeInfoBusiness;
        private readonly IAgroUnitInfo _agroUnitInfo;
        private readonly IMeasuremenBusiness _measuremenBusiness;
        private readonly ICommissionOnProductOnSalesBusiness _commissionOnProductOnSalesBusiness;
        private readonly IStockiestUserBusiness _stockiestUserBusiness;

        public AgroProductSalesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAppUserBusiness appUserBusiness, IStockiestInfo stockiestInfo, ITerritorySetup territorySetup, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IUserAssignBussiness userAssignBussiness, IUserInfo userInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IAgroUnitInfo agroUnitInfo, IMeasuremenBusiness measuremenBusiness,ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness, IStockiestUserBusiness stockiestUserBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);
            this._appUserBusiness = appUserBusiness;
            this._stockiestInfo = stockiestInfo;
            this._territorySetup = territorySetup;
            this._areaSetupBusiness = areaSetupBusiness;
            this._divisionInfo = divisionInfo;
            this._regionSetup = regionSetup;
            this._zoneSetup = zoneSetup;
            this._userAssignBussiness = userAssignBussiness;
            this._userInfo = userInfo;
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
            this._agroUnitInfo = agroUnitInfo;
            this._measuremenBusiness = measuremenBusiness;
            this._commissionOnProductOnSalesBusiness = commissionOnProductOnSalesBusiness;
            this._stockiestUserBusiness = stockiestUserBusiness;
        }



        public AgroProductSalesInfo CheckBYProductSalesInfoId(long? ProductSalesInfoId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(i => i.ProductSalesInfoId == ProductSalesInfoId);
        }

        public AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(f => f.ProductSalesInfoId == id);
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId)
        {
            return _agroProductSalesInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(stockiestId, invoiceNo, fromDate, toDate)).ToList();
        }

        private string QueryForAgroSalesInfoss(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            //param += string.Format(@" and sales.OrganizationId={0}", orgId);
            if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and sales.StockiestId={0}", stockiestId);
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                param += string.Format(@"and sales.InvoiceNo like '%{0}%'", invoiceNo);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date)='{0}'", tDate);
            }


            query = string.Format(@"	select sales.TotalAmount,sales.DueAmount,sales.PaidAmount,sales.InvoiceNo,sales.ProductSalesInfoId,CONVERT(date,sales.InvoiceDate)as InvoiceDate,stock.StockiestName
                from tblProductSalesInfo sales
                inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId 
                --select sales.InvoiceNo,sales.InvoiceDate,stock.StockiestName
                --from tblProductSalesInfo sales
                --inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId 

                Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }


        public IEnumerable<AgroProductSalesInfo> GetUserName(long orgId)
        {
            throw new NotImplementedException();
        }
        // Working here

        public bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId)
        {

            bool isSuccess = false;

            var ChallanNo = "CHA-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            var InvoiceNo = "INV-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");



            if (agroSalesInfoDTO.ProductSalesInfoId == 0)
            {
                var UserId= _stockiestUserBusiness.GetStockiestInfoById(agroSalesInfoDTO.UserId, orgId).UserId;
                var stockeiestId = _appUserBusiness.GetId(UserId, orgId).StockiestId;
                long stockId = agroSalesInfoDTO.UserId;

                var territoryId = _stockiestInfo.GetStockiestInfoById(stockId, orgId).TerritoryId;

                var areaId = _territorySetup.GetTerritoryNamebyId(territoryId, orgId).AreaId;

                var regionId = _areaSetupBusiness.GetAreaInfoById(areaId, orgId).RegionId;

                var divisionId = _regionSetup.GetRegionNamebyId(regionId, orgId).DivisionId;

                var zoneId = _divisionInfo.GetDivisionInfoById(divisionId, orgId).ZoneId;


                //paymenttable
                if (agroSalesInfoDTO.PaymentMode == "Cash")
                {

                    AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
                    {
                        //Info
                        ChallanNo = ChallanNo,
                        InvoiceNo = InvoiceNo,
                        VehicleNumber = agroSalesInfoDTO.VehicleNumber,
                        ProductSalesInfoId = agroSalesInfoDTO.ProductSalesInfoId,
                        DeliveryPlace = agroSalesInfoDTO.DeliveryPlace,
                        DoADO_Name = agroSalesInfoDTO.DoADO_Name,
                        DriverName = agroSalesInfoDTO.DriverName,
                        InvoiceDate = agroSalesInfoDTO.InvoiceDate,
                        PaymentMode = agroSalesInfoDTO.PaymentMode,
                        VehicleType = agroSalesInfoDTO.VehicleType,
                        UserAssignId = agroSalesInfoDTO.UserAssignId,
                        UserId = UserId,
                        StockiestId = stockId,
                        TerritoryId = territoryId,
                        AreaId = areaId,
                        DivisionId = divisionId,
                        RegionId = regionId,
                        ZoneId = zoneId,
                        ChallanDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        OrganizationId = orgId,
                        Depot = agroSalesInfoDTO.Depot,
                        Do_ADO_DA = agroSalesInfoDTO.Do_ADO_DA,
                        TotalAmount = agroSalesInfoDTO.TotalAmount,
                        PaidAmount = agroSalesInfoDTO.TotalAmount,
                        DueAmount = 0

                    };


                    List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();

                    foreach (var item in details)
                    {
                        //double ProductMesurement = 0;
                        //double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
                        //double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
                        //double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;
                        var UnitQtys = item.QtyKG.Split('(', ')');
                        int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
                        string ProductUnit = UnitQtys[1];
                        //if (MasterCartonMasurement != 0)
                        //{
                        //     ProductMesurement = MasterCartonMasurement * InnerBoxMasurement ;
                        //}
                        //else
                        //{
                        //     ProductMesurement = InnerBoxMasurement;
                        //}
                        //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;



                        var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
                        //var receipeBatch=_finishGoodRecipeInfoBusiness
                        //var receipeBatch = item.ReceipeBatchCode.Split('(',')');
                        var FGRId = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).FGRId;
                        var receipeBatch = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).ReceipeBatchCode;

                        AgroProductSalesDetails agroSalesDetails = new AgroProductSalesDetails()
                        {
                            //Details
                            Discount = item.Discount,
                            DiscountTk = item.DiscountTk,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            MeasurementId = item.MeasurementId,
                            MeasurementSize = item.MeasurementSize,
                            OrganizationId = orgId,
                            Price = item.Price,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            Quanity = item.Quanity,
                            FinishGoodProductInfoId = item.FinishGoodProductInfoId,
                            ProductSalesDetailsId = item.ProductSalesDetailsId,
                            ReceipeBatchCode = receipeBatch,
                            FGRId = FGRId,
                            QtyKG = item.QtyKG,
                            BoxQuanity = item.BoxQuanity




                        };
                        agroDetails.Add(agroSalesDetails);
                    }
                    agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
                    _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);



                    isSuccess = _agroProductSalesInfoRepository.Save();

                    if (isSuccess)
                    {
                        //Commission on Sales 

                        isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);

                    }


                    SalesPaymentRegister salesPayment = new SalesPaymentRegister
                    {
                        PaymentDate = DateTime.Now,
                        PaymentAmount = agroSalesInfoDTO.TotalAmount,
                        ProductSalesInfoId = agroSalesProductionInfo.ProductSalesInfoId,
                        Remarks = "SalesTime",
                        EntryUserId = userId
                    };
                    //paymenttable
                    _salesPaymentRegisterRepository.Insert(salesPayment);
                    isSuccess = _salesPaymentRegisterRepository.Save();
                }

                else
                {
                    AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
                    {
                        //Info
                        ChallanNo = ChallanNo,
                        InvoiceNo = InvoiceNo,
                        VehicleNumber = agroSalesInfoDTO.VehicleNumber,
                        ProductSalesInfoId = agroSalesInfoDTO.ProductSalesInfoId,
                        DeliveryPlace = agroSalesInfoDTO.DeliveryPlace,
                        DoADO_Name = agroSalesInfoDTO.DoADO_Name,
                        DriverName = agroSalesInfoDTO.DriverName,
                        InvoiceDate = agroSalesInfoDTO.InvoiceDate,
                        PaymentMode = agroSalesInfoDTO.PaymentMode,
                        VehicleType = agroSalesInfoDTO.VehicleType,
                        UserAssignId = agroSalesInfoDTO.UserAssignId,
                        UserId = agroSalesInfoDTO.UserId,
                        StockiestId = stockId,
                        TerritoryId = territoryId,
                        AreaId = areaId,
                        DivisionId = divisionId,
                        RegionId = regionId,
                        ZoneId = zoneId,
                        ChallanDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        OrganizationId = orgId,
                        Depot = agroSalesInfoDTO.Depot,
                        Do_ADO_DA = agroSalesInfoDTO.Do_ADO_DA,
                        TotalAmount = agroSalesInfoDTO.TotalAmount,
                        PaidAmount = 0,
                        DueAmount = agroSalesInfoDTO.TotalAmount

                    };
                    List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();

                    foreach (var item in details)
                    {
                        //double ProductMesurement = 0;
                        //double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
                        //double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
                        //double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;
                        var UnitQtys = item.QtyKG.Split('(', ')');
                        int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
                        string ProductUnit = UnitQtys[1];
                        //if (MasterCartonMasurement != 0)
                        //{
                        //     ProductMesurement = MasterCartonMasurement * InnerBoxMasurement ;
                        //}
                        //else
                        //{
                        //     ProductMesurement = InnerBoxMasurement;
                        //}
                        //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;



                        var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
                        //var receipeBatch=_finishGoodRecipeInfoBusiness
                        //var receipeBatch = item.ReceipeBatchCode.Split('(',')');
                        var FGRId = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).FGRId;
                        var receipeBatch = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).ReceipeBatchCode;

                        AgroProductSalesDetails agroSalesDetails = new AgroProductSalesDetails()
                        {
                            //Details
                            Discount = item.Discount,
                            DiscountTk = item.DiscountTk,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            MeasurementId = item.MeasurementId,
                            MeasurementSize = item.MeasurementSize,
                            OrganizationId = orgId,
                            Price = item.Price,
                            ProductSalesInfoId = item.ProductSalesInfoId,
                            Quanity = item.Quanity,
                            FinishGoodProductInfoId = item.FinishGoodProductInfoId,
                            ProductSalesDetailsId = item.ProductSalesDetailsId,
                            ReceipeBatchCode = receipeBatch,
                            FGRId = FGRId,
                            QtyKG = item.QtyKG,
                            BoxQuanity = item.BoxQuanity




                        };
                        agroDetails.Add(agroSalesDetails);
                    }
                    agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
                    _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);



                    isSuccess = _agroProductSalesInfoRepository.Save();
                    if (isSuccess)
                    {
                        //Commission on Sales 

                        isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);

                    }


                    SalesPaymentRegister salesPayment = new SalesPaymentRegister
                    {
                        PaymentDate = DateTime.Now,
                        PaymentAmount = 0,
                        ProductSalesInfoId = agroSalesProductionInfo.ProductSalesInfoId,
                        Remarks = "SalesTime",
                        EntryUserId = userId
                    };
                    //paymenttable
                    _salesPaymentRegisterRepository.Insert(salesPayment);
                    isSuccess = _salesPaymentRegisterRepository.Save();
                }


            }

            return isSuccess;
        }

        public IEnumerable<ProductSalesDataReport> GetProductSalesData(string InvoiceNo)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<ProductSalesDataReport>(QueryProductSalesReport(InvoiceNo));
        }

        private string QueryProductSalesReport(string InvoiceNo)
        {
            string param = string.Empty;
            string query = string.Empty;


            if (!string.IsNullOrEmpty(InvoiceNo))
            {
                param += string.Format(@"and sales.InvoiceNo ='{0}'", InvoiceNo);
            }
            //if (!string.IsNullOrEmpty(status))
            //{
            //    param += string.Format(@"and Taskstatus ='{0}'", status);
            //}

            query = string.Format(@"select DISTINCT 
            AU.FullName,
            ST.StockiestName,
            TE.TerritoryName,
            AU.Address,
            AU.MobileNo,
            sales.InvoiceNo,
            CONVERT(date,sales.InvoiceDate) as InvoiceDate,
            sales.ChallanNo,
            CONVERT(date,sales.ChallanDate) as ChallanDate,
            sales.Depot,
            sales.VehicleType,
            sales.VehicleNumber,
            sales.DriverName,
            sales.DeliveryPlace,
            sales.Do_ADO_DA,
            sales.DoADO_Name,
            sales.PaymentMode,

            ZoneName=(select Z.ZoneName from [Agriculture].[dbo].[tblZoneInfo] Z where Z.ZoneId=sales.ZoneId),

            DivisionName=(select DIV.DivisionName from [Agriculture].[dbo].[tblDivisionInfo] DIV where DIV.DivisionId=sales.DivisionId),

            RegionName=(select R.RegionName from [Agriculture].[dbo].[tblRegionInfos] R where R.RegionId=sales.RegionId),

            AreaName=(select A.AreaName from [Agriculture].[dbo].[tblAreaSetup] A where A.AreaId=sales.AreaId),

            salesD.ProductSalesInfoId,
            salesD.FinishGoodProductInfoId,
            FGPN.FinishGoodProductName,

            salesD.Quanity,
            salesD.Price,
            salesD.MeasurementSize,
            salesD.Discount,
            salesD.DiscountTk,
            sales.PaidAmount,
            sales.DueAmount,
            (salesD.Price*salesD.Quanity) AS Total,
            sales.TotalAmount,
            dbo.fnIntegerToWords(TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText




            from [Agriculture].[dbo].[tblProductSalesInfo] sales
            INNER JOIN [Agriculture].[dbo].[tblProductSalesDetails] salesD
            on sales.ProductSalesInfoId=salesD.ProductSalesInfoId

            INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN
            on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId

            LEFT JOIN [ControlPanelAgro].[dbo].[tblApplicationUsers] AU
            on AU.UserId=sales.UserId
            LEFT JOIN [Agriculture].[dbo].[tblStockiestInfo] ST
            on ST.StockiestId=AU.StockiestId
            LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
            on TE.TerritoryId=ST.TerritoryId

                        Where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }


        public IEnumerable<AgroProductSalesInfo> GetAllDueSalesInvoice()
        {
            return _agroProductSalesInfoRepository.GetAll(z => z.PaidAmount < z.TotalAmount).ToList();
        }

        public AgroProductSalesInfo GetInvoiceProductionInfoById(long ProductSalesInfoId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(f => f.ProductSalesInfoId == ProductSalesInfoId);
        }

        public AgroProductSalesInfo GetChallanProductionInfoById(long ProductSalesInfoId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(f => f.ProductSalesInfoId == ProductSalesInfoId);
        }

        public IEnumerable<ProductSalesDataChallanReport> GetProductSalesChallanData(string ChallanNo)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<ProductSalesDataChallanReport>(QueryProductSalesChallanReport(ChallanNo));
        }

        private string QueryProductSalesChallanReport(string ChallanNo)
        {
            string param = string.Empty;
            string query = string.Empty;


            if (!string.IsNullOrEmpty(ChallanNo))
            {
                param += string.Format(@"and sales.ChallanNo ='{0}'", ChallanNo);
            }
            //if (!string.IsNullOrEmpty(status))
            //{
            //    param += string.Format(@"and Taskstatus ='{0}'", status);
            //}

            query = string.Format(@"select DISTINCT 
            AU.FullName,
            ST.StockiestName,
            TE.TerritoryName,
            AU.Address,
            AU.MobileNo,
            sales.InvoiceNo,
            CONVERT(date,sales.InvoiceDate) as InvoiceDate,
            sales.ChallanNo,
            CONVERT(date,sales.ChallanDate) as ChallanDate,
            sales.Depot,
            sales.VehicleType,
            sales.VehicleNumber,
            sales.DriverName,
            sales.DeliveryPlace,
            sales.Do_ADO_DA,
            sales.DoADO_Name,
            sales.PaymentMode,

            ZoneName=(select Z.ZoneName from [Agriculture].[dbo].[tblZoneInfo] Z where Z.ZoneId=sales.ZoneId),

            DivisionName=(select DIV.DivisionName from [Agriculture].[dbo].[tblDivisionInfo] DIV where DIV.DivisionId=sales.DivisionId),

            RegionName=(select R.RegionName from [Agriculture].[dbo].[tblRegionInfos] R where R.RegionId=sales.RegionId),

            AreaName=(select A.AreaName from [Agriculture].[dbo].[tblAreaSetup] A where A.AreaId=sales.AreaId),

            salesD.ProductSalesInfoId,
            salesD.FinishGoodProductInfoId,
            FGPN.FinishGoodProductName,

            salesD.Quanity,
            salesD.Price,
            salesD.MeasurementSize,
            salesD.Discount,
            salesD.DiscountTk,
            sales.PaidAmount,
            sales.DueAmount,
            (salesD.Price*salesD.Quanity) AS Total,
            sales.TotalAmount,
            dbo.fnIntegerToWords(TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText




            from [Agriculture].[dbo].[tblProductSalesInfo] sales
            INNER JOIN [Agriculture].[dbo].[tblProductSalesDetails] salesD
            on sales.ProductSalesInfoId=salesD.ProductSalesInfoId

            INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN
            on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId

            LEFT JOIN [ControlPanelAgro].[dbo].[tblApplicationUsers] AU
            on AU.UserId=sales.UserId
            LEFT JOIN [Agriculture].[dbo].[tblStockiestInfo] ST
            on ST.StockiestId=AU.StockiestId
            LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
            on TE.TerritoryId=ST.TerritoryId

            Where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<InvoiceWiseCollectionSalesReport> GetInvoiceWiseSalesReport(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<InvoiceWiseCollectionSalesReport>(QueryForInvoiceWiseSalesReport(stockiestId, invoiceNo,fromDate, toDate));
        }
        public string QueryForInvoiceWiseSalesReport(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            string param = string.Empty;
            string query = string.Empty;

            if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and STI.StockiestId={0}", stockiestId);
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                param += string.Format(@"and SI.InvoiceNo like '%{0}%'", invoiceNo);
            }


            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(SI.InvoiceDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(SI.InvoiceDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(SI.InvoiceDate as date)='{0}'", tDate);
            }



            query = string.Format(@"
                    SELECT  todate='" + fromDate + "', fromDate='" + toDate + "',SI.ProductSalesInfoId, A.AreaName,ZoneUserName =( SELECT distinct Concat(AU.FullName,'( ',AU.Desigation,' )') AS FullName FROM   [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), ZoneUserMobile=( SELECT distinct AU.MobileNo FROM  [Agriculture].[dbo].tblProductSalesInfo SI  INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), TI.TerritoryName, TerritoryUserName=(SELECT distinct Concat(AUT.FullName ,'( ',AUT.Desigation,' )') AS FullName FROM  [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId=AUT.UserId ), TerritoryUserMobile=(SELECT distinct AUT.MobileNo FROM  [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId=AUT.UserId ), STI.StockiestName, SI.InvoiceNo, CONVERT(date,SI.InvoiceDate) AS InvoiceDate, SI.TotalAmount AS InvoiceTk, Collaction=ISNULL((SELECT Sum(PSPH.PaymentAmount) FROM tblProductSalesPaymentHistory PSPH Where  SI.ProductSalesInfoId=PSPH.ProductSalesInfoId),0),  SI.DueAmount, DiscountTk=ISNULL((SELECT Sum(PSD.DiscountTk) FROM tblProductSalesDetails PSD Where        SI.ProductSalesInfoId=PSD.ProductSalesInfoId),0), CONVERT(date,PsPH.PaymentDate) AS PaymentDate,  PsPH.Remarks,  PsPH.PaymentAmount FROM [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblAreaSetup A on SI.AreaId=A.AreaId INNER JOIN [Agriculture].[dbo].tblTerritoryInfos TI on SI.TerritoryId=TI.TerritoryId INNER JOIN [Agriculture].[dbo].tblStockiestInfo STI on SI.StockiestId=STI.StockiestId  INNER JOIN [Agriculture].[dbo].tblProductSalesPaymentHistory PsPH on     SI.ProductSalesInfoId=PsPH.ProductSalesInfoId where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<ProductWiseSalesStementReport> GetProductwisesalesReportDownloadRpt(long? productId, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<ProductWiseSalesStementReport>(QueryProductWiseSalesStementReport(productId,fromDate, toDate));
        }

        private string QueryProductWiseSalesStementReport(long? productId, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            if (productId != 0 && productId > 0)
            {
                param += string.Format(@" and FGPN.FinishGoodProductId={0}", productId);
            }

            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }



            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                FromDate += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                ToDate += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", tDate);
            }

            query = string.Format(@"SELECT DISTINCT todate='"+fromDate+"', fromDate='"+toDate+"',FGPN.FinishGoodProductName, M.MeasurementName AS PackSize, QtyCTN=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId),QtyKG=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId) * M.UnitKG,Total=(SELECT SUM(sd.Price) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId)FROM [Agriculture].[dbo].[tblProductSalesDetails] salesD INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId INNER JOIN [Agriculture].[dbo].[tblMeasurement] M on salesD.MeasurementId=M.MeasurementId  Inner Join [Agriculture].[dbo].[tblProductSalesInfo] sales  on sales.ProductSalesInfoId=salesD.ProductSalesInfoId Where 1=1{0} Group by FGPN.FinishGoodProductName, M.MeasurementName,salesD.MeasurementId,salesD.FinishGoodProductInfoId,  salesD.Quanity,M.UnitKG,salesD.Price,salesD.EntryDate,sales.TotalAmount,salesD.ProductSalesInfoId", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroSalesinfoByStokiestId(long StockiestId)
        {
            return _agroProductSalesInfoRepository.GetAll(d => d.StockiestId == StockiestId);

        }

        public IEnumerable<AgroProductSalesInfoDTO> GetPaymentListInfos(string name, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryPaymentListReport(name, fromDate, toDate));
        }

        private string QueryPaymentListReport(string name, string fromDate, string toDate)
        {
            string param = string.Empty;
            string query = string.Empty;


            if (!string.IsNullOrEmpty(name))
            {
                param += string.Format(@"and InvoiceNo like '%{0}%'", name);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(sales.InvoiceDate as date)='{0}'", tDate);
            }


            query = string.Format(@"
                   select DISTINCT 
		    AU.FullName,
            ST.StockiestName,
            TE.TerritoryName,
            AU.Address,
            AU.MobileNo,
            sales.InvoiceNo,
            CONVERT(date,sales.InvoiceDate) as InvoiceDate,
            sales.ChallanNo,
            CONVERT(date,sales.ChallanDate) as ChallanDate,
            sales.Depot,
            sales.VehicleType,
            sales.VehicleNumber,
            sales.DriverName,
            sales.DeliveryPlace,
            sales.Do_ADO_DA,
            sales.DoADO_Name,
            sales.PaymentMode,
            sales.PaidAmount,
            sales.DueAmount,
            sales.TotalAmount,
			sales.ProductSalesInfoId
            from [Agriculture].[dbo].[tblProductSalesInfo] sales
			 LEFT JOIN [ControlPanelAgro].[dbo].[tblApplicationUsers] AU
            on AU.UserId=sales.UserId
            LEFT JOIN [Agriculture].[dbo].[tblStockiestInfo] ST
            on ST.StockiestId=AU.StockiestId
            LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
            on TE.TerritoryId=ST.TerritoryId


            Where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }


        public IEnumerable<AgroProductSalesInfoDTO> GetSalesAdjustInfos(string invoiceNo, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForSalesReturnAdjust(invoiceNo,fromDate,toDate)).ToList();
        }

        private string QueryForSalesReturnAdjust(string invoiceNo, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                param += string.Format(@"and i.InvoiceNo like '%{0}%'", invoiceNo);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(i.InvoiceDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(i.InvoiceDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(i.InvoiceDate as date)='{0}'", tDate);
            }


            query = string.Format(@"select distinct isnull((r.SalesReturnId),0) as SalesReturnId, i.ProductSalesInfoId, i.InvoiceNo,convert(date,i.InvoiceDate) as InvoiceDate, 
StockiestName=(select StockiestName from tblStockiestInfo where StockiestId=i.StockiestId),
i.TotalAmount,i.DueAmount,i.PaidAmount,
 
 AdjustTotalReturn = isnull( (select sum(sr.ReturnTotalPrice) from tblSalesReturn sr 
 where sr.ProductSalesInfoId= i.ProductSalesInfoId and sr.Status='ADJUST'),0),

 NotAdjustTotalReturn = isnull( (select sum(sr.ReturnTotalPrice) from tblSalesReturn sr 
 where sr.ProductSalesInfoId= i.ProductSalesInfoId and sr.Status='NOTADJUST'),0)


 from tblProductSalesInfo i
 inner join tblProductSalesDetails d on i.ProductSalesInfoId=d.ProductSalesInfoId
 left join tblSalesReturn r on i.ProductSalesInfoId=r.ProductSalesInfoId


Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }
    }
}
