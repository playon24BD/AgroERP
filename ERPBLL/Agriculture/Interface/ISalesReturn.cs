using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface ISalesReturn
    {

        IEnumerable<SalesReturnDTO> GetInvoice(long orgId);

        IEnumerable<SalesReturnReportData> GetSalesReturnReportSave(string InvoiceNo,string returnDate);

        IEnumerable<SalesReturnReportData> GetSalesReturnReportData(long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate);

        IEnumerable<SalesReturnDTO> GetSalesReturnReportList( long? productId, long? stockiestId, string invoiceNo, string status, string fromDate, string toDate);
        

        bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId);


        bool SaveSalesReturn(List<SalesReturnDTO> detailsDTO, long userId, long orgid);



        IEnumerable<SalesReturnDTO> GetSalesReturns(long? ProductId, string name,string status);


        IEnumerable<SalesReturn> GetSalesSalesReturnByInfoIdNotAdjust(long? ProductSalesInfoId);

        bool updateadjustsales(List<SalesReturnDTO> salesReturnDTOs);

        // ReturnRawMaterial GetReturnsById(long ReturnRawMaterialId);

        SalesReturn GetSalesReturnsById(long SalesReturnId);

        IEnumerable<SalesReturn> GetAgroSalesreturnByStokiestId(long StockiestId , string status);
        IEnumerable<SalesReturn> AgroSalesreturnByStokiestId(long StockiestId);



        IEnumerable<SalesReturn> GetSalesReturnsAdjustById(long id, long orgId);



        IEnumerable<SalesReturn> GetAgroSalesreturnByFGRandproductId(long FGRId,long FinishGoodProductInfoId);


    }
}
