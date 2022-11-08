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

        public AgroProductSalesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAppUserBusiness appUserBusiness,IStockiestInfo stockiestInfo,ITerritorySetup territorySetup, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IUserAssignBussiness userAssignBussiness, IUserInfo userInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness, IAgroUnitInfo agroUnitInfo)
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
        }



        public AgroProductSalesInfo CheckBYProductSalesInfoId(long? ProductSalesInfoId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(i => i.ProductSalesInfoId == ProductSalesInfoId );
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
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(stockiestId,invoiceNo,fromDate,toDate)).ToList();
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

        public bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId)
        {

            bool isSuccess = false;

            var ChallanNo = "CHA-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            var InvoiceNo = "INV-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            string myList = "";

            if (agroSalesInfoDTO.ProductSalesInfoId == 0)
            {

                    var stockeiestId = _appUserBusiness.GetId(agroSalesInfoDTO.UserId, orgId).StockiestId;
                long stockId =Convert.ToInt32( stockeiestId);

                    var territoryId = _stockiestInfo.GetStockiestInfoById(stockId, orgId).TerritoryId;

                var areaId =_territorySetup.GetTerritoryNamebyId(stockId, orgId).AreaId;

                var regionId = _areaSetupBusiness.GetAreaInfoById(stockId, orgId).RegionId;

                var divisionId = _regionSetup.GetRegionNamebyId(stockId, orgId).DivisionId;

                var zoneId = _divisionInfo.GetDivisionInfoById(stockId, orgId).ZoneId;

                

                //var userAssignId = _userAssignBussiness.GetUserAssignById(stockId, orgId).UserAssignId;

                //var userInfo = _userInfo.GetUserInfoById(stockeiestId.Value, orgId).UserId;






                AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
                {

                    ChallanNo = ChallanNo,
                    InvoiceNo=InvoiceNo,
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
                    //UserAssignId=userAssignId,
                    //UserId = userInfo,
                    ZoneId=zoneId,
                    ChallanDate = DateTime.Now,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                     //InvoiceNo=agroSalesInfoDTO.InvoiceNo,
                     Depot=agroSalesInfoDTO.Depot,
                     Do_ADO_DA=agroSalesInfoDTO.Do_ADO_DA,
                     TotalAmount = agroSalesInfoDTO.TotalAmount,
                     PaidAmount = agroSalesInfoDTO.PaidAmount,
                     DueAmount = agroSalesInfoDTO.TotalAmount - agroSalesInfoDTO.PaidAmount

                };




                List<AgroProductSalesDetails> agroDetails = new List<AgroProductSalesDetails>();

                foreach (var item in details)
                {
                    var UnitQtys = item.QtyKG.Split('(', ')');
                    int ProductUnitQty =Convert.ToInt32( UnitQtys[0]);
                    string ProductUnit = UnitQtys[1];
                    var UnitId = _agroUnitInfo.GetUnitId(ProductUnit).UnitId;
                    //var receipeBatch=_finishGoodRecipeInfoBusiness
                    //var receipeBatch = item.ReceipeBatchCode.Split('(',')');
                    var FGRId = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).FGRId;
                    var receipeBatch = _finishGoodRecipeInfoBusiness.GetReceipId(item.FinishGoodProductInfoId, ProductUnitQty, UnitId).ReceipeBatchCode;

                    AgroProductSalesDetails agroSalesDetails = new AgroProductSalesDetails()
                    {
                       Discount=item.Discount,
                        DiscountTk=item.DiscountTk,
                         EntryDate=DateTime.Now,
                          EntryUserId=userId,
                           MeasurementId=item.MeasurementId,
                            MeasurementSize=item.MeasurementSize,
                             OrganizationId=orgId,
                              Price=item.Price,
                               ProductSalesInfoId=item.ProductSalesInfoId,
                                Quanity=item.Quanity,
                                 FinishGoodProductInfoId=item.FinishGoodProductInfoId,
                                  ProductSalesDetailsId=item.ProductSalesDetailsId,
                                  ReceipeBatchCode= receipeBatch,
                                  FGRId= FGRId,
                                  QtyKG=item.QtyKG




                    };
                    agroDetails.Add(agroSalesDetails);
                }
                agroSalesProductionInfo.AgroProductSalesDetails = agroDetails;
                _agroProductSalesInfoRepository.Insert(agroSalesProductionInfo);
                isSuccess = _agroProductSalesInfoRepository.Save();


                //paymenttable
                SalesPaymentRegister salesPayment = new SalesPaymentRegister
                {
                    PaymentDate = DateTime.Now,
                    PaymentAmount = agroSalesInfoDTO.PaidAmount,
                    ProductSalesInfoId = agroSalesProductionInfo.ProductSalesInfoId,
                    Remarks = "SalesTime",
                    EntryUserId = userId
                };
                //paymenttable
                _salesPaymentRegisterRepository.Insert(salesPayment);
                isSuccess = _salesPaymentRegisterRepository.Save();

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

        public IEnumerable<InvoiceWiseCollectionSalesReport> GetInvoiceWiseSalesReport(string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<InvoiceWiseCollectionSalesReport>(QueryForInvoiceWiseSalesReport(fromDate, toDate));
        }
        public string QueryForInvoiceWiseSalesReport(string fromDate,string toDate)
        {
            string param = string.Empty;
            string query = string.Empty;

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(SI.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }
            //else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            //{
            //    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
            //    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", fDate);
            //}
            //else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            //{
            //    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
            //    param += string.Format(@" and Cast(sales.EntryDate as date)='{0}'", tDate);
            //}

            //if (!string.IsNullOrEmpty(fromDate))
            //{
            //    param += string.Format(@" and CONVERT(date, SI.EntryDate) = '{0}'", fromDate);
            //}
            //if (!string.IsNullOrEmpty(toDate))
            //{
            //    param += string.Format(@" and CONVERT(date, SI.EntryDate) = '{0}'", toDate);
            //}

            //if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            //{
            //    param += string.Format(@" and CONVERT(date, SI.EntryDate) Between '{0}' and '{0}'", fromDate, toDate);
            //    //CONVERT(date, SI.EntryDate) Between CONVERT(date,'2022-10-27') And CONVERT(date,'2022-10-27')
            //}


            query = string.Format(@"
                SELECT  SI.ProductSalesInfoId, A.AreaName,
        ZoneUserName=( SELECT distinct AU.UserName FROM 
        [Agriculture].[dbo].tblProductSalesInfo SI
        INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId
        INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId),
        
        ZoneUserMobile=( SELECT distinct AU.MobileNo FROM 
        [Agriculture].[dbo].tblProductSalesInfo SI
        INNER JOIN [Agriculture].[dbo].tblZoneUser ZU ON SI.ZoneId=ZU.ZoneId
        INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AU on ZU.UserId=AU.UserId),
        
        TI.TerritoryName,
        TerritoryUserName=(SELECT distinct AUT.UserName FROM 
        [Agriculture].[dbo].tblProductSalesInfo SI
        INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId
        INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId=AUT.UserId ),
        
        TerritoryUserMobile=(SELECT distinct AUT.MobileNo FROM 
        [Agriculture].[dbo].tblProductSalesInfo SI
        INNER JOIN [Agriculture].[dbo].tblTerritoryUser TU ON SI.TerritoryId=TU.TerritoryId
        INNER JOIN [ControlPanelAgro].[dbo].tblApplicationUsers AUT on TU.UserId=AUT.UserId ),
        STI.StockiestName,
        SI.InvoiceNo,
        CONVERT(date,SI.InvoiceDate) AS InvoiceDate,
        SI.TotalAmount AS InvoiceTk,
        Collaction=(SELECT Sum(PSPH.PaymentAmount) FROM tblProductSalesPaymentHistory PSPH Where        SI.ProductSalesInfoId=PSPH.ProductSalesInfoId),
        SI.DueAmount,
        DiscountTk=(SELECT Sum(PSD.DiscountTk) FROM tblProductSalesDetails PSD Where        SI.ProductSalesInfoId=PSD.ProductSalesInfoId),
        
        CONVERT(date,PsPH.PaymentDate) AS PaymentDate,
        PsPH.Remarks, 
        PsPH.PaymentAmount
        --PaymentAmount=(SELECT Sum(PsPHs.PaymentAmount) From [Agriculture].        [dbo].tblProductSalesPaymentHistory PsPHs where         SI.ProductSalesInfoId=PsPHs.ProductSalesInfoId)
        
        
        
        
        FROM [Agriculture].[dbo].tblProductSalesInfo SI
        INNER JOIN [Agriculture].[dbo].tblAreaSetup A on SI.AreaId=A.AreaId
        INNER JOIN [Agriculture].[dbo].tblTerritoryInfos TI on SI.TerritoryId=TI.TerritoryId
        INNER JOIN [Agriculture].[dbo].tblStockiestInfo STI on SI.StockiestId=STI.StockiestId
        INNER JOIN [Agriculture].[dbo].tblProductSalesPaymentHistory PsPH on        SI.ProductSalesInfoId=PsPH.ProductSalesInfoId
                 where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<ProductWiseSalesStementReport> GetProductwisesalesReportDownloadRpt(string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<ProductWiseSalesStementReport>(QueryProductWiseSalesStementReport(fromDate, toDate));
        }

        private string QueryProductWiseSalesStementReport(string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;


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

            query = string.Format(@"select DISTINCT 
              sales.EntryDate,
              FGPN.FinishGoodProductName,
              M.MeasurementName AS PackSize,
              salesD.Quanity AS QtyCTN,
              M.UnitKG,
              salesD.Quanity*M.UnitKG AS QtyKG,
              
              (salesD.Price*salesD.Quanity) AS Total,
              sales.TotalAmount
              
              
              
              from [Agriculture].[dbo].[tblProductSalesInfo] sales
              INNER JOIN [Agriculture].[dbo].[tblProductSalesDetails] salesD
              on sales.ProductSalesInfoId=salesD.ProductSalesInfoId
              INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN
              on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId
              INNER JOIN [Agriculture].[dbo].[tblMeasurement] M
              on salesD.MeasurementId=M.MeasurementId
              Where 1=1{0}", Utility.ParamChecker(param));
                          return query;
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroSalesinfoByStokiestId(long StockiestId)
        {
            return _agroProductSalesInfoRepository.GetAll(d => d.StockiestId == StockiestId);

        }

        public IEnumerable<AgroProductSalesInfoDTO> GetPaymentListInfos(string name, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryPaymentListReport(name,fromDate, toDate));
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
--select InvoiceNo,InvoiceDate,TotalAmount,PaidAmount,DueAmount

 --from tblProductSalesInfo
 --where DueAmount >0

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
sales.TotalAmount
--dbo.fnIntegerToWords(TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText




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


        public IEnumerable<AgroProductSalesInfoDTO> GetSalesAdjustInfos()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForSalesReturnAdjust()).ToList();
        }

        private string QueryForSalesReturnAdjust()
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"

select distinct i.ProductSalesInfoId, i.InvoiceNo,i.InvoiceDate, 
StockiestName=(select StockiestName from tblStockiestInfo where StockiestId=i.StockiestId),
i.TotalAmount,i.DueAmount,i.PaidAmount,
 
 AdjustTotalReturn = isnull( (select sum(sr.ReturnTotalPrice) from tblSalesReturn sr 
 where sr.ProductSalesInfoId= i.ProductSalesInfoId and sr.Status='ADJUST'),0),

 NotAdjustTotalReturn = isnull( (select sum(sr.ReturnTotalPrice) from tblSalesReturn sr 
 where sr.ProductSalesInfoId= i.ProductSalesInfoId and sr.Status='NOTADJUST'),0)


 from tblProductSalesInfo i
 inner join tblProductSalesDetails d on i.ProductSalesInfoId=d.ProductSalesInfoId


Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }
    }
}
