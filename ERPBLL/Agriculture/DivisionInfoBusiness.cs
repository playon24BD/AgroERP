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

        public IEnumerable<DivisionInfoDTO> GetDivisionInfos(long orgId, long? divisionId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<DivisionInfoDTO>(QueryForDivisionInfoss(orgId, divisionId)).ToList();
        }

        private string QueryForDivisionInfoss(long orgId, long? divisionId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and OrganizationId={0}", orgId);
            if (divisionId != null && divisionId > 0)
            {
                param += string.Format(@" and DivisionId={0}", divisionId);
            }
            query = string.Format(@"
           select d.DivisionName,d.DivisionAssignName,z.ZoneName,d.MobileNumber,d.Remarks
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
                    DivisionAssignName = item.DivisionAssignName,
                    MobileNumber = item.MobileNumber,
                    ZoneId = item.ZoneId,
                    Remarks = item.Remarks,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                };
                _divisionInfoRepository.Insert(division);
            }

            isSuccess = _divisionInfoRepository.Save();
            return isSuccess;
        }
    }
}
