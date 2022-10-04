using ERPBLL.Agriculture.Interface;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class ZoneDetailBusiness:IZoneDetail
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ZoneDetailRepository _zoneDetailRepository;

        public ZoneDetailBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._zoneDetailRepository = new ZoneDetailRepository(this._agricultureUnitOfWork);
        }
    }
}
