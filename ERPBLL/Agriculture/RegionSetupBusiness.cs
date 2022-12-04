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

        public IEnumerable<RegionSetup> GetAllRegionSetup(long OrgId)
        {
            return _regionSetupRepository.GetAll(x => x.OrganizationId == OrgId).ToList();
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




        public IEnumerable<RegionSetupDTO> GetRegionInfos(long orgId, string name, long? regionId, long? divisionId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RegionSetupDTO>(QueryForRegion(orgId, name, regionId, divisionId)).ToList();
        }


        private string QueryForRegion(long orgId, string name, long? regionId, long? divisionId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and r.OrganizationId={0}", orgId);
            if (regionId != null && regionId > 0)
            {
                param += string.Format(@" and r.RegionId={0}", regionId);
            }
            if (divisionId != null && divisionId > 0)
            {
                param += string.Format(@" and r.DivisionId={0}", divisionId);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and r.RegionName like '%{0}%'", name);
            }
            query = string.Format(@"
           select r.RegionName,d.DivisionName,r.Status,r.RegionId,r.DivisionId,r.OrganizationId from tblRegionInfos r
inner join tblDivisionInfo d
on r.DivisionId=d.DivisionId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<RegionSetupDTO> GetAllRegionDetails(long DivisionId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRegionInfoEdit(RegionSetupDTO dTO, long userId, long orgId)
        {
            bool IsSuccess = false;

            RegionSetup regionSetup = new RegionSetup();

            regionSetup = GetRegionNamebyId(dTO.RegionId, orgId);
            regionSetup.RegionName = dTO.RegionName;
            regionSetup.DivisionId = dTO.DivisionId;
            regionSetup.Status = dTO.Status;
            regionSetup.UpdateUserId = userId;
            regionSetup.UpdateDate = DateTime.Now;
            regionSetup.OrganizationId = regionSetup.OrganizationId;
            regionSetup.EntryDate = regionSetup.EntryDate;
            regionSetup.EntryUserId = regionSetup.EntryUserId;


            IsSuccess = _regionSetupRepository.Save();
           

            return IsSuccess;
        }

        public RegionSetup GetRegionNamebyId(long regionId, long orgId)
        {
            return _regionSetupRepository.GetOneByOrg(x => x.RegionId == regionId && x.OrganizationId == orgId);
        }
    }
}
