﻿using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
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
        IEnumerable<InvoiceWiseSalesReport> GetInvoiceWiseSalesReport( string fromDate,string toDate);
        IEnumerable<ProductSalesDataReport> GetProductSalesData(string InvoiceNo);

        IEnumerable<ProductSalesDataChallanReport> GetProductSalesChallanData(string ChallanNo);
        IEnumerable<AgroProductSalesInfoDTO> GetAgroSalesInfos(long orgId, long? ProductId);
        IEnumerable<AgroProductSalesInfo> GetUserName(long orgId);
        AgroProductSalesInfo GetAgroProductionInfoById(long id, long orgId);
        IEnumerable<AgroProductSalesInfo> GetAgroProductionSalesInfo(long orgId);
        AgroProductSalesInfo CheckBYProductSalesInfoId(long? ProductSalesInfoId);
        bool SaveAgroProductSalesInfo(AgroProductSalesInfoDTO agroSalesInfoDTO, List<AgroProductSalesDetailsDTO> details, long userId, long orgId);



        IEnumerable<AgroProductSalesInfo> GetAllDueSalesInvoice();


        AgroProductSalesInfo GetInvoiceProductionInfoById(long ProductSalesInfoId);

        AgroProductSalesInfo GetChallanProductionInfoById(long ProductSalesInfoId);

    }
}
