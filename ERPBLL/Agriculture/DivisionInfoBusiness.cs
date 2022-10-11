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
    public class DivisionInfoBusiness : IDivisionInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly DivisionInfoRepository _divisionInfoRepository;

        public DivisionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._divisionInfoRepository = new DivisionInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<DivisionInfo> GetAllDivisionSetup(long OrgId)
        {
            return _divisionInfoRepository.GetAll(o => o.OrganizationId == OrgId).ToList();
        }

        public IEnumerable<DivisionInfoDTO> GetDivisionInfos(long orgId, long? divisionId, long? zoneId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<DivisionInfoDTO>(QueryForDivisionInfoss(orgId, divisionId, zoneId)).ToList();
        }

        private string QueryForDivisionInfoss(long orgId, long? divisionId, long? zoneId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and d.OrganizationId={0}", orgId);
            if (divisionId != null && divisionId > 0)
            {
                param += string.Format(@" and d.DivisionId={0}", divisionId);
            }
            if (zoneId != null && zoneId > 0)
            {
                param += string.Format(@" and z.ZoneId={0}", zoneId);
            }

            query = string.Format(@"
           select d.DivisionId,z.ZoneId,d.DivisionName,z.ZoneName,d.Status
from tblDivisionInfo d
inner join tblZoneInfos z on d.ZoneId=z.
ZoneId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveDivisionInfo(List<DivisionInfoDTO> infoDTO, long userId, long orgId)
        {
            bool isSuccess = false;

            List<DivisionInfo> divisionInfos = new List<DivisionInfo>();

            foreach (var item in infoDTO)
            {
                DivisionInfo division = new DivisionInfo()
                {
                    OrganizationId = orgId,
                    DivisionName = item.DivisionName,
                    ZoneId = item.ZoneId,
                    Status = "Active",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                };
                _divisionInfoRepository.Insert(division);
            }

            //DivisionInfo info = new DivisionInfo();
            //info = GetDivisionInfoById(infoDTO.divisionId, orgId);
            //info.DivisionName = infoDTO.DivisionName;
            //info.ZoneName = infoDTO.ZoneName;
            //_divisionInfoRepository.Update(info);

            isSuccess = _divisionInfoRepository.Save();
            return isSuccess;
        }

        public DivisionInfo GetDivisionInfoById(long divisionId, long orgId)
        {
            return _divisionInfoRepository.GetOneByOrg(r => r.DivisionId == divisionId && r.OrganizationId == orgId);
        }
    }
}
