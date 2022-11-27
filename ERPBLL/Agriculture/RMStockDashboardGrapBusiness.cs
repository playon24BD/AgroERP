using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RMStockDashboardGrapBusiness : IRMStockDashboardGrap
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;
        public RMStockDashboardGrapBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
        }
        public IEnumerable<RawMaterialTrackDTO> GetMainStockRMName(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialTrackDTO>(QueryForGetMainStockRMName  (orgId)).ToList();
        }
        private string QueryForGetMainStockRMCurrentStock(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;


            //if (orgId != 0)
            //{
            //    param += string.Format(@" and RM.RawMaterialName like '%{0}%'", name);
            //}
            query = string.Format(@"
SELECT Distinct RM.RawMaterialName,
CurrentStock= isnull( isnull((SELECT sum(t.Quantity) FROM  tblRawMaterialTrackInfo t
where t.IssueStatus ='StockIn'and t.RawMaterialId=RM.RawMaterialId),0)- isnull((SELECT sum(t.Quantity) FROM  tblRawMaterialTrackInfo t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0)+ isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where rr.ReturnType ='Good' and rr.Status='Approved' and rr.RawMaterialId=RM.RawMaterialId),0),0)
FROM  
tblRawMaterialTrackInfo t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
      where 1=1    {0}",
            Utility.ParamChecker(param));
            return query;
        }
        public IEnumerable<RawMaterialTrackDTO> GetMainStockRMCurrentStock(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialTrackDTO>(QueryForGetMainStockRMCurrentStock(orgId)).ToList();
        }
        private string QueryForGetMainStockRMName(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;


            //if (orgId != 0)
            //{
            //    param += string.Format(@" and RM.RawMaterialName like '%{0}%'", name);
            //}
            query = string.Format(@"
SELECT Distinct RM.RawMaterialName

FROM  
tblRawMaterialTrackInfo t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
      where 1=1    {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetMainStockFGProductName(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForGetMainStockFGName(orgId)).ToList();
        }
        private string QueryForGetMainStockFGName(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"
select Distinct concat (p.FinishGoodProductName,re.FGRQty,u.UnitName) AS FinishGoodProductName from FinishGoodProductionInfoes fp
inner join tblFinishGoodProductInfo p on fp.FinishGoodProductId=p.FinishGoodProductId
inner join tblFinishGoodRecipeInfo re on fp.FGRId=re.FGRId
inner join tblAgroUnitInfo u on re.UnitId= u.UnitId
      where 1=1    {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetMainStockFGProductCurrentStock(long orgId)
        {
           return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForGetMainStockFGCurrentStock(orgId)).ToList();
        }
        private string QueryForGetMainStockFGCurrentStock(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"
select Distinct concat (p.FinishGoodProductName,fr.FGRQty,un.UnitName) AS FinishGoodProductName,

CurrentStock=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId and fgp.Status='Approved'),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId and sd.Status is null),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0)

from FinishGoodProductionInfoes fgp
inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId
inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId
inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId

      where 1=1    {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<Last30DaysSalesGraph> Last30DaysSellsChart(string fromDate, string toDate, long orgId)
        {
            IEnumerable<Last30DaysSalesGraph> data = new List<Last30DaysSalesGraph>();
            if (orgId > 0)
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<Last30DaysSalesGraph>(
                string.Format(@"Exec [dbo].[spLast30DaysSellsChart] '{0}','{1}',{2}", fromDate, toDate, orgId)).ToList();
            }
            return data;
        }
    }
}
