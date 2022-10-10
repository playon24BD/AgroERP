using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class DivisionInfoBusiness:IDivisionInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly DivisionInfoRepository _divisionInfoRepository;

        public DivisionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._divisionInfoRepository = new DivisionInfoRepository(this._agricultureUnitOfWork);
        }

    }
}
