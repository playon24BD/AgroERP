using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class SalesPaymentRegisterBusiness : ISalesPaymentRegister
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly SalesPaymentRegisterRepository _salesPaymentRegisterRepository;
        private readonly AgroProductSalesInfoRepository _agroProductSalesInfoRepository;

        private readonly IAgroProductSalesInfoBusiness _agroProductSalesInfoBusiness;
        private readonly ICommissionOnProductOnSalesBusiness _commissionOnProductOnSalesBusiness;

        public SalesPaymentRegisterBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IAgroProductSalesInfoBusiness agroProductSalesInfoBusiness, ICommissionOnProductOnSalesBusiness commissionOnProductOnSalesBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._salesPaymentRegisterRepository = new SalesPaymentRegisterRepository(this._agricultureUnitOfWork);
            this._agroProductSalesInfoRepository = new AgroProductSalesInfoRepository(this._agricultureUnitOfWork);

            this._agroProductSalesInfoBusiness = agroProductSalesInfoBusiness;
            this._commissionOnProductOnSalesBusiness = commissionOnProductOnSalesBusiness;
        }

        public IEnumerable<SalesPaymentRegister> GetPaymentDetailsByInvoiceId(long infoId)
        {
            try
            {
                return _salesPaymentRegisterRepository.GetAll(i => i.ProductSalesInfoId == infoId).ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool SaveSalesPayment(SalesPaymentRegisterDTO info, long userId)
        {
            bool IsSuccess = false;

            if (info.PaymentRegisterID == 0)
            {
                SalesPaymentRegister model = new SalesPaymentRegister
                {

                    PaymentAmount = info.PaymentAmount,
                    PaymentDate = DateTime.Now,
                    Remarks = info.Remarks,
                    ProductSalesInfoId = info.ProductSalesInfoId,
                    EntryUserId = userId,
                    AccounrNumber = info.AccounrNumber,
                    PaymentMode = info.PaymentMode
                };
                _salesPaymentRegisterRepository.Insert(model);
                IsSuccess = _salesPaymentRegisterRepository.Save();
                //
                if (IsSuccess)
                {
                    var salesPayment = _agroProductSalesInfoBusiness.CheckBYProductSalesInfoId(info.ProductSalesInfoId);
                    salesPayment.PaidAmount += model.PaymentAmount;
                    salesPayment.DueAmount -= model.PaymentAmount;

                    _agroProductSalesInfoRepository.Update(salesPayment);
                    IsSuccess = _agroProductSalesInfoRepository.Save();
                    var dateDif = (DateTime.Now.Date - salesPayment.InvoiceDate.Value.Date).Days;
                    if (dateDif < 4 && salesPayment.DueAmount == 0)
                    {

                        _commissionOnProductOnSalesBusiness.UpdateCommissionOnProductOnSales(salesPayment, userId, salesPayment.OrganizationId);
                    }
                }
            }
            return IsSuccess;
        }


        public IEnumerable<DateWiseCollectionReport> GetDateWiseCollectionReport(long? zoneId, long? divisonId, long? regionId, long? areaId, long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                return _agricultureUnitOfWork.Db.Database.SqlQuery<DateWiseCollectionReport>(QueryForDateWiseCollectionReport(zoneId, divisonId, regionId, areaId, stockiestId, territoryId, invoiceNo, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string QueryForDateWiseCollectionReport(long? zoneId, long? divisonId, long? regionId, long? areaId, long? stockiestId, long? territoryId, string invoiceNo, string fromDate, string toDate)
        {
            try
            {
                string param = string.Empty;
                string query = string.Empty;

                if (stockiestId != null && stockiestId > 0)
                {
                    param += string.Format(@" and info.StockiestId={0}", stockiestId);
                }
                if (territoryId != null && territoryId > 0)
                {
                    param += string.Format(@" and info.TerritoryId={0}", territoryId);
                }
                if (areaId != null && areaId > 0)
                {
                    param += string.Format(@" and info.AreaId={0}", areaId);
                }
                if (regionId != null && regionId > 0)
                {
                    param += string.Format(@" and info.RegionId={0}", regionId);
                }

                if (divisonId != null && divisonId > 0)
                {
                    param += string.Format(@" and info.DivisionId={0}", divisonId);
                }
                if (zoneId != null && zoneId > 0)
                {
                    param += string.Format(@" and info.ZoneId={0}", zoneId);
                }





                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(ph.PaymentDate as date) between '{0}' and '{1}'", fDate, tDate);
                }
                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(ph.PaymentDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(ph.PaymentDate as date)='{0}'", tDate);
                }

                query = string.Format(@"SELECT  todate='" + fromDate + "', fromDate='" + toDate + "', z.ZoneName,d.DivisionName,r.RegionName,a.AreaName,  t.TerritoryName,s.StockiestName, info.InvoiceNo,info.TotalAmount,ph.PaymentAmount,ph.PaymentDate,ph.PaymentMode,Case When ph.AccounrNumber IS Null Then 'N/A' Else ph.AccounrNumber End As AccountNumber from tblProductSalesPaymentHistory ph Inner Join tblProductSalesInfo info on ph.ProductSalesInfoId=info.ProductSalesInfoId inner join tblZoneInfos z on info.ZoneId=z.ZoneId inner join tblDivisionInfo d on info.DivisionId=d.DivisionId inner join tblRegionInfos r on info.RegionId=r.RegionId inner join tblAreaSetup a on info.AreaId=a.AreaId inner join tblTerritoryInfos t on info.TerritoryId=t.TerritoryId inner join tblStockiestInfo s on info.StockiestId=s.StockiestId Where ph.PaymentAmount>0  and ph.PaymentMode IS NOT NULL {0}", Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
