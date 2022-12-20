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
        AgroProductSalesDetails GetSalesDetailsById(long ProductSalesDetailsId, long orgId);
        IEnumerable<AgroProductSalesDetailsDTO> GetAllAgroSalesDetailsInfos( long orgId);
        IEnumerable<AgroProductSalesDetails> GetAgroSalesDetailsByInfoId(long infoId, long orgId);
        IEnumerable<AgroProductSalesDetailsDTO> GetAgroSalesDetailsByInfoIdGet(long infoId, long orgId);
        AgroProductSalesDetails AgroProductSalesDetailsbyInfoId(long productSalesinfoId);
        AgroProductSalesDetails AgroProductSalesDetailsbyId(long id);


        IEnumerable<AgroProductSalesDetailsDTO> GetSalesDetailsByInfoId(long infoId);

        IEnumerable<AgroProductSalesDetailsDTO> GetSalesEditByInfoId(long ProductSalesInfoId);


        IEnumerable<AgroProductSalesDetailsDTO> DealerGetSalesDetailsByInfoId(long infoId);


        IEnumerable<AgroProductSalesDetails> GetProductSalesbyPMRid(long MeasurementId, long FinishGoodProductId, long FGRId);
        IEnumerable<AgroProductSalesDetails> GetProductSalesbyPMRidDRP(long MeasurementId, long FinishGoodProductId, long FGRId);
    }
}
