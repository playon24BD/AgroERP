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

        public IEnumerable<DivisionInfoDTO> GetDivisionInfos(string name,long? divisionId, long? zoneId, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<DivisionInfoDTO>(QueryForDivisionInfoss( name,divisionId, zoneId, orgId)).ToList();
        }

        private string QueryForDivisionInfoss(string name,long? divisionId, long? zoneId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and d.OrganizationId={0}", orgId);
            if (divisionId != null && divisionId > 0)
            {
                param += string.Format(@" and d.DivisionId like '%{0}%'", divisionId);
            }
            if (zoneId != null && zoneId > 0)
            {
                param += string.Format(@" and z.ZoneId={0}", zoneId);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and d.DivisionName like '%{0}%'", name);
            }
            query = string.Format(@"
           select d.DivisionId,z.ZoneId,d.DivisionName,z.ZoneName,d.Status
from tblDivisionInfo d
inner join tblZoneInfos z on d.ZoneId=z.
ZoneId
            where 1=1  {0} order by d.DivisionId desc",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveDivisionInfo(List<DivisionInfoDTO> infoDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            //if (updateDTOs.DivisionId !=0)
            //{

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

            //}
            //else
            //{
            //    DivisionInfo info = new DivisionInfo();
            //    info = GetDivisionInfoById(updateDTOs.DivisionId, orgId);
            //    info.DivisionName = updateDTOs.DivisionName;
            //    info.ZoneId = updateDTOs.ZoneId;
            //    _divisionInfoRepository.Update(info);
            //}



            isSuccess = _divisionInfoRepository.Save();
            return isSuccess;
        }

        public DivisionInfo GetDivisionInfoById(long divisionId, long orgId)
        {
            return _divisionInfoRepository.GetOneByOrg(r => r.DivisionId == divisionId && r.OrganizationId == orgId);
        }
        public IEnumerable<DivisionInfoDTO> GetAllDivisionDetails(long ZoneId, long orgId)
        {
            IEnumerable<DivisionInfoDTO> details = new List<DivisionInfoDTO>();
            details = this._agricultureUnitOfWork.Db.Database.SqlQuery<DivisionInfoDTO>(string.Format(@"SELECT D.DivisionId,D.DivisionName 
From [Agriculture].[dbo].tblDivisionInfo D
Inner Join [Agriculture].[dbo].tblZoneInfos Z on D.ZoneId=Z.ZoneId Where 1=1 and D.ZoneId={0} and D.OrganizationId={1} ", ZoneId, orgId)).ToList();
            return details;

        }

        public bool UpdateDivision(DivisionInfoDTO updateDTOs, long userId, long orgId)
        {
            bool isSuccess = false;

            DivisionInfo info = new DivisionInfo();
            info = GetDivisionInfoById(updateDTOs.DivisionId, orgId);
            info.DivisionName = updateDTOs.DivisionName;
            info.ZoneId = updateDTOs.ZoneId;
            info.Status = updateDTOs.Status;
            info.UpdateDate = DateTime.Now;
            info.UpdateUserId = userId;
            _divisionInfoRepository.Update(info);

            isSuccess = _divisionInfoRepository.Save();
            return isSuccess;

        }
    }
}
