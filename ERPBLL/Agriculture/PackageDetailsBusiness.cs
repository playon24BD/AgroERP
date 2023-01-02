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

        public IEnumerable<PackageDetailsDTO> GetPackageDetailsbySales(long PackageId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PackageDetailsDTO>(QueryForPackageDetailsbySales(PackageId)).ToList();
        }
        private string QueryForPackageDetailsbySales(long PackageId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (PackageId != 0 && PackageId > 0)
            {
                param += string.Format(@" and d.PackageId={0}", PackageId);
            }

            query = string.Format(@"

select concat(r.FGRQty,'(',u.UnitName,')') as QtyKG, d.AccessoriesId,d.PackageId, isnull(m.MeasurementId,0) as MeasurementId,isnull(r.FGRId,0)as FGRId ,d.Amount, isnull(i.FinishGoodProductId,0) as FinishGoodProductId,
CASE WHEN i.FinishGoodProductName is null THEN a.AccessoriesName ELSE i.FinishGoodProductName END AS FinishGoodProductName,
m.MeasurementName,
Rate=(d.Amount/d.Quanity),
d.Quanity,d.Status,d.Amount,d.EntryDate
from tblPackageDetails d
left join tblFinishGoodProductInfo i on d.FinishGoodProductId=i.FinishGoodProductId
left join tblMeasurement m on d.MeasurementId=m.MeasurementId
left join tblAccessoriesInfo a on a.AccessoriesId=d.AccessoriesId
left join tblFinishGoodRecipeInfo r on d.FGRId= r.FGRId
left join  tblAgroUnitInfo u on r.UnitId=u.UnitId

where 1=1 {0}", Utility.ParamChecker(param));

            return query;
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
d.AccessoriesId,d.PackageId,i.FinishGoodProductName,m.MeasurementName,a.AccessoriesName,
Rate=(d.Amount/d.Quanity),
d.Quanity,d.Status,d.Amount,d.EntryDate
from tblPackageDetails d
left join tblFinishGoodProductInfo i on d.FinishGoodProductId=i.FinishGoodProductId
left join tblMeasurement m on d.MeasurementId=m.MeasurementId
left join tblAccessoriesInfo a on a.AccessoriesId=d.AccessoriesId

where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

    }
}
