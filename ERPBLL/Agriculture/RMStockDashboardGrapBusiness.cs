using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DTOModels;
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
SELECT Distinct 
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
    }
}
