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
    public class StockiestWiseYearlyTargetBusiness: IStockiestWiseYearlyTarget
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestWiseYearlyTargetRepository _stockiestWiseYearlyTargetRepository;

        public StockiestWiseYearlyTargetBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestWiseYearlyTargetRepository = new StockiestWiseYearlyTargetRepository(this._agricultureUnitOfWork);

        }

        public IEnumerable<StockiestWiseYearlyTargetDTO> GetYearlyTargetList(long? territoryId, long? stockiestId, long? productId, string fromDate, string toDate)
        {
            try
            {

                return this._agricultureUnitOfWork.Db.Database.SqlQuery<StockiestWiseYearlyTargetDTO>(QueryForGetYearlyTargetList(territoryId,stockiestId,productId,fromDate,toDate)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForGetYearlyTargetList(long? territoryId, long? stockiestId, long? productId, string fromDate, string toDate)
        {
            try
            {

                string query = string.Empty;
                string param = string.Empty;


                if (stockiestId != null && stockiestId > 0)
                {
                    param += string.Format(@" and s.StockiestId={0}", stockiestId);
                }
                if (territoryId != null && territoryId > 0)
                {
                    param += string.Format(@" and t.TerritoryId={0}", territoryId);
                }
                if (productId != null && productId > 0)
                {
                    param += string.Format(@" and f.FinishGoodProductId={0}", productId);
                }


                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(y.FromDate as date) = '{0}'",fDate);
                    

                }
                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(y.ToDate as date) = '{0}'", tDate);


                }

                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(y.FromDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(y.ToDate as date)='{0}'", tDate);
                }


                query = string.Format(@"


select y.StockiestWiseYearlyTargetId,t.TerritoryName,s.StockiestName,f.FinishGoodProductName,y.FromDate,y.ToDate,cast(y.TargetQty as decimal (10,2))TargetValue from tblStockiestWiseYearlyTarget y
left join tblTerritoryInfos t on t.TerritoryId=y.TerritoryId
left join tblStockiestInfo s on s.StockiestId=y.StockiestId
left join tblFinishGoodProductInfo f on f.FinishGoodProductId=y.FinishGoodProductId

where 1=1 {0} order by StockiestWiseYearlyTargetId desc", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }

        }



        public bool SaveStockiestWiseYearlyTargetList(List<StockiestWiseYearlyTargetDTO> detailsDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            List<StockiestWiseYearlyTarget> stockiestWiseYearlies = new List<StockiestWiseYearlyTarget>();

            foreach(var item in detailsDTO)
            {
                StockiestWiseYearlyTarget yearlyTarget = new StockiestWiseYearlyTarget()
                {
                    OrganizationId = orgId,
                    EntryDate=DateTime.Now,
                    EntryUserId=userId,
                    StockiestId=item.StockiestId,
                    Flag=item.Flag,
                    Month=item.Month,
                    Year=item.Year,
                    TargetQty=item.TargetQty,
                    TerritoryId=item.TerritoryId,
                    Day=item.Day,
                    FromDate=item.FromDate,
                    ToDate=item.ToDate,
                    FinishGoodProductId=item.FinishGoodProductId
                    
                };
                _stockiestWiseYearlyTargetRepository.Insert(yearlyTarget);
            }

            isSuccess = _stockiestWiseYearlyTargetRepository.Save();
            return isSuccess;
        }
    }
}
