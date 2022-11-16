using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAgroProductSalesDetailsBusiness
    {
        IEnumerable<AgroProductSalesDetailsDTO> GetAllAgroSalesDetailsInfos( long orgId);
        IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByInfoId(long infoId, long orgId);
        IEnumerable<AgroProductSalesDetailsDTO> GetAgroSalesDetailsByInfoIdGet(long infoId, long orgId);
        AgroProductSalesDetails AgroProductSalesDetailsbyInfoId(long productSalesinfoId);
        AgroProductSalesDetails AgroProductSalesDetailsbyId(long id);




    }
}
