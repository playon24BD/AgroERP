using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class PackageInfoBusiness : IPackageInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PackageInfoRepository  _packageInfoRepository;

        public PackageInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._packageInfoRepository = new PackageInfoRepository(this._agricultureUnitOfWork);
        }



    }
}
