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
    public class AccessoriesPurchaseDetailsBusiness : IAccessoriesPurchaseDetails
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AccessoriesPurchaseDetailsRepository _accessoriesPurchaseDetailsRepository;

        public AccessoriesPurchaseDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._accessoriesPurchaseDetailsRepository = new AccessoriesPurchaseDetailsRepository(this._agricultureUnitOfWork);
        }
        public IEnumerable<AccessoriesPurchaseDetailsDTO> GetAccessoriesDetailsList(long? id)
        {

            return _agricultureUnitOfWork.Db.Database.SqlQuery<AccessoriesPurchaseDetailsDTO>(QueryForAccessoriesDetailsList(id));
        }

        private string QueryForAccessoriesDetailsList(long? id)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                if (id != null && id > 0)
                {
                    param += string.Format(@" and d.AccessoriesPurchaseInfoId={0}", id);
                }

                query = string.Format(@"

        


select d.AccessoriesPurchaseInfoId, i.AccessoriesName,d.Quantity,d.UnitPrice,d.EntryDate from tblAccessoriesPurchaseDetails d
inner join tblAccessoriesInfo i
on i.AccessoriesId=d.AccessoriesId
where 1=1 {0}

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
