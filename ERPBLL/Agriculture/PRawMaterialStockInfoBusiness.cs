using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
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



        public IEnumerable<PRawMaterialStockInfoDTO> GetAllPRawMaterialStockInfo(long OrgId, string name, long? rsupid)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PRawMaterialStockInfoDTO>(QueryForRMPurchaseMinfo(OrgId, name, rsupid)).ToList();
        }

        private string QueryForRMPurchaseMinfo(long OrgId, string name, long? rsupid)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (rsupid != null && rsupid > 0)
            {
                param += string.Format(@" and i.RawMaterialSupplierId={0}", rsupid);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and i.InvoiceNo like '%{0}%'", name);
            }
            query = string.Format(@"
           select i.PRawMaterialStockId, s.RawMaterialSupplierName,i.RawMaterialSupplierId,i.BatchCode,i.ChalanNo,i.ChalanDate,i.InvoiceNo,i.InvoiceDate,i.TotalAmount from tblPRawMaterialStockInfo i
inner join tblRawMaterialSupplierInfo s
on i.RawMaterialSupplierId=s.RawMaterialSupplierId
            where 1=1  {0}",
Utility.ParamChecker(param));
            return query;
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

        public PRawMaterialStockInfo GetRawmaterialPuschaseInfoOneById(long id, long orgId)
        {
            
            return _pRawMaterialStockInfoRepository.GetOneByOrg(p => p.PRawMaterialStockId == id && p.OrganizationId == orgId);
        }
    }
}
