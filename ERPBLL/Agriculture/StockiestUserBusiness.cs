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
    public class StockiestUserBusiness : IStockiestUserBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestUserBusinessRepository _stockiestUserBusinessRepository;

        public StockiestUserBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestUserBusinessRepository = new StockiestUserBusinessRepository(this._agricultureUnitOfWork);
        }
        public List<StockiestInfoViewModel> GetAllStockiest(long userId, long orgId)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<StockiestInfoViewModel>(string.Format("Select su.StockiestId,s.StockiestName from tblStockiestUser su Inner Join [dbo].[tblStockiestInfo] s on su.StockiestId = s.StockiestId Where su.UserId = {0} and su.OrganizationId= {1}", userId,orgId)).ToList();
        }

        public IEnumerable<StockiestUser> GetAllStockiestByUserIdAndStockiestId(long userId, long orgId)
        {
            throw new NotImplementedException();
        }

        public StockiestUser GetStockiestInfoById(long stockiestId, long orgId)
        {
            return _stockiestUserBusinessRepository.GetOneByOrg(a => a.StockiestId == stockiestId && a.OrganizationId == orgId);

          //  return _stockiestInfoRepository.GetOneByOrg(r => r.StockiestId == stockiestId && r.OrganizationId == orgId);
        }

        public bool SaveStockiestUser(List<string> stockiest, long userId, long suserId, long orgId,string action)
        {
            if (action == "Update")
            {
                _stockiestUserBusinessRepository.DeleteAll(s => s.UserId == userId && s.OrganizationId == orgId);
                _stockiestUserBusinessRepository.Save();
            }


            bool isSuccess = false;
            List<StockiestUser> stockiestUser = new List<StockiestUser>();
            foreach (var item in stockiest)
            {
                StockiestUser stock = new StockiestUser()
                {
                    StockiestId = Convert.ToInt64(item),
                    EntryDate = DateTime.Now,
                    EntryUserId = suserId,
                    UserId = userId,
                    OrganizationId = orgId,
                };
                stockiestUser.Add(stock);
            }
            if (stockiestUser.Count() > 0)
            {
                _stockiestUserBusinessRepository.InsertAll(stockiestUser);
                isSuccess = _stockiestUserBusinessRepository.Save();
            }
            return isSuccess;
        }

        public bool UpdateStockiestUser(List<string> stockiest, long userId, long suserId, long orgId)
        {
            throw new NotImplementedException();
        }
    }
}
