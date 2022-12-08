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
    public interface ISalesPaymentRegister
    {
        bool SaveSalesPayment(SalesPaymentRegisterDTO info,long userId);

        IEnumerable<SalesPaymentRegister> GetPaymentDetailsByInvoiceId(long infoId);
        IEnumerable<DateWiseCollectionReport> GetDateWiseCollectionReport(long? zoneId, long? divisonId, long? regionId, long? areaId, long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate);
        IEnumerable<SalesPaymentRegisterDTO> GetSalesPaymentRegisterList(long? zoneId, long? divisonId, long? regionId, long? areaId, long? stockiestId, long? territoryId, string fromDate, string toDate);
    }
}
