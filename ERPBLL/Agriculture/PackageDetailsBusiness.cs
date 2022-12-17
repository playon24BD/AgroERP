using ERPBLL.Agriculture.Interface;
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

    }
}
