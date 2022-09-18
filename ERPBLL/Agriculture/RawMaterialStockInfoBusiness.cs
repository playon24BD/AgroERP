using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialStockInfoBusiness:IRawMaterialStockInfo
    {
        private readonly RawMaterialStockInfoRepository _rawMaterialStockInfoRepository;
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;

        public RawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockInfoRepository = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }

        public bool SaveRawMaterialStock(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;
            if (info.RawMaterialStockId == 0)
            {
                RawMaterialStockInfo stockInfo = new RawMaterialStockInfo
                {
                    BatchCode= "BC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss"),

                    RawMaterialId =info.RawMaterialId,
                    OrganizationId=orgId,
                    Quantity=info.Quantity,
                    Unit=info.Unit,
                    EntryDate=DateTime.Now,
                    EntryUserId=userId
                };
                List<RawMaterialStockDetail> stockDetails = new List<RawMaterialStockDetail>();
                foreach (var item in details)
                {
                    RawMaterialStockDetail rawMaterial = new RawMaterialStockDetail()
                    {
                        
                        OrganizationId =item.OrganizationId,
                        RawMaterialId=item.RawMaterialId,
                        Quantity=item.Quantity,
                        Unit=item.Unit,
                        StockDate=DateTime.Now,
                        UpdateDate=item.UpdateDate,
                        UpdateUserId=item.UpdateUserId,
                        RawMaterialStockId=item.RawMaterialStockId,
                        Status=item.Status="Pending"
                        
                    };
                    stockDetails.Add(rawMaterial);

                }
                stockInfo.RawMaterialStockDetails = stockDetails;
                _rawMaterialStockInfoRepository.Insert(stockInfo);
                isSuccess = _rawMaterialStockInfoRepository.Save();
            }
            return isSuccess;
        }
    }
}
