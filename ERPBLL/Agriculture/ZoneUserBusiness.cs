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
    public class ZoneUserBusiness : IZoneUserBusiness
    {

        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly ZoneUserBusinessRepository _zonuserBusinessRepository;
        public ZoneUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = agricultureUnitOfWork;
            this._zonuserBusinessRepository = new ZoneUserBusinessRepository(_AgricultureUnitOfWork);
        }
        public List<ZoneSetupViewModel> GetAllZone(long userId, long orgId)
        {

            return _AgricultureUnitOfWork.Db.Database.SqlQuery<ZoneSetupViewModel>(string.Format(@"Select zu.ZoneId,z.ZoneName 
From tblZoneUser zu 
Inner Join tblZoneInfos z on z.ZoneId = zu.ZoneId 
Where zu.UserId = {0} and zu.OrganizationId= {1}", userId, orgId)).ToList();
        }

        public IEnumerable<ZoneUser> GetAllZoneByUserIdAndZoneId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveZoneUser(List<string> zones, long userId, long suserId, long orgId,string action)
        {
            if (action=="Update")
            {
                _zonuserBusinessRepository.DeleteAll(s => s.UserId == userId && s.OrganizationId == orgId);
                _zonuserBusinessRepository.Save();
            }


            bool isSuccess = false;
            List<ZoneUser> zonesUser = new List<ZoneUser>();
            foreach (var item in zones)
            {
                ZoneUser zon = new ZoneUser()
                {
                    ZoneId = Convert.ToInt64(item),
                    EntryDate = DateTime.Now,
                    EntryUserId = suserId,
                    UserId = userId,
                    OrganizationId = orgId,
                };
                zonesUser.Add(zon);
            }
            if (zonesUser.Count() > 0)
            {
                _zonuserBusinessRepository.InsertAll(zonesUser);
                isSuccess = _zonuserBusinessRepository.Save();
            }
            return isSuccess;
        }

        public bool UpdateZone(List<string> zones, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
