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
    public class AccessoriesTrackInfoBusiness : IAccessoriesTrackInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AccessoriesTrackInfoRepository _accessoriesTrackInfoRepository;

        public AccessoriesTrackInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._accessoriesTrackInfoRepository = new AccessoriesTrackInfoRepository(this._agricultureUnitOfWork);
        }
        public IEnumerable<AccessoriesTrackInfoDTO> GetAccessoriesPurchaseStock()
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<AccessoriesTrackInfoDTO>(QueryForAccessoriesStockList());
        }

        private string QueryForAccessoriesStockList()
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                query = string.Format(@"

select distinct ai.AccessoriesName,ti.AccessoriesId,

StockIn=ISNULL((select sum(ti.Quantity) from tblAccessoriesTrackInfo ti where 
ti.IssueStatus='StockIn' and ti.AccessoriesId=ai.AccessoriesId),0),

StockOut=ISNULL((select sum(ti.Quantity) from tblAccessoriesTrackInfo ti where
ti.IssueStatus='StockOut' and ti.AccessoriesId=ai.AccessoriesId),0),

TotalStock=isnull(ISNULL((select sum(ti.Quantity) from tblAccessoriesTrackInfo ti where 
ti.IssueStatus='StockIn' and ti.AccessoriesId=ai.AccessoriesId),0)-ISNULL((select sum(ti.Quantity) from tblAccessoriesTrackInfo ti where
ti.IssueStatus='StockOut' and ti.AccessoriesId=ai.AccessoriesId),0),0)

 from tblAccessoriesTrackInfo ti
inner join tblAccessoriesInfo ai on ai.AccessoriesId=ti.AccessoriesId

where 1=1 {0} order by ti.AccessoriesId desc

", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
