using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ISalesReturn
    {

        bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId);


        IEnumerable<SalesReturnDTO> GetSalesReturns(long? ProductId, string name);

    }
}
