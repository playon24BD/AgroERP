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

    public class PRawMaterialStockInfoBusiness : IPRawMaterialStockInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PRawMaterialStockInfoRepository  _pRawMaterialStockInfoRepository;


        //contractor
        public PRawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._pRawMaterialStockInfoRepository = new PRawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }


        public IEnumerable<PRawMaterialStockInfo> GetAllPRawMaterialStockInfo(long OrgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRawMaterialPurchaseStock(PRawMaterialStockInfoDTO info, List<PRawMaterialStockIDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
            var Pinumber = "BPI-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            if (info.PRawMaterialStockId == 0)
            {
                PRawMaterialStockInfo model = new PRawMaterialStockInfo
                {

                    BatchCode = Pinumber,
                    ChalanNo = info.ChalanNo,
                    ChalanDate = DateTime.Now,
                    InvoiceNo= info.InvoiceNo,
                    InvoiceDate = info.InvoiceDate,
                    RawMaterialSupplierId = info.RawMaterialSupplierId,
                    IssueStatus = "Active",

                    OrganizationId = orgId,
                    EntryUserId= userId,
                    EntryDate = DateTime.Now,
                    TotalAmount = info.TotalAmount

                };
                List<PRawMaterialStockIDetails> modelDetails = new List<PRawMaterialStockIDetails>();

                foreach (var item in details)
                {
                    PRawMaterialStockIDetails PRawMaterialStockIDetails = new PRawMaterialStockIDetails()
                    {
                        //RawMaterialId = item.RawMaterialId,
                        //ReceipeBatchCode = ReceipeBatchCodes,
                        //FGRRawMaterQty = item.FGRRawMaterQty,
                        //UnitId = item.UnitId,
                        //OrganizationId = orgId,
                        //EUserId = userId,
                        //EntryDate = DateTime.Now
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        UnitID = item.UnitID,
                        UnitPrice = item.UnitPrice,
                        SubTotal = item.SubTotal,
                        StockDate = DateTime.Now,
                        StockIssueDate = DateTime.Now,
                        Status = "StockIn",
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        ExpireDate = DateTime.Now
                        

                    };
                    modelDetails.Add(PRawMaterialStockIDetails);
                }
                model.PRawMaterialStockIDetails = modelDetails;

                _pRawMaterialStockInfoRepository.Insert(model);
                IsSuccess = _pRawMaterialStockInfoRepository.Save();
            }
            //else
            //{

            //    IsSuccess = _fDetail.updateFinishGoodRecipDetails(info, details, userId, orgId);

            //}
            return IsSuccess;
        }
    }
}
