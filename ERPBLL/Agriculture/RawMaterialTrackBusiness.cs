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
    public class RawMaterialTrackBusiness : IRawMaterialTrack
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;


        //contractor
        public RawMaterialTrackBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialTrack> GetAllRawMaterialTruck()
        {
            return _rawMaterialTrackInfoRepository.GetAll().ToList();
        }

        public IEnumerable<RawMaterialTrackDTO> GetMainStockInOutInfos(string name)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialTrackDTO>(QueryForRawMaterialMainINOUTQTY(name)).ToList();
        }
        private string QueryForRawMaterialMainINOUTQTY(string name)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (name != null && name != "")
            {
                param += string.Format(@" and RM.RawMaterialName like '%{0}%'", name);
            }
            query = string.Format(@"
SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,
StockIN= isnull((SELECT sum(t.Quantity) FROM  tblRawMaterialTrackInfo t
where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),0),
StockOut=isnull((SELECT sum(t.Quantity) FROM  tblRawMaterialTrackInfo t
where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),0),

StockINReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where rr.ReturnType ='Good' and rr.Status='Approved' and rr.RawMaterialId=RM.RawMaterialId),0),
StockDamagReturn=isnull((SELECT sum(rr.Quantity) FROM  tblReturnRawMaterial rr
where rr.ReturnType ='Damage' and rr.Status='Approved' and rr.RawMaterialId=RM.RawMaterialId),0),

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
    }
}
