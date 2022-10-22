using ERPBLL.Agriculture.Interface;
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
        //private readonly AppUserRepository appUserRepository; // repo
        private readonly IAppUserBusiness _appUserBusiness;
        private readonly IStockiestInfo _stockiestInfo;
        private readonly IAreaSetupBusiness _areaSetupBusiness;
        private readonly IDivisionInfo _divisionInfo;
        private readonly IRegionSetup _regionSetup;
        private readonly IZoneSetup _zoneSetup;
        private readonly IUserAssignBussiness _userAssignBussiness;
        private readonly IUserInfo _userInfo;
        public AgroProductSalesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAppUserBusiness appUserBusiness,IStockiestInfo stockiestInfo, IAreaSetupBusiness areaSetupBusiness, IDivisionInfo divisionInfo, IRegionSetup regionSetup, IZoneSetup zoneSetup, IUserAssignBussiness userAssignBussiness, IUserInfo userInfo)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);
            this._appUserBusiness = appUserBusiness;
            this._stockiestInfo = stockiestInfo;
            this._areaSetupBusiness = areaSetupBusiness;
            this._divisionInfo = divisionInfo;
            this._regionSetup = regionSetup;
            this._zoneSetup = zoneSetup;
            this._userAssignBussiness = userAssignBussiness;
            this._userInfo = userInfo;
        }

        public AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId)
        {
            return _agroProductSalesInfoRepository.GetOneByOrg(f => f.ProductSalesInfoId == id);
        }

        public IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId)
        {
            return _agroProductSalesInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }

        public IEnumerable<AgroProductSalesInfo> GetUserName(long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId)
        {

            bool isSuccess = false;

            var ChallanNo = "CHA-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            var VehicleNumber = "VEH-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            

            if (agroSalesInfoDTO.ProductSalesInfoId == 0)
            {

                    var stockeiestId = _appUserBusiness.GetId(agroSalesInfoDTO.StockiestId, orgId).StockiestId;

                    var territoryId = _stockiestInfo.GetStockiestInfoById(1, orgId).TerritoryId;

                var divisionId = _divisionInfo.GetDivisionInfoById(1, orgId).DivisionId;
                var regionId = _regionSetup.GetRegionNamebyId(1, orgId).RegionId;

                //var userAssignId = _userAssignBussiness.GetUserAssignById(stockeiestId.Value, orgId).UserAssignId;

                //var userInfo = _userInfo.GetUserInfoById(stockeiestId.Value, orgId).UserId;
                
                //var zoneId = _zoneSetup.GetZoneNamebyId(stockeiestId.Value, orgId).ZoneId;

                var areaId = _areaSetupBusiness.GetAreaInfoById(1, orgId).AreaId;


                AgroProductSalesInfo agroSalesProductionInfo = new AgroProductSalesInfo
                {

                    ChallanNo = ChallanNo,
                    VehicleNumber = VehicleNumber,
                    ProductSalesInfoId = agroSalesInfoDTO.ProductSalesInfoId,
                    DeliveryPlace = agroSalesInfoDTO.DeliveryPlace,
                    DoADO_Name = agroSalesInfoDTO.DoADO_Name,
                    DriverName = agroSalesInfoDTO.DriverName,
                    InvoiceDate = agroSalesInfoDTO.InvoiceDate,
                    PaymentMode = agroSalesInfoDTO.PaymentMode,
                    VehicleType = agroSalesInfoDTO.VehicleType,
                    //UserAssignId = agroSalesInfoDTO.UserAssignId,
                    StockiestId = 1,
                    TerritoryId = territoryId,
                    DivisionId = divisionId,
                    RegionId = regionId,
                    //UserAssignId=userAssignId,
                   //UserId = userInfo,
                    AreaId = areaId,
                    //ZoneId=zoneId,
                    ChallanDate =DateTime.Now,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                     InvoiceNo=agroSalesInfoDTO.InvoiceNo,
                     Depot=agroSalesInfoDTO.Depot,
                     Do_ADO_DA=agroSalesInfoDTO.Do_ADO_DA

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
            }
            
            return isSuccess;
        }
    }
}
