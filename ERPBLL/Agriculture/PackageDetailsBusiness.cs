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
    public class PackageDetailsBusiness : IPackageDetails
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PackageDetailsRepository _packageDetailsRepository;

        public PackageDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._packageDetailsRepository = new PackageDetailsRepository(this._agricultureUnitOfWork);
        }



        public IEnumerable<PackageDetails> GetPackageDetailsBY(long PackageId)
        {
            return _packageDetailsRepository.GetAll(i => i.PackageId == PackageId).ToList();
        }

        public IEnumerable<PackageDetailsDTO> GetPackageDetailsView(long id,long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PackageDetailsDTO>(QueryForPackageDetailsView(id,orgId)).ToList();
        }

        private string QueryForPackageDetailsView(long id, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (id != 0 && id > 0)
            {
                param += string.Format(@" and d.PackageId={0}", id);
            }

            query = string.Format(@"

select 
d.PackageId,i.FinishGoodProductName,m.MeasurementName,
Rate=(d.Amount/d.Quanity),
d.Quanity,d.Status,d.Amount,d.EntryDate
from tblPackageDetails d
inner join tblFinishGoodProductInfo i on d.FinishGoodProductId=i.FinishGoodProductId
inner join tblMeasurement m on d.MeasurementId=m.MeasurementId

 
where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

    }
}
