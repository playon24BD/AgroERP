using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPBO.Common;
using ERPDAL.AgricultureContextMigrations;
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
        private readonly AgroProductSalesDetailsRepository _agroProductSalesDetailsRepository;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;

        //private readonly AppUserRepository appUserRepository; // repo
        private readonly IAgroProductSalesDetailsBusiness _agroProductSalesDetailsBusiness;
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
        private readonly IFinishGoodProductionInfoBusiness _finishGoodProductionInfoBusiness;
        private readonly ISalesReturn _salesReturn;

        public AgroProductSalesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork,ISalesReturn salesReturn, IFinishGoodProductionInfoBusiness finishGoodProductionInfoBusiness,IAppUserBusiness appUserBusiness, IStockiestInfo stockiestInfo, ITerritorySetup territorySetup, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IUserAssignBussiness userAssignBussiness, IUserInfo userInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IAgroUnitInfo agroUnitInfo, IMeasuremenBusiness measuremenBusiness, ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness, IStockiestUserBusiness stockiestUserBusiness, IAgroProductSalesDetailsBusiness agroProductSalesDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);
            this._agroProductSalesDetailsRepository = new AgroProductSalesDetailsRepository(this._agricultureUnitOfWork);

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
            this._agroProductSalesDetailsBusiness = agroProductSalesDetailsBusiness;
            this._finishGoodProductionInfoBusiness= finishGoodProductionInfoBusiness;
            this._salesReturn = salesReturn;
        }



        public AgroProductSalesInfo CheckBYProductSalesInfoId(long? ProductSalesInfoId)
        {
            try
            {

                return _agroProductSalesInfoRepository.GetOneByOrg(i => i.ProductSalesInfoId == ProductSalesInfoId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId)
        {
            try
            {

                return _agroProductSalesInfoRepository.GetOneByOrg(f => f.ProductSalesInfoId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId)
        {
            return _agroProductSalesInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }
        public IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(stockiestId, invoiceNo, fromDate, toDate)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetLastInvoice(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfossLastInvoice(orgId)).ToList();
        }

        private string QueryForAgroSalesInfossLastInvoice(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"	SELECT Top 1 * FROM tblProductSalesInfo where OrganizationId=9 order by ProductSalesInfoId DESC", Utility.ParamChecker(param));

            return query;
        }

        private string QueryForAgroSalesInfoss(long? stockiestId, string invoiceNo, string fromDate, string toDate)
        {
            try
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


                query = string.Format(@"	select sales.StockiestId,sales.ChallanNo,sales.DriverName,sales.DeliveryPlace,sales.VehicleType,sales.VehicleNumber,sales.TotalAmount,sales.DueAmount,sales.PaidAmount,sales.InvoiceNo,sales.ProductSalesInfoId,CONVERT(date,sales.InvoiceDate)as InvoiceDate,stock.StockiestName
                from tblProductSalesInfo sales
                inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId 
                
                Where 1=1 {0} and sales.Status is null  order by sales.ProductSalesInfoId desc

", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<AgroProductSalesInfo> GetUserName(long orgId)
        {
            throw new NotImplementedException();
        }
        // Working here

        public bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details,long userId, long orgId)
        {

            bool isSuccess = false;

            double issueQunatity = 0;
            double requirdQuantity = 0;
            bool Checked = false;


            var ChallanNo = "CHA-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            var InvoiceNo = "INV-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");


            var pro = details.GroupBy(r => new { r.FinishGoodProductInfoId, r.MeasurementId,r.FGRId }).Select( g => new AgroProductSalesDetailsDTO
            {
                FinishGoodProductInfoId = g.Key.FinishGoodProductInfoId,
                MeasurementId = g.Key.MeasurementId,
                FGRId = g.Key.FGRId,
                Quanity = g.Sum(r => r.Quanity),
               
            
            });



            foreach (var product in pro)
            {

                var Productstockin = _finishGoodProductionInfoBusiness.GetProductStockINbyPMRid(product.MeasurementId, product.FinishGoodProductInfoId, product.FGRId).ToList();
                var SumProductStockin = Productstockin.Sum(c => c.TargetQuantity);

                var ProductSales = _agroProductSalesDetailsBusiness.GetProductSalesbyPMRid(product.MeasurementId, product.FinishGoodProductInfoId, product.FGRId).ToList();
                var SumProductSales = ProductSales.Sum(s => s.Quanity);

                var ProductSalesDrop = _agroProductSalesDetailsBusiness.GetProductSalesbyPMRidDRP(product.MeasurementId, product.FinishGoodProductInfoId, product.FGRId).ToList();
                var SumProductSalesDrop = ProductSalesDrop.Sum(d => d.Quanity);

                var ProductSalesReturn = _salesReturn.GetProductReturnbyPMRid(product.MeasurementId, product.FinishGoodProductInfoId, product.FGRId).ToList();
                var SumProductSalesReturn = ProductSalesReturn.Sum(r => r.ReturnQuanity);


                issueQunatity = SumProductStockin - SumProductSales + SumProductSalesDrop + SumProductSalesReturn;

                requirdQuantity = product.Quanity;


                if (requirdQuantity > issueQunatity)
                {

                    Checked = false;
                    break;
                }
                else
                {
                    Checked = true;
                   

                }

            }

            if (Checked)
            {
                if (agroSalesInfoDTO.ProductSalesInfoId == 0)
                {
                    var UserId = _stockiestUserBusiness.GetStockiestInfoById(agroSalesInfoDTO.UserId, orgId).UserId;
                    var stockeiestId = _appUserBusiness.GetId(UserId, orgId).StockiestId;
                    long stockId = agroSalesInfoDTO.UserId;

                    var territoryId = _stockiestInfo.GetStockiestInfoById(stockId, orgId).TerritoryId;

                    var areaId = _territorySetup.GetTerritoryNamebyId(territoryId, orgId).AreaId;

                    var regionId = _areaSetupBusiness.GetAreaInfoById(areaId, orgId).RegionId;

                    var divisionId = _regionSetup.GetRegionNamebyId(regionId, orgId).DivisionId;

                    var zoneId = _divisionInfo.GetDivisionInfoById(divisionId, orgId).ZoneId;

                    ExecutionStateWithText executionState = new ExecutionStateWithText();
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
                            ChallanDate = agroSalesInfoDTO.ChallanDate,
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
                            double ProductMesurement = 0;
                            double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
                            double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
                            double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;

                            var UnitQtys = item.QtyKG.Split('(', ')');
                            int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
                            string ProductUnit = UnitQtys[1];

                            if (MasterCartonMasurement != 0)
                            {
                                ProductMesurement = MasterCartonMasurement * InnerBoxMasurement;
                            }
                            else
                            {
                                ProductMesurement = InnerBoxMasurement;
                            }




                            var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
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
                                BoxQuanity = ProductMesurement




                            };
                            agroDetails.Add(agroSalesDetails);
                        }
                        agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
                        _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);



                        isSuccess = _agroProductSalesInfoRepository.Save();

                        if (isSuccess)
                        {
                            //Commission on Sales 
                            executionState.text = InvoiceNo;
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
                            UserId = UserId,
                            StockiestId = stockId,
                            TerritoryId = territoryId,
                            AreaId = areaId,
                            DivisionId = divisionId,
                            RegionId = regionId,
                            ZoneId = zoneId,
                            ChallanDate = agroSalesInfoDTO.ChallanDate,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            OrganizationId = orgId,
                            Depot = agroSalesInfoDTO.Depot,
                            Do_ADO_DA = agroSalesInfoDTO.Do_ADO_DA,
                            TotalAmount = agroSalesInfoDTO.TotalAmount,
                            PaidAmount = 0,
                            DueAmount = agroSalesInfoDTO.TotalAmount,

                        };
                        List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();
                        if (details.Count > 0)
                        {
                            foreach (var item in details)
                            {
                                double ProductMesurement = 0;
                                double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
                                double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
                                double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;

                                var UnitQtys = item.QtyKG.Split('(', ')');
                                int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
                                string ProductUnit = UnitQtys[1];
                                if (MasterCartonMasurement != 0)
                                {
                                    ProductMesurement = MasterCartonMasurement * InnerBoxMasurement;
                                }
                                else
                                {
                                    ProductMesurement = InnerBoxMasurement;
                                }
                                //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;



                                var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;

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
                                    BoxQuanity = ProductMesurement,
                                    PackageId = item.PackageId,

                                };
                                agroDetails.Add(agroSalesDetails);
                            }

                            agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
                            _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);
                            isSuccess = _agroProductSalesInfoRepository.Save();

                        }

                        if (isSuccess)
                        {
                            //Commission on Sales 
                            executionState.text = InvoiceNo;
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
            on ST.StockiestId=sales.StockiestId
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
            on ST.StockiestId=sales.StockiestId
            LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
            on TE.TerritoryId=ST.TerritoryId

            Where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<InvoiceWiseCollectionSalesReport> GetInvoiceWiseSalesReport(long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                return _agricultureUnitOfWork.Db.Database.SqlQuery<InvoiceWiseCollectionSalesReport>(QueryForInvoiceWiseSalesReport(stockiestId, territoryId, invoiceNo, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string QueryForInvoiceWiseSalesReport(long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                string param = string.Empty;
                string query = string.Empty;

                if (stockiestId != null && stockiestId > 0)
                {
                    param += string.Format(@" and STI.StockiestId={0}", stockiestId);
                }
                if (territoryId != null && territoryId > 0)
                {
                    param += string.Format(@" and TI.TerritoryId={0}", territoryId);
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
                    SELECT  todate='" + fromDate + "', fromDate='" + toDate + "',SI.ProductSalesInfoId, A.AreaName,ZoneUserName =( SELECT distinct Concat(AU.FullName,'( ',AU.Desigation,' )') AS FullName FROM   [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), ZoneUserMobile=( SELECT distinct AU.MobileNo FROM  [Agriculture].[dbo].tblProductSalesInfo SI  INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), TI.TerritoryName, Concat(AUT.FullName ,'( ',AUT.Desigation,' )') AS TerritoryUserName,AUT.MobileNo, STI.StockiestName, SI.InvoiceNo, CONVERT(date,SI.InvoiceDate) AS InvoiceDate, SI.TotalAmount AS InvoiceTk, Collaction=ISNULL((SELECT Sum(PSPH.PaymentAmount) FROM tblProductSalesPaymentHistory PSPH Where  SI.ProductSalesInfoId=PSPH.ProductSalesInfoId),0),  SI.DueAmount, DiscountTk=ISNULL((SELECT Sum(PSD.DiscountTk) FROM tblProductSalesDetails PSD Where        SI.ProductSalesInfoId=PSD.ProductSalesInfoId),0), CONVERT(date,PsPH.PaymentDate) AS PaymentDate,  PsPH.Remarks,  PsPH.PaymentAmount FROM [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblAreaSetup A on SI.AreaId=A.AreaId INNER JOIN [Agriculture].[dbo].tblTerritoryInfos TI on SI.TerritoryId=TI.TerritoryId INNER JOIN [Agriculture].[dbo].tblStockiestInfo STI on SI.StockiestId=STI.StockiestId  INNER JOIN [Agriculture].[dbo].tblProductSalesPaymentHistory PsPH on SI.ProductSalesInfoId=PsPH.ProductSalesInfoId INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId INNER JOIN[ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId = AUT.UserId  where 1=1 {0}", Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<ProductWiseSalesStementReport> GetProductwisesalesReportDownloadRpt(long? productId, string fromDate, string toDate)
        {
            try
            {
                return _agricultureUnitOfWork.Db.Database.SqlQuery<ProductWiseSalesStementReport>(QueryProductWiseSalesStementReport(productId, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryProductWiseSalesStementReport(long? productId, string fromDate, string toDate)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;
                string FromDate = string.Empty;
                string ToDate = string.Empty;

                if (productId != 0 && productId > 0)
                {
                    param += string.Format(@" and FGPN.FinishGoodProductId={0}", productId);
                }

                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
                }



                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", tDate);
                }

                query = string.Format(@"SELECT DISTINCT todate='" + fromDate + "', fromDate='" + toDate + "',FGPN.FinishGoodProductName,salesD.MeasurementSize AS PackSize, QtyCTN=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId),QtyKG=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId) * M.UnitKG,Total=(SELECT SUM(sd.Price) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId)FROM [Agriculture].[dbo].[tblProductSalesDetails] salesD INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId INNER JOIN [Agriculture].[dbo].[tblMeasurement] M on salesD.MeasurementId=M.MeasurementId  Inner Join [Agriculture].[dbo].[tblProductSalesInfo] sales  on sales.ProductSalesInfoId=salesD.ProductSalesInfoId Where 1=1{0} Group by FGPN.FinishGoodProductName, M.MeasurementName,salesD.MeasurementId,salesD.FinishGoodProductInfoId,  salesD.Quanity,M.UnitKG,salesD.Price,sales.TotalAmount,salesD.ProductSalesInfoId,M.MasterCarton,M.InnerBox,M.PackSize,salesD.MeasurementSize", Utility.ParamChecker(param));

                //query = string.Format(@"SELECT DISTINCT todate='" + fromDate + "', fromDate='" + toDate + "',FGPN.FinishGoodProductName,salesD.MeasurementSize AS PackSize, QtyCTN=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId),QtyKG=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId) * M.UnitKG,Total=(SELECT SUM(sd.Price) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId)FROM [Agriculture].[dbo].[tblProductSalesDetails] salesD INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId INNER JOIN [Agriculture].[dbo].[tblMeasurement] M on salesD.MeasurementId=M.MeasurementId  Inner Join [Agriculture].[dbo].[tblProductSalesInfo] sales  on sales.ProductSalesInfoId=salesD.ProductSalesInfoId Where 1=1{0} Group by FGPN.FinishGoodProductName, M.MeasurementName,salesD.MeasurementId,salesD.FinishGoodProductInfoId,  salesD.Quanity,M.UnitKG,salesD.Price,salesD.EntryDate,sales.TotalAmount,salesD.ProductSalesInfoId,M.MasterCarton,M.InnerBox,M.PackSize,salesD.MeasurementSize", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroSalesinfoByStokiestId(long StockiestId)
        {
            return _agroProductSalesInfoRepository.GetAll(d => d.StockiestId == StockiestId && d.Status == null);

        }

        public IEnumerable<AgroProductSalesInfoDTO> GetPaymentListInfos(string name, string fromDate, string toDate)
        {
            try
            {

                return _agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryPaymentListReport(name, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryPaymentListReport(string name, string fromDate, string toDate)
        {
            try
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
            on ST.StockiestId=sales.StockiestId
            LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
            on TE.TerritoryId=ST.TerritoryId


            Where 1=1 and sales.PaymentMode='Credit' {0} order by sales.ProductSalesInfoId DESC", Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<SalesReturnDTO> GetSalesAdjustInfos(string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<SalesReturnDTO>(QueryForSalesReturnAdjust(invoiceNo, fromDate, toDate)).ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForSalesReturnAdjust(string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    param += string.Format(@"and sr.InvoiceNo like '%{0}%'", invoiceNo);
                }

                //if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                //{
                //    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                //    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                //    param += string.Format(@" and Cast(i.InvoiceDate as date) between '{0}' and '{1}'", fDate, tDate);
                //}
                //else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                //{
                //    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                //    param += string.Format(@" and Cast(i.InvoiceDate as date)='{0}'", fDate);
                //}
                //else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                //{
                //    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                //    param += string.Format(@" and Cast(i.InvoiceDate as date)='{0}'", tDate);
                //}


                query = string.Format(@"select distinct sr.ProductSalesInfoId,sr.InvoiceNo,st.StockiestName
FROM tblSalesReturn sr INNER JOIN tblProductSalesInfo si on sr.ProductSalesInfoId = si.ProductSalesInfoId
inner join tblFinishGoodProductInfo fpi on sr.FinishGoodProductInfoId = fpi.FinishGoodProductId 
inner join tblStockiestInfo st on sr.StockiestId = st.StockiestId


Where 1=1 {0}", Utility.ParamChecker(param));

                return query;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetInvoiceReportList(long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForInvoiceReportList(stockiestId, territoryId, invoiceNo, fromDate, toDate)).ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }
        private string QueryForInvoiceReportList(long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;

                //param += string.Format(@" and sales.OrganizationId={0}", orgId);
                if (stockiestId != null && stockiestId > 0)
                {
                    param += string.Format(@" and STI.StockiestId={0}", stockiestId);
                }
                if (territoryId != null && territoryId > 0)
                {
                    param += string.Format(@" and TI.TerritoryId={0}", territoryId);
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


                query = string.Format(@"SELECT  todate='" + fromDate + "', fromDate='" + toDate + "',SI.ProductSalesInfoId, ZoneUserName =( SELECT distinct Concat(AU.FullName,'( ',AU.Desigation,' )') AS FullName FROM   [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), ZoneUserMobile=( SELECT distinct AU.MobileNo FROM  [Agriculture].[dbo].tblProductSalesInfo SI  INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId), TI.TerritoryName, Concat(AUT.FullName ,'( ',AUT.Desigation,' )') AS TerritoryUserName,AUT.MobileNo, STI.StockiestName, SI.InvoiceNo, CONVERT(date,SI.InvoiceDate) AS InvoiceDate, SI.TotalAmount AS InvoiceTk, Collaction=ISNULL((SELECT Sum(PSPH.PaymentAmount) FROM tblProductSalesPaymentHistory PSPH Where  SI.ProductSalesInfoId=PSPH.ProductSalesInfoId),0),  SI.DueAmount, DiscountTk=ISNULL((SELECT Sum(PSD.DiscountTk) FROM tblProductSalesDetails PSD Where        SI.ProductSalesInfoId=PSD.ProductSalesInfoId),0), CONVERT(date,PsPH.PaymentDate) AS PaymentDate,  PsPH.Remarks,  PsPH.PaymentAmount FROM [Agriculture].[dbo].tblProductSalesInfo SI INNER JOIN [Agriculture].[dbo].tblAreaSetup A on SI.AreaId=A.AreaId INNER JOIN [Agriculture].[dbo].tblTerritoryInfos TI on SI.TerritoryId=TI.TerritoryId INNER JOIN [Agriculture].[dbo].tblStockiestInfo STI on SI.StockiestId=STI.StockiestId  INNER JOIN [Agriculture].[dbo].tblProductSalesPaymentHistory PsPH on SI.ProductSalesInfoId=PsPH.ProductSalesInfoId INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId INNER JOIN[ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId = AUT.UserId  where 1=1 {0}", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IEnumerable<AgroProductSalesInfoDTO> GetProductWiseReportList(long? productId, string fromDate, string toDate)
        {
            try
            {

                return _agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryProductWiseSalesReportList(productId, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryProductWiseSalesReportList(long? productId, string fromDate, string toDate)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;


                if (productId != 0 && productId > 0)
                {
                    param += string.Format(@" and FGPN.FinishGoodProductId={0}", productId);
                }

                 if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
                }

                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", tDate);
                }

                query = string.Format(@"SELECT DISTINCT  todate='" + fromDate + "', fromDate='" + toDate + "', FGPN.FinishGoodProductName,salesD.MeasurementSize AS PackSize, QtyCTN=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId),QtyKG=(SELECT SUM(sd.Quanity) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId) * M.UnitKG,Total=(SELECT SUM(sd.Price) FROM [Agriculture].[dbo].[tblProductSalesDetails] sd where sd.MeasurementId=salesD.MeasurementId and sd.FinishGoodProductInfoId=salesD.FinishGoodProductInfoId)FROM [Agriculture].[dbo].[tblProductSalesDetails] salesD INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId INNER JOIN [Agriculture].[dbo].[tblMeasurement] M on salesD.MeasurementId=M.MeasurementId  Inner Join [Agriculture].[dbo].[tblProductSalesInfo] sales  on sales.ProductSalesInfoId=salesD.ProductSalesInfoId Where 1=1{0} Group by FGPN.FinishGoodProductName, M.MeasurementName,salesD.MeasurementId,salesD.FinishGoodProductInfoId,  salesD.Quanity,M.UnitKG,salesD.Price,salesD.EntryDate,sales.TotalAmount,salesD.ProductSalesInfoId,M.MasterCarton,M.InnerBox,M.PackSize,salesD.MeasurementSize", Utility.ParamChecker(param));
                return query;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetAllINVBYSTOKIESTID(long StockiestId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForGetAllINVBYSTOKIESTID(StockiestId)).ToList();
        }
        private string QueryForGetAllINVBYSTOKIESTID(long StockiestId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (StockiestId > 0)
            {
                param += string.Format(@" and s.StockiestId={0}", StockiestId);
            }

            query = string.Format(@"select s.ProductSalesInfoId,s.InvoiceNo,s.StockiestId from tblProductSalesInfo s
			inner join tblStockiestInfo st on s.StockiestId=st.StockiestId	
            Where 1=1 {0} and s.Status is null", Utility.ParamChecker(param));

            return query;
        }

        public bool UpdateInvoiceDrop(long productSalesInfoId, long userId)
        {
            bool isUpdateSucccess = false;

            var SalesInfoDb = GetInvoiceProductionInfoById(productSalesInfoId);

            if (SalesInfoDb != null)
            {
                SalesInfoDb.Status = "Drop";
                SalesInfoDb.UpdateDate = DateTime.Now;
                SalesInfoDb.UpdateUserId = userId;
                _agroProductSalesInfoRepository.Update(SalesInfoDb);
                isUpdateSucccess = _agroProductSalesInfoRepository.Save();
            }
            var SalesDetailsDb = _agroProductSalesDetailsBusiness.GetAgroSalesDetailsByInfoId(productSalesInfoId, 9);
            if (SalesDetailsDb != null)
            {
                foreach (var item in SalesDetailsDb)
                {
                    item.Status = "Drop";
                    item.UpdateDate = DateTime.Now;
                    item.UpdateUserId = userId;
                    _agroProductSalesDetailsRepository.Update(item);
                    isUpdateSucccess = _agroProductSalesDetailsRepository.Save();

                }
            }


            return isUpdateSucccess;
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetSalesDropList(string invoiceNo)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForSalesDropList(invoiceNo)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string QueryForSalesDropList(string invoiceNo)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;

                if (!string.IsNullOrEmpty(invoiceNo))
                {
                    param += string.Format(@"and info.InvoiceNo like '%{0}%'", invoiceNo);
                }


                query = string.Format(@"
select  info.InvoiceNo,info.InvoiceDate,info.ProductSalesInfoId,st.StockiestName,info.TotalAmount,info.PaidAmount,info.DueAmount from tblProductSalesInfo info
inner join tblStockiestInfo st on info.StockiestId=st.StockiestId
 where 1=1 {0} and info.Status='Drop' order by info.ProductSalesInfoId desc
			
 ", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateProductSalesEdit(AgroProductSalesInfoDTO infoDTO, List<AgroProductSalesDetailsDTO> detailsDTO, long userId, long orgId)
        {

            bool IsSuccess = false;
            double Totalinvoicepayble = 0;
            List<AgroProductSalesDetails> salesDetailss = new List<AgroProductSalesDetails>();

            AgroProductSalesDetails productSalesDetailss = new AgroProductSalesDetails();

            foreach (var tom in detailsDTO)
            {
                if(tom.ISActive != true)
                {
                    var measurmentid = _agroProductSalesDetailsBusiness.GetSalesDetailsById(tom.ProductSalesDetailsId, orgId).MeasurementId;
                    double TotalproductQtyinfo = 0;

                    double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).MasterCarton;
                    double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).InnerBox;
                    double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).PackSize;


                    if (MasterCartonMasurement != 0)
                    {
                        TotalproductQtyinfo = tom.Quanity;
                    }
                    else
                    {
                        TotalproductQtyinfo = tom.Quanity;
                    }

                    productSalesDetailss = _agroProductSalesDetailsBusiness.GetSalesDetailsById(tom.ProductSalesDetailsId, orgId);

                    double totalbill = TotalproductQtyinfo * productSalesDetailss.Price;


                    double dicounttotal = 0;
                    if(tom.Discount !=0)
                    {
                        dicounttotal = totalbill * tom.Discount / 100;
                    }
                    else
                    {
                        dicounttotal = 0;
                    }

                    double payableamount = totalbill - dicounttotal;
                    Totalinvoicepayble += payableamount;
                }

            }


            AgroProductSalesInfo salesInfo = new AgroProductSalesInfo();

            salesInfo = GetSalesById(infoDTO.ProductSalesInfoId, orgId);
            salesInfo.StockiestId = infoDTO.StockiestId;
            salesInfo.DriverName = infoDTO.DriverName;
            salesInfo.DeliveryPlace = infoDTO.DeliveryPlace;
            salesInfo.VehicleType = infoDTO.VehicleType;
            salesInfo.TotalAmount = Totalinvoicepayble;
            salesInfo.PaidAmount = 0;
            salesInfo.DueAmount = Totalinvoicepayble;

            _agroProductSalesInfoRepository.Update(salesInfo);
            IsSuccess = _agroProductSalesInfoRepository.Save();




            //details
            List<AgroProductSalesDetails> salesDetails = new List<AgroProductSalesDetails>();

            AgroProductSalesDetails productSalesDetails = new AgroProductSalesDetails();

            foreach (var item in detailsDTO)
            {

                if (item.ISActive == true)
                {
                    productSalesDetails = _agroProductSalesDetailsBusiness.GetSalesDetailsById(item.ProductSalesDetailsId, orgId);
                    productSalesDetails.Status = "Drop";

                }
                else
                {


                    var measurmentid = _agroProductSalesDetailsBusiness.GetSalesDetailsById(item.ProductSalesDetailsId, orgId).MeasurementId;
                    double TotalproductQty = 0;

                    double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).MasterCarton;
                    double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).InnerBox;
                    double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(measurmentid, orgId).PackSize;


                    if (MasterCartonMasurement != 0)
                    {
                        TotalproductQty = item.Quanity;

                    }
                    else
                    {
                        TotalproductQty = item.Quanity;
                    }



                    productSalesDetails = _agroProductSalesDetailsBusiness.GetSalesDetailsById(item.ProductSalesDetailsId, orgId);
                    productSalesDetails.BoxQuanity = item.BoxQuanity;
                    productSalesDetails.Quanity = TotalproductQty;
                    double Distk = 0;
                    if (item.Discount != 0)
                    {

                        Distk = (TotalproductQty * productSalesDetails.Price) * item.Discount / 100;

                    }
                    else
                    {
                        Distk = 0;
                    }

                    
                    

                    productSalesDetails.Discount = item.Discount;
                    productSalesDetails.DiscountTk = Distk;

                }
                _agroProductSalesDetailsRepository.Update(productSalesDetails);

            }



            _agroProductSalesDetailsRepository.UpdateAll(salesDetails);
            IsSuccess = _agroProductSalesDetailsRepository.Save();

            return IsSuccess;










        }








        public AgroProductSalesInfo GetSalesById(long ProductSalesInfoId, long orgId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(a => a.ProductSalesInfoId == ProductSalesInfoId && a.OrganizationId == orgId);
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetDealerLadserInfos(long id, string fromDate, string toDate)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForDealerLadserInfos(id, fromDate, toDate)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string QueryForDealerLadserInfos(long id,string fromDate, string toDate)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;

                //param += string.Format(@" and sales.OrganizationId={0}", orgId);
                //if (stockiestId != null && stockiestId > 0)
                //{
                //    param += string.Format(@" and sales.StockiestId={0}", stockiestId);
                //}
                if (id != 0 && id > 0)
                {
                    param += string.Format(@" and sales.StockiestId={0}", id);
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
select distinct *,(t.copsdSum+t.phsum)CommisionAmount from (select sales.StockiestId,sales.ChallanNo,sales.DriverName,sales.DeliveryPlace,
sales.VehicleType,sales.VehicleNumber,sales.DueAmount,
sales.PaidAmount,sales.InvoiceNo,sales.ProductSalesInfoId,CONVERT(date,sales.InvoiceDate)as InvoiceDate,stock.StockiestName,
Amount = ISNULL((select sum(sr.ReturnTotalPrice) from tblSalesReturn sr where sr.ProductSalesInfoId = sales.ProductSalesInfoId and sr.Status='ADJUST' ),0),

TotalAmount=(sales.TotalAmount)-ISNULL((select sum(sr.ReturnTotalPrice) from tblSalesReturn sr where sr.ProductSalesInfoId = sales.ProductSalesInfoId and sr.Status='ADJUST' ),0),


	  (select sum(TotalCommission) from tblCommisionOnProductSalesDetails copsd where copsd.CommissionOnProductOnSalesId=tconps.CommissionOnProductOnSalesId )copsdSum,
	  ( select sum(isnull(CommisionAmount,0)) from tblProductSalesPaymentHistory psph where psph.ProductSalesInfoId=tconps.ProductSalesInfoId)phsum
	 from tblCommissionOnProductSales tconps
	  Inner join tblCommisionOnProductSalesDetails cpsd
            on tconps.CommissionOnProductOnSalesId=cpsd.CommissionOnProductOnSalesId
	   Inner join tblProductSalesInfo sales
            on sales.ProductSalesInfoId=tconps.ProductSalesInfoId
			Inner join tblStockiestInfo stock
            on sales.StockiestId=stock.StockiestId
where 1=1 {0}

	 ) t  

", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<AgroProductSalesInfoDTO> GetPaymentLadserInfos(long StockiestId)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForGetPaymentLadserInfos(StockiestId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForGetPaymentLadserInfos(long StockiestId)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;


                if (StockiestId != 0 && StockiestId > 0)
                {
                    param += string.Format(@" and sales.StockiestId={0}", StockiestId);
                }

                query = string.Format(@"	
select sales.StockiestId,sales.ChallanNo,sales.DriverName,sales.DeliveryPlace,sales.VehicleType,sales.VehicleNumber,sales.TotalAmount,sales.DueAmount,sales.PaidAmount,sales.InvoiceNo,sales.ProductSalesInfoId,CONVERT(date,sales.InvoiceDate)as InvoiceDate,stock.StockiestName,


Amount = ISNULL((select sum(sr.ReturnTotalPrice) from tblSalesReturn sr where sr.ProductSalesInfoId = sales.ProductSalesInfoId and sr.Status='ADJUST' ),0),
(sales.TotalAmount)-ISNULL((select sum(sr.ReturnTotalPrice) from tblSalesReturn sr where sr.ProductSalesInfoId = sales.ProductSalesInfoId and sr.Status='ADJUST' ),0)as TotalAmount


from tblProductSalesInfo sales
inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId  Where 1=1 and sales.DueAmount !< 1 {0}              
and sales.Status is null  order by sales.ProductSalesInfoId desc

", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }



        //ExecutionStateWithText IAgroProductSalesInfoBusiness.  (AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId)
        //{
        //    bool isSuccess = false;

        //    var ChallanNo = "CHA-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

        //    var InvoiceNo = "INV-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

        //    ExecutionStateWithText executionState = new ExecutionStateWithText();

        //    if (agroSalesInfoDTO.ProductSalesInfoId == 0)
        //    {
        //        var UserId = _stockiestUserBusiness.GetStockiestInfoById(agroSalesInfoDTO.UserId, orgId).UserId;
        //        var stockeiestId = _appUserBusiness.GetId(UserId, orgId).StockiestId;
        //        long stockId = agroSalesInfoDTO.UserId;

        //        var territoryId = _stockiestInfo.GetStockiestInfoById(stockId, orgId).TerritoryId;

        //        var areaId = _territorySetup.GetTerritoryNamebyId(territoryId, orgId).AreaId;

        //        var regionId = _areaSetupBusiness.GetAreaInfoById(areaId, orgId).RegionId;

        //        var divisionId = _regionSetup.GetRegionNamebyId(regionId, orgId).DivisionId;

        //        var zoneId = _divisionInfo.GetDivisionInfoById(divisionId, orgId).ZoneId;


        //        //paymenttable
        //        if (agroSalesInfoDTO.PaymentMode == "Cash")
        //        {

        //            AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
        //            {
        //                //Info
        //                ChallanNo = ChallanNo,
        //                InvoiceNo = InvoiceNo,
        //                VehicleNumber = agroSalesInfoDTO.VehicleNumber,
        //                ProductSalesInfoId = agroSalesInfoDTO.ProductSalesInfoId,
        //                DeliveryPlace = agroSalesInfoDTO.DeliveryPlace,
        //                DoADO_Name = agroSalesInfoDTO.DoADO_Name,
        //                DriverName = agroSalesInfoDTO.DriverName,
        //                InvoiceDate = agroSalesInfoDTO.InvoiceDate,
        //                PaymentMode = agroSalesInfoDTO.PaymentMode,
        //                VehicleType = agroSalesInfoDTO.VehicleType,
        //                UserAssignId = agroSalesInfoDTO.UserAssignId,
        //                UserId = UserId,
        //                StockiestId = stockId,
        //                TerritoryId = territoryId,
        //                AreaId = areaId,
        //                DivisionId = divisionId,
        //                RegionId = regionId,
        //                ZoneId = zoneId,
        //                ChallanDate = DateTime.Now,
        //                EntryDate = DateTime.Now,
        //                EntryUserId = userId,
        //                OrganizationId = orgId,
        //                Depot = agroSalesInfoDTO.Depot,
        //                Do_ADO_DA = agroSalesInfoDTO.Do_ADO_DA,
        //                TotalAmount = agroSalesInfoDTO.TotalAmount,
        //                PaidAmount = agroSalesInfoDTO.TotalAmount,
        //                DueAmount = 0

        //            };


        //            List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();

        //            foreach (var item in details)
        //            {
        //                //double ProductMesurement = 0;
        //                //double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
        //                //double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
        //                //double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;
        //                var UnitQtys = item.QtyKG.Split('(', ')');
        //                int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
        //                string ProductUnit = UnitQtys[1];
        //                //if (MasterCartonMasurement != 0)
        //                //{
        //                //     ProductMesurement = MasterCartonMasurement * InnerBoxMasurement ;
        //                //}
        //                //else
        //                //{
        //                //     ProductMesurement = InnerBoxMasurement;
        //                //}
        //                //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;



        //                var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
        //                //var receipeBatch=_finishGoodRecipeInfoBusiness
        //                //var receipeBatch = item.ReceipeBatchCode.Split('(',')');
        //                var FGRId = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).FGRId;
        //                var receipeBatch = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).ReceipeBatchCode;

        //                AgroProductSalesDetails agroSalesDetails = new AgroProductSalesDetails()
        //                {
        //                    //Details
        //                    Discount = item.Discount,
        //                    DiscountTk = item.DiscountTk,
        //                    EntryDate = DateTime.Now,
        //                    EntryUserId = userId,
        //                    MeasurementId = item.MeasurementId,
        //                    MeasurementSize = item.MeasurementSize,
        //                    OrganizationId = orgId,
        //                    Price = item.Price,
        //                    ProductSalesInfoId = item.ProductSalesInfoId,
        //                    Quanity = item.Quanity,
        //                    FinishGoodProductInfoId = item.FinishGoodProductInfoId,
        //                    ProductSalesDetailsId = item.ProductSalesDetailsId,
        //                    ReceipeBatchCode = receipeBatch,
        //                    FGRId = FGRId,
        //                    QtyKG = item.QtyKG,
        //                    BoxQuanity = item.BoxQuanity




        //                };
        //                agroDetails.Add(agroSalesDetails);
        //            }
        //            agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
        //            _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);



        //            //isSuccess = _agroProductSalesInfoRepository.Save();

        //            //if (isSuccess)
        //            //{
        //            //    //Commission on Sales 

        //            //    isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);

        //            //}
        //            if (_agroProductSalesInfoRepository.Save() == true)
        //            {
        //                isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);
        //                executionState.isSuccess = isSuccess;
        //                executionState.text = InvoiceNo;
        //            }


        //            SalesPaymentRegister salesPayment = new SalesPaymentRegister
        //            {
        //                PaymentDate = DateTime.Now,
        //                PaymentAmount = agroSalesInfoDTO.TotalAmount,
        //                ProductSalesInfoId = agroSalesProductionInfo.ProductSalesInfoId,
        //                Remarks = "SalesTime",
        //                EntryUserId = userId
        //            };
        //            //paymenttable
        //            _salesPaymentRegisterRepository.Insert(salesPayment);
        //            isSuccess = _salesPaymentRegisterRepository.Save();
        //        }

        //        else
        //        {
        //            AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
        //            {
        //                //Info
        //                ChallanNo = ChallanNo,
        //                InvoiceNo = InvoiceNo,
        //                VehicleNumber = agroSalesInfoDTO.VehicleNumber,
        //                ProductSalesInfoId = agroSalesInfoDTO.ProductSalesInfoId,
        //                DeliveryPlace = agroSalesInfoDTO.DeliveryPlace,
        //                DoADO_Name = agroSalesInfoDTO.DoADO_Name,
        //                DriverName = agroSalesInfoDTO.DriverName,
        //                InvoiceDate = agroSalesInfoDTO.InvoiceDate,
        //                PaymentMode = agroSalesInfoDTO.PaymentMode,
        //                VehicleType = agroSalesInfoDTO.VehicleType,
        //                UserAssignId = agroSalesInfoDTO.UserAssignId,
        //                UserId = UserId,
        //                StockiestId = stockId,
        //                TerritoryId = territoryId,
        //                AreaId = areaId,
        //                DivisionId = divisionId,
        //                RegionId = regionId,
        //                ZoneId = zoneId,
        //                ChallanDate = DateTime.Now,
        //                EntryDate = DateTime.Now,
        //                EntryUserId = userId,
        //                OrganizationId = orgId,
        //                Depot = agroSalesInfoDTO.Depot,
        //                Do_ADO_DA = agroSalesInfoDTO.Do_ADO_DA,
        //                TotalAmount = agroSalesInfoDTO.TotalAmount,
        //                PaidAmount = 0,
        //                DueAmount = agroSalesInfoDTO.TotalAmount

        //            };
        //            List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();

        //            foreach (var item in details)
        //            {
        //                //double ProductMesurement = 0;
        //                //double MasterCartonMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).MasterCarton;
        //                //double InnerBoxMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).InnerBox;
        //                //double PackSizeMasurement = _measuremenBusiness.GetMeasurementById(item.MeasurementId, orgId).PackSize;
        //                var UnitQtys = item.QtyKG.Split('(', ')');
        //                int ProductUnitQty = Convert.ToInt32(UnitQtys[0]);
        //                string ProductUnit = UnitQtys[1];
        //                //if (MasterCartonMasurement != 0)
        //                //{
        //                //     ProductMesurement = MasterCartonMasurement * InnerBoxMasurement ;
        //                //}
        //                //else
        //                //{
        //                //     ProductMesurement = InnerBoxMasurement;
        //                //}
        //                //var TotalProductSaleQty = ProductMesurement * ProductUnitQty;



        //                var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
        //                //var receipeBatch=_finishGoodRecipeInfoBusiness
        //                //var receipeBatch = item.ReceipeBatchCode.Split('(',')');
        //                var FGRId = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).FGRId;
        //                var receipeBatch = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).ReceipeBatchCode;

        //                AgroProductSalesDetails agroSalesDetails = new AgroProductSalesDetails()
        //                {
        //                    //Details
        //                    Discount = item.Discount,
        //                    DiscountTk = item.DiscountTk,
        //                    EntryDate = DateTime.Now,
        //                    EntryUserId = userId,
        //                    MeasurementId = item.MeasurementId,
        //                    MeasurementSize = item.MeasurementSize,
        //                    OrganizationId = orgId,
        //                    Price = item.Price,
        //                    ProductSalesInfoId = item.ProductSalesInfoId,
        //                    Quanity = item.Quanity,
        //                    FinishGoodProductInfoId = item.FinishGoodProductInfoId,
        //                    ProductSalesDetailsId = item.ProductSalesDetailsId,
        //                    ReceipeBatchCode = receipeBatch,
        //                    FGRId = FGRId,
        //                    QtyKG = item.QtyKG,
        //                    BoxQuanity = item.BoxQuanity




        //                };
        //                agroDetails.Add(agroSalesDetails);
        //            }
        //            agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
        //            _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);



        //            //isSuccess = _agroProductSalesInfoRepository.Save();
        //            //if (isSuccess)
        //            //{
        //            //    //Commission on Sales 

        //            //    isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);

        //            //}
        //            if (_agroProductSalesInfoRepository.Save() == true)
        //            {
        //                isSuccess = _commissionOnProductOnSalesBusiness.SaveCommissionOnProductOnSales(agroSalesProductionInfo, userId, orgId);
        //                executionState.isSuccess = isSuccess;
        //                executionState.text = InvoiceNo;
        //            }


        //            SalesPaymentRegister salesPayment = new SalesPaymentRegister
        //            {
        //                PaymentDate = DateTime.Now,
        //                PaymentAmount = 0,
        //                ProductSalesInfoId = agroSalesProductionInfo.ProductSalesInfoId,
        //                Remarks = "SalesTime",
        //                EntryUserId = userId
        //            };
        //            //paymenttable
        //            _salesPaymentRegisterRepository.Insert(salesPayment);
        //            isSuccess = _salesPaymentRegisterRepository.Save();



        //        }


        //    }

        //    return executionState;
        //}
    }
}
