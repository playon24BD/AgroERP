using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.ViewModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RegionUserBusiness : IRegionUserBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RegionUserBusinessRepository _regionUserBusinessRepository;
        public RegionUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._regionUserBusinessRepository = new RegionUserBusinessRepository(this._agricultureUnitOfWork);
        }
        public List<RegionSetupViewModel> GetAllRegion(long userId, long orgId)
        {
           return _agricultureUnitOfWork.Db.Database.SqlQuery<RegionSetupViewModel>(string.Format("SELECT ru.RegionId,r.RegionName FROM[dbo].[tblRegionUser] ru Inner join tblRegionInfos r on ru.RegionId = r.RegionId Where ru.UserId = {0} and ru.OrganizationId= {1}", userId, orgId)).ToList();
        }

        public IEnumerable<RegionUser> GetAllRegionByUserIdAndRegionId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRegionUser(List<string> regions, long userId, long suserId, long orgId,string action)
        {
            if (action == "Update")
            {
                _regionUserBusinessRepository.DeleteAll(s => s.UserId == userId && s.OrganizationId == orgId);
                _regionUserBusinessRepository.Save();
            }


            bool isSuccess = false;
            List<RegionUser> regionUser = new List<RegionUser>();
            foreach (var item in regions)
            {
                RegionUser region = new RegionUser()
                {
                    RegionId = Convert.ToInt64(item),
                    EntryDate = DateTime.Now,
                    EntryUserId = suserId,
                    UserId = userId,
                    OrganizationId = orgId,
                };
                regionUser.Add(region);
            }
            if (regionUser.Count() > 0)
            {
                _regionUserBusinessRepository.InsertAll(regionUser);
                isSuccess = _regionUserBusinessRepository.Save();
            }
            return isSuccess;
        }

        public bool UpdateRegion(List<string> regions, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
