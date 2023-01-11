using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPBO.Common;
using ERPBO.ControlPanel.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture.Interface
{
    public interface IAgroProductSalesInfoBusiness
    {
        IEnumerable<AgroProductSalesInfoDTO> GetDailySalesReportList(string invoiceNo, long? territoryId, long? stockiestId, string fromDate, string toDate);
        IEnumerable<DailySalesDataReport> GetDailySalesReport(string invoiceNo, long? territoryId, long? stockiestId, string fromDate, string toDate);
        AgroProductSalesInfo GetSalesById(long ProductSalesInfoId, long orgId);
        
        bool UpdateProductSalesEdit(AgroProductSalesInfoDTO infoDTO, List<AgroProductSalesDetailsDTO> detailsDTO, long userId, long orgId);
        
        IEnumerable<AgroProductSalesInfoDTO> GetSalesDropList(string invoiceNo);
        IEnumerable<AgroProductSalesInfoDTO> GetSalesDropApprovedList();


        IEnumerable<AgroProductSalesInfoDTO> GetProductWiseReportList(long? productId, string fromDate, string toDate);
        
        IEnumerable<AgroProductSalesInfoDTO> GetInvoiceReportList(long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate);
        
        IEnumerable<SalesReturnDTO> GetSalesAdjustInfos(string invoiceNo, string fromDate, string toDate);

        IEnumerable<AgroProductSalesInfoDTO> GetPaymentListInfos(string name,string fromDate, string toDate);
        
        IEnumerable<InvoiceWiseCollectionSalesReport> GetInvoiceWiseSalesReport(long? stockiestId, long? territoryId, string invoiceNo, string fromDate,string toDate);
        IEnumerable<ProductSalesDataReport> GetProductSalesData(long? SalesInfoId);
        IEnumerable<ProductSalesDataReport1> GetProductSalesData1(long? SalesInfoId);
        IEnumerable<ProductSalesDataReport2> GetProductSalesData2(long? SalesInfoId);
        IEnumerable<ProductSalesDataReport> ProductSalesInvoiceDrop(long? SalesInfoId);
        IEnumerable<ProductSalesDataReport1> ProductSalesInvoiceDrop1(long? SalesInfoId);
        IEnumerable<ProductSalesDataReport2> ProductSalesInvoiceDrop2(long? SalesInfoId);
        
        IEnumerable<ProductSalesDataChallanReport> GetProductSalesChallanData(long? SalesId);
        IEnumerable<ProductSalesDataChallanReport1> GetProductSalesChallanData1(long? SalesId);
        IEnumerable<ProductSalesDataChallanReport2> GetProductSalesChallanData2(long? SalesId);
        IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long? stockiestId, string invoiceNo, string fromDate, string toDate);
        IEnumerable<AgroProductSalesInfo> GetUserName(long orgId);
        AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId);
        IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId);
        IEnumerable<AgroProductSalesInfoDTO> GetLastInvoice(long orgId);
        AgroProductSalesInfo CheckBYProductSalesInfoId(long? ProductSalesInfoId);
        bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId);
        //ExecutionStateWithText SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId);



        IEnumerable<AgroProductSalesInfo> GetAllDueSalesInvoice();


        AgroProductSalesInfo GetInvoiceProductionInfoById(long ProductSalesInfoId);
        AgroProductSalesInfo GetInvoiceProductionInfoByIdNew(string invoiceNo);

        AgroProductSalesInfo GetChallanProductionInfoById(long ProductSalesInfoId);

        IEnumerable<ProductWiseSalesStementReport> GetProductwisesalesReportDownloadRpt(long? productId, string fromDate, string toDate);

        IEnumerable<AgroProductSalesInfo> GetAgroSalesinfoByStokiestId(long StockiestId);

        IEnumerable<AgroProductSalesInfoDTO> GetAllINVBYSTOKIESTID(long StockiestId);
        bool UpdateInvoiceDrop(long productSalesInfoId, long userId);


        IEnumerable<AgroProductSalesInfoDTO> GetDealerLadserInfos(long id, string fromDate, string toDate);

        IEnumerable<AgroProductSalesInfoDTO> GetPaymentLadserInfos(long? StockiestId, string invoiceNo);
    }
}
