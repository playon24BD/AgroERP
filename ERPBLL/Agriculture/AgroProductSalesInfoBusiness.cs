using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
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
        public AgroProductSalesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAppUserBusiness appUserBusiness,IStockiestInfo stockiestInfo,ITerritorySetup territorySetup, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IUserAssignBussiness userAssignBussiness, IUserInfo userInfo)
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

        public IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long orgId, long? ProductId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesInfoDTO>(QueryForAgroSalesInfoss(orgId, ProductId)).ToList();
        }

        private string QueryForAgroSalesInfoss(long orgId, long? ProductId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and sales.OrganizationId={0}", orgId);
            if (ProductId != null && ProductId > 0)
            {
                param += string.Format(@" and sales.ProductSalesInfoId={0}", ProductId);
            }
            query = string.Format(@"	select sales.TotalAmount,sales.DueAmount,sales.PaidAmount,sales.InvoiceNo,sales.ProductSalesInfoId,sales.InvoiceDate,stock.StockiestName
from tblProductSalesInfo sales
inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId 
select sales.InvoiceNo,sales.InvoiceDate,stock.StockiestName
from tblProductSalesInfo sales
inner join tblStockiestInfo stock on sales.StockiestId=stock.StockiestId 

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
                                  ProductSalesDetailsId=item.ProductSalesDetailsId
                                  


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
    }
}
