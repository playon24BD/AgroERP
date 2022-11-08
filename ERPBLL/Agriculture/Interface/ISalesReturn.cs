using ERPBO.Agriculture.DomainModels;
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


        bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId, long orgid);



        IEnumerable<SalesReturnDTO> GetSalesReturns(long? ProductId, string name,string status);


        IEnumerable<SalesReturn> GetSalesSalesReturnByInfoIdNotAdjust(long? ProductSalesInfoId);

        bool updateadjustsales(List<SalesReturnDTO> salesReturnDTOs);

        // ReturnRawMaterial GetReturnsById(long ReturnRawMaterialId);

        SalesReturn GetSalesReturnsById(long SalesReturnId);


        IEnumerable<SalesReturn> GetSalesReturnsAdjustById(long id, long orgId);



    }
}
