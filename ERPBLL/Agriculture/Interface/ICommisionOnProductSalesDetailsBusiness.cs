using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ICommisionOnProductSalesDetailsBusiness
    {
       IEnumerable<CommisionOnProductSalesDetails> GetCommisionOnProductSalesDetails(long orgId);
       CommisionOnProductSalesDetails GetCommisionOnProductSalesDetailsbyId(long commisionOnProductSalesDetailsId,long orgId);
       bool SaveCommisionOnProductSalesDetails(List<AgroProductSalesDetails> onProductSalesDetailsDTO,long id,string flag,long userId,long orgId);
    }
}
