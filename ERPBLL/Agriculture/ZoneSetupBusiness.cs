using ERPBLL.Agriculture.Interface;
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
    public class ZoneSetupBusiness : IZoneSetup
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly ZoneSetupRepository _depotSetupRepository;

        public IEnumerable<ZoneSetup> GetAllZoneSetup(long OrgId)
        {
            throw new NotImplementedException();
        }

        public ZoneSetup GetZoneNamebyId(long zoneId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveDepotInfo(ZoneSetupDTO infoDTO, long orgId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
