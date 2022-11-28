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
  
        public AgroProductSalesDetails AgroProductSalesDetailsbyInfoId(long productSalesinfoId)
        {
            return _agroProductSalesDetailsRepository.GetOneByOrg(a=>a.ProductSalesInfoId==productSalesinfoId);
        }
        public AgroProductSalesDetails AgroProductSalesDetailsbyId(long id)
        {
            return _agroProductSalesDetailsRepository.GetOneByOrg(a => a.ProductSalesDetailsId == id);
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
sales.TotalAmount
--dbo.fnIntegerToWords(TotalAmount)+' '+'Taka Only ..........' AS TotalAmountText




from [Agriculture].[dbo].[tblProductSalesInfo] sales
INNER JOIN [Agriculture].[dbo].[tblProductSalesDetails] salesD
on sales.ProductSalesInfoId=salesD.ProductSalesInfoId

INNER JOIN [Agriculture].[dbo].[tblFinishGoodProductInfo] FGPN
on salesD.FinishGoodProductInfoId=FGPN.FinishGoodProductId

LEFT JOIN [ControlPanelAgro].[dbo].[tblApplicationUsers] AU
on AU.UserId=sales.UserId
 Left JOIN [Agriculture].[dbo].[tblStockiestInfo] ST
 on ST.StockiestId=sales.StockiestId
LEFT JOIN [Agriculture].[dbo].[tblTerritoryInfos] TE
on TE.TerritoryId=ST.TerritoryId
Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByinfoId(long infoId, long orgId)
        {
            return _agroProductSalesDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.ProductSalesInfoId == infoId).ToList();
        }

        public IEnumerable<AgroProductSalesDetailsDTO> GetAgroSalesDetailsByInfoIdGet(long infoId, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesDetailsDTO>(QueryForGetAgroSalesDetailsByInfoIdGet(infoId, orgId)).ToList();
        }

        private string QueryForGetAgroSalesDetailsByInfoIdGet(long infoId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (infoId != 0 && infoId > 0)
            {
                param += string.Format(@" and SD.ProductSalesInfoId={0}", infoId);
            }
          

            query = string.Format(@"	
SELECT Distinct SD.FGRId,SD.QtyKG,SD.ProductSalesDetailsId,SD.BoxQuanity,m.MeasurementId,
SD.ProductSalesInfoId,SD.Price,SD.Discount,SD.FinishGoodProductInfoId,FGP.FinishGoodProductName,SD.MeasurementSize,
SD.Quanity,
ReturnQTY=ISNULL((SELECT SUM(SR.BoxQuanity) from tblSalesReturn SR where SD.ProductSalesInfoId=SR.ProductSalesInfoId and SR.FinishGoodProductInfoId=SD.FinishGoodProductInfoId and SR.FGRId=SD.FGRId and SR.Status='ADJUST'),0),
ReturnTotalQTY=ISNULL((SELECT SUM(SR.ReturnQuanity) from tblSalesReturn SR where SD.ProductSalesInfoId=SR.ProductSalesInfoId and  SR.FinishGoodProductInfoId=SD.FinishGoodProductInfoId and SR.FGRId=SD.FGRId and SR.Status='ADJUST'),0),

RT=ISNULL((SELECT 
 CASE When count(*) > 0 then 1
 else 0
 END AS myValue
 from tblSalesReturn sr where sr.FinishGoodProductInfoId=SD.FinishGoodProductInfoId and sr.ProductSalesInfoId=SD.ProductSalesInfoId and sr.FGRId=SD.FGRId and  sr.Status='NOTADJUST'),0),


CurrentQTY= ISNULL(SD.BoxQuanity,0)-ISNULL((SELECT SUM(SR.BoxQuanity) from tblSalesReturn SR where SD.ProductSalesInfoId=SR.ProductSalesInfoId and SR.FinishGoodProductInfoId=SD.FinishGoodProductInfoId and SR.FGRId=SD.FGRId and SR.Status='ADJUST'),0),

TotalCurrentQTY= ISNULL(SD.Quanity,0)-ISNULL((SELECT SUM(SR.ReturnQuanity) from tblSalesReturn SR where SD.ProductSalesInfoId=SR.ProductSalesInfoId and  SR.FinishGoodProductInfoId=SD.FinishGoodProductInfoId and SR.FGRId=SD.FGRId and SR.Status='ADJUST'),0)

from tblProductSalesDetails SD
INNER JOIN tblFinishGoodProductInfo FGP on SD.FinishGoodProductInfoId=FGP.FinishGoodProductId
INNER JOIN tblMeasurement M on SD.MeasurementId=M.MeasurementId
inner join tblProductSalesInfo si on SD.ProductSalesInfoId = si.ProductSalesInfoId

                Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<AgroProductSalesDetailsDTO> GetSalesDetailsByInfoId(long ProductSalesInfoId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesDetailsDTO>(QueryForGetAgroSalesDetailsmain(ProductSalesInfoId)).ToList();
        }


        private string QueryForGetAgroSalesDetailsmain(long ProductSalesInfoId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (ProductSalesInfoId != 0 && ProductSalesInfoId > 0)
            {
                param += string.Format(@" and i.ProductSalesInfoId={0}", ProductSalesInfoId);
            }


            query = string.Format(@"	
select f.FinishGoodProductName,i.ProductSalesInfoId,i.InvoiceNo,d.ProductSalesDetailsId,d.FinishGoodProductInfoId,d.Quanity,d.BoxQuanity,d.Price,d.Price,d.Discount,d.DiscountTk,d.MeasurementSize,d.ReceipeBatchCode,d.QtyKG,(d.Quanity*d.Price-DiscountTk)as ProductTotal,
 RT=ISNULL((SELECT 
 CASE When count(*) > 0 then 1
 else 0
 END AS myValue
 from tblSalesReturn sr where sr.FinishGoodProductInfoId=d.FinishGoodProductInfoId and sr.ProductSalesInfoId=d.ProductSalesInfoId and sr.FGRId=d.FGRId and  sr.Status='ADJUST'),0)

from tblProductSalesInfo i
inner join tblProductSalesDetails d on i.ProductSalesInfoId=d.ProductSalesInfoId
inner join tblFinishGoodProductInfo f on d.FinishGoodProductInfoId=f.FinishGoodProductId



                Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<AgroProductSalesDetailsDTO> GetSalesEditByInfoId(long ProductSalesInfoId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroProductSalesDetailsDTO>(QueryForGetAgroSalesEdit(ProductSalesInfoId)).ToList();
        }

        private string QueryForGetAgroSalesEdit(long ProductSalesInfoId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (ProductSalesInfoId != 0 && ProductSalesInfoId > 0)
            {
                param += string.Format(@" and i.ProductSalesInfoId={0}", ProductSalesInfoId);
            }


            query = string.Format(@"	
select f.FinishGoodProductName,i.ProductSalesInfoId,i.InvoiceNo,d.ProductSalesDetailsId,d.FinishGoodProductInfoId,d.Quanity,d.BoxQuanity,d.Price,d.Price,d.Discount,d.DiscountTk,d.MeasurementSize,d.ReceipeBatchCode,d.QtyKG,(d.Quanity*d.Price-DiscountTk)as ProductTotal,
 RT=ISNULL((SELECT 
 CASE When count(*) > 0 then 1
 else 0
 END AS myValue
 from tblSalesReturn sr where sr.FinishGoodProductInfoId=d.FinishGoodProductInfoId and sr.ProductSalesInfoId=d.ProductSalesInfoId and sr.FGRId=d.FGRId and  sr.Status='ADJUST'),0)

from tblProductSalesInfo i
inner join tblProductSalesDetails d on i.ProductSalesInfoId=d.ProductSalesInfoId
inner join tblFinishGoodProductInfo f on d.FinishGoodProductInfoId=f.FinishGoodProductId



                Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }



    }
}
