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
    public class StockiestWiseYearlyTargetBusiness: IStockiestWiseYearlyTarget
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestWiseYearlyTargetRepository _stockiestWiseYearlyTargetRepository;

        public StockiestWiseYearlyTargetBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestWiseYearlyTargetRepository = new StockiestWiseYearlyTargetRepository(this._agricultureUnitOfWork);

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
                    ProductId=item.ProductId
                    
                };
                _stockiestWiseYearlyTargetRepository.Insert(yearlyTarget);
            }

            isSuccess = _stockiestWiseYearlyTargetRepository.Save();
            return isSuccess;
        }
    }
}
