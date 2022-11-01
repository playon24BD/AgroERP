using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class AgroProductSalesDetailsBusiness : IAgroProductSalesDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AgroProductSalesDetailsRepository _agroProductSalesDetailsRepository;
        public AgroProductSalesDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._agroProductSalesDetailsRepository = new AgroProductSalesDetailsRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByInfoId(long infoId, long orgId)
        {
            return _agroProductSalesDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.ProductSalesInfoId == infoId).ToList();
        }


        public IEnumerable<AgroProductSalesDetailsDTO> GetAllAgroSalesDetailsInfos(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesDetailsDTO>(QueryForAgroSalesDetailsInfoss(orgId)).ToList();
        }

        private string QueryForAgroSalesDetailsInfoss(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and sales.OrganizationId={0}", orgId);
            //if (ProductId != null && ProductId > 0)
            //{
            //    param += string.Format(@" and sales.ProductSalesInfoId={0}", ProductId);
            //}
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

        public IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByinfoId(long infoId, long orgId)
        {
            return _agroProductSalesDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.ProductSalesInfoId == infoId).ToList();
        }
    }
}
