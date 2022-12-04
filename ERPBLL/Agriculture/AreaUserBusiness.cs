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
    public class AreaUserBusiness : IAreaUserBusiness
    {

        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AreaUserBusinessRepository _areaUserBusinessRepository;
        public AreaUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._areaUserBusinessRepository = new AreaUserBusinessRepository(this._agricultureUnitOfWork);

        }
        public List<AreaSetupViewModel> GetAllArea(long userId, long orgId)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<AreaSetupViewModel>(string.Format("SELECT au.AreaId,a.AreaName FROM dbo.tblAreaUser au Inner join tblAreaSetup a On au.AreaId = a.AreaId Where au.UserId = {0} and au.OrganizationId= {1}", userId, orgId)).ToList();
        }

        public IEnumerable<AreaUser> GetAllAreaByUserIdAndAreaId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveAreasUser(List<string> areas, long userId, long suserId, long orgId,string action)
        {
            if (action == "Update")
            {
                _areaUserBusinessRepository.DeleteAll(s => s.UserId == userId && s.OrganizationId == orgId);
                _areaUserBusinessRepository.Save();
            }


            bool isSuccess = false;
            List<AreaUser> areasUser = new List<AreaUser>();
            foreach (var item in areas)
            {
                AreaUser area = new AreaUser()
                {
                    AreaId = Convert.ToInt64(item),
                    EntryDate = DateTime.Now,
                    EntryUserId = suserId,
                    UserId = userId,
                    OrganizationId = orgId,
                };
                areasUser.Add(area);
            }
            if (areasUser.Count() > 0)
            {
                _areaUserBusinessRepository.InsertAll(areasUser);
                isSuccess = _areaUserBusinessRepository.Save();
            }
            return isSuccess;
        }

        public bool UpdateAreaUser(List<string> areas, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
