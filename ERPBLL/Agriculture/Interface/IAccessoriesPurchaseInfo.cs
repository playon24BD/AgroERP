using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAccessoriesPurchaseInfo
    {
        IEnumerable<AccessoriesPurchaseInfoDTO> GetAccessoriesStockList(string invoiceNo);
        bool SaveAccessoriesPurchaseStock(AccessoriesPurchaseInfoDTO info, List<AccessoriesPurchaseDetailsDTO> details, long userId);
    }
}
