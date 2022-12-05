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
    public class TerritoryUserBusiness : ITerritoryUserBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly TerritoryUserBusinessRepository _territoryUserBusinessRepository;
        public TerritoryUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._territoryUserBusinessRepository = new TerritoryUserBusinessRepository(this._agricultureUnitOfWork);

        }
        public List<TerritorySetupViewModel> GetAllTerritory(long userId, long orgId)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<TerritorySetupViewModel>(string.Format("Select tu.TerritoryId,t.TerritoryName from [dbo].[tblTerritoryUser] tu Inner join tblTerritoryInfos t on tu.TerritoryId = t.TerritoryId Where tu.UserId = {0} and tu.OrganizationId= {1}", userId, orgId)).ToList();
        }

        public IEnumerable<TerritoryUser> GetAllTerritoryByUserIdAndTerritory(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public TerritoryUser GetTerritoryUserById(long TerritoryId)
        {
            return _territoryUserBusinessRepository.GetOneByOrg(t=>t.TerritoryId == TerritoryId);
        }

        public bool SaveTerritoryUser(List<string> territories, long userId, long suserId, long orgId,string action)
        {

            if (action == "Update")
            {
                _territoryUserBusinessRepository.DeleteAll(s => s.UserId == userId && s.OrganizationId == orgId);
                _territoryUserBusinessRepository.Save();
            }


            bool isSuccess = false;
            List<TerritoryUser> territoryUser = new List<TerritoryUser>();
            foreach (var item in territories)
            {
                TerritoryUser territory = new TerritoryUser()
                {
                    TerritoryId = Convert.ToInt64(item),
                    EntryDate = DateTime.Now,
                    EntryUserId = suserId,
                    UserId = userId,
                    OrganizationId = orgId,
                };
                territoryUser.Add(territory);
            }
            if (territoryUser.Count() > 0)
            {
                _territoryUserBusinessRepository.InsertAll(territoryUser);
                isSuccess = _territoryUserBusinessRepository.Save();
            }
            return isSuccess;
        }

        public bool UpdateTerritoryUser(List<string> territories, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
