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
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ZoneSetupRepository _zoneSetupRepository;


        //contractor
        public ZoneSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._zoneSetupRepository = new ZoneSetupRepository(this._agricultureUnitOfWork);
        }


        public IEnumerable<ZoneSetup> GetAllZoneSetup(long OrgId)
        {
            return _zoneSetupRepository.GetAll(x => x.OrganizationId == OrgId).ToList();

        }

        public ZoneSetup GetZoneNamebyId(long zoneId, long orgId)
        {
            return _zoneSetupRepository.GetOneByOrg(x => x.ZoneId == zoneId && x.OrganizationId == orgId);
        }

        public bool SaveZoneInfo(List<ZoneSetupDTO> detailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<ZoneSetup> ZoneSetup = new List<ZoneSetup>();

            foreach(var item in detailsDTO)
            {
                ZoneSetup zoneSetups = new ZoneSetup()
                {
                    OrganizationId = item.OrganizationId,
                    ZoneId = item.ZoneId,
                    ZoneName = item.ZoneName,
                    ZoneAsignName = item.ZoneAsignName,
                    MobileNumber = item.MobileNumber,
                    Remarks = item.Remarks,

                    EntryUserId = userId,
                    UpdateUserId = userId,
                    EntryDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    RoleId = 0

                };
                _zoneSetupRepository.Insert(zoneSetups);
            }

            IsSuccess = _zoneSetupRepository.Save();
            return IsSuccess;
        }

    }
}
