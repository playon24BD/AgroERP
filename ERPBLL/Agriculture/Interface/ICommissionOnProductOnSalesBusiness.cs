using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;

using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ICommissionOnProductOnSalesBusiness
    {
        IEnumerable<CommissionOnProductOnSales> GetCommissionOnProductOnSales(long orgId);
        CommissionOnProductOnSales GetCommissionOnProductById(long commissionOnProductSalesId,long orgId );
        bool SaveCommissionOnProductOnSales(AgroProductSalesInfo agroProductSalesInfo , long userId,long orgId);
        IEnumerable<CommissionOnProductOnSalesDTO> GetAllCommissionOnProductOnSales(string invoice, string fdate, string tdate, long orgId);
    }
}
