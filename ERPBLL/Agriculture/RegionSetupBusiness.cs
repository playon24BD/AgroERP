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
    public class RegionSetupBusiness : IRegionSetup
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RegionSetupRepository _regionSetupRepository;

        public RegionSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._regionSetupRepository = new RegionSetupRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RegionSetupDTO> GetAllRegionDetails(long DivisionId, long orgId)
        {
            IEnumerable<RegionSetupDTO> details = new List<RegionSetupDTO>();
            details = this._agricultureUnitOfWork.Db.Database.SqlQuery<RegionSetupDTO>(string.Format(@"SELECT R.RegionId,R.RegionName 
From [Agriculture].[dbo].tblRegionInfos R
Inner Join [Agriculture].[dbo].tblDivisionInfo D on R.DivisionId=D.DivisionId
where R.DivisionId={0} and D.OrganizationId={1} ", DivisionId, orgId)).ToList();
            return details;
        }

        public IEnumerable<RegionSetup> GetAllRegionSetup(long OrgId)
        {
            return _regionSetupRepository.GetAll(x => x.OrganizationId == OrgId).ToList();
        }

        private string QueryForRegion(long orgId, long? regionId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and OrganizationId={0}", orgId);
            if (regionId != null && regionId > 0)
            {
                param += string.Format(@" and RegionId={0}", regionId);
            }
            query = string.Format(@"
           select r.RegionName,d.DivisionName,r.Status from tblRegionInfos r
inner join tblDivisionInfo d
on r.DivisionId=d.DivisionId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveRegionInfo(List<RegionSetupDTO> detailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<RegionSetup> RegionSetup = new List<RegionSetup>();

            foreach (var item in detailsDTO)
            {
                if (item.RegionId == 0)
                {
                    RegionSetup regionSetups = new RegionSetup()
                    {
                        RegionId = item.RegionId,
                        OrganizationId = orgId,
                        DivisionId= item.DivisionId,
                        RegionName = item.RegionName,
                        

                        EntryUserId = userId,
                        //UpdateUserId = userId,
                        EntryDate = DateTime.Now,
                        //UpdateDate = DateTime.Now,
                        Status= "Active"

                    };
                    _regionSetupRepository.Insert(regionSetups);
                }


            }

            IsSuccess = _regionSetupRepository.Save();
            return IsSuccess;
        }

        public RegionSetup GetRegionNamebyId(long regionId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
