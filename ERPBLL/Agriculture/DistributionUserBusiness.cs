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
    public class DistributionUserBusiness : IDistributionUserBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly DistributionUserBusinessRepository _distributionUserRepostitory;
        public DistributionUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._distributionUserRepostitory = new DistributionUserBusinessRepository(_agricultureUnitOfWork);

        }

        public bool SaveDistributionUser(List<DistributionUserDTO> distributionUsersDTO, long suserId, long orgId)
        {

            List<DistributionUser> distributionUsers = new List<DistributionUser>();
            foreach (var item in distributionUsersDTO)
            {
                DistributionUser ds = new DistributionUser()
                {
                    UserId = item.UserId,
                    ZoneId = item.ZoneId,
                    DivisionId = item.DivisionId,
                    RegionId = item.RegionId,
                    AreaId = item.AreaId,
                    TerritoryId = item.TerritoryId,
                    StockiestId = item.StockiestId,
                    DistributionType = item.DistributionType,
                    OrganizationId = orgId,
                    EntryDate = DateTime.Now,
                    EntryUserId =suserId,
                    Status=item.Status,
                    Flag=item.Flag,
                };
                distributionUsers.Add(ds);
            }
            _distributionUserRepostitory.InsertAll(distributionUsers);
            return _distributionUserRepostitory.Save();
        }
    }
}
