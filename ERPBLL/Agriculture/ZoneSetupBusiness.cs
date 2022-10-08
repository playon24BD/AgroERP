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

        public bool SaveZonetInfo(ZoneSetupDTO infoDTO, long orgId, long userId)
        {

            bool savesuccess = false;

            //validation same name
            var zonename = _zoneSetupRepository.GetAll(x => x.ZoneName == infoDTO.ZoneName).FirstOrDefault();
            if(zonename != null)
            {
                return savesuccess;

            }
            else
            {

                if (infoDTO.ZoneId == 0)
                {
                    ZoneSetup zoneSetupinfo = new ZoneSetup()
                    {
                        ZoneName = infoDTO.ZoneName,
                        Remarks = infoDTO.Remarks,
                        MobileNumber = infoDTO.MobileNumber,
                        ZoneAsignName = infoDTO.ZoneAsignName,
                        OrganizationId = infoDTO.OrganizationId,

                        EntryUserId = userId,
                        UpdateUserId = infoDTO.UpdateUserId,
                        EntryDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        RoleId = infoDTO.RoleId

                    };
                    _zoneSetupRepository.Insert(zoneSetupinfo);
                }
                else
                {
                    ZoneSetup zoneSetup = new ZoneSetup();
                    zoneSetup = GetZoneNamebyId(infoDTO.ZoneId, orgId);
                    zoneSetup.ZoneName = infoDTO.ZoneName;
                    zoneSetup.ZoneAsignName = infoDTO.ZoneAsignName;
                    zoneSetup.Remarks = infoDTO.Remarks;
                    zoneSetup.MobileNumber = infoDTO.MobileNumber;

                    zoneSetup.OrganizationId = orgId;
                    zoneSetup.EntryUserId = zoneSetup.EntryUserId;
                    zoneSetup.UpdateUserId = userId;
                    zoneSetup.EntryDate = zoneSetup.EntryDate;
                    zoneSetup.UpdateDate = DateTime.Now;
                    zoneSetup.RoleId = infoDTO.RoleId;

                }
                savesuccess = _zoneSetupRepository.Save();

                return savesuccess;

            }

            
        }
    }
}
