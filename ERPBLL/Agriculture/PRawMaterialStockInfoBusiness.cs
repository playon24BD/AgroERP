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
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;


        //contractor
        public PRawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._pRawMaterialStockInfoRepository = new PRawMaterialStockInfoRepository(this._agricultureUnitOfWork);
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
        }



        public IEnumerable<PRawMaterialStockInfoDTO> GetAllPRawMaterialStockInfo(long OrgId, string name, string ChallanNo, string PONumber, long? supplierId, long? rsupid)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PRawMaterialStockInfoDTO>(QueryForRMPurchaseMinfo(OrgId, name,ChallanNo,PONumber,supplierId, rsupid)).ToList();
        }

        private string QueryForRMPurchaseMinfo(long OrgId, string name, string ChallanNo, string PONumber, long? supplierId, long? rsupid)
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
            if (ChallanNo != null && ChallanNo != "")
            {
                param += string.Format(@" and i.ChalanNo like '%{0}%'", ChallanNo);
            }
            if (PONumber != null && PONumber != "")
            {
                param += string.Format(@" and i.BatchCode like '%{0}%'", PONumber);
            }
            if (supplierId != null && supplierId >0)
            {
                param += string.Format(@" and i.RawMaterialSupplierId= {0}", supplierId);
            }

            query = string.Format(@"
           select i.PRawMaterialStockId, s.RawMaterialSupplierName,i.RawMaterialSupplierId,i.BatchCode,i.ChalanNo,i.ChalanDate,i.InvoiceNo,i.InvoiceDate,i.TotalAmount from tblPRawMaterialStockInfo i
inner join tblRawMaterialSupplierInfo s
on i.RawMaterialSupplierId=s.RawMaterialSupplierId
            where 1=1  {0} order by i.PRawMaterialStockId Desc",
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
                List<RawMaterialTrack> modeltrk = new List<RawMaterialTrack>();//RawMaterialTrackInfo table update

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
                        ExpireDate = item.ExpireDate
                        

                    };
                    modelDetails.Add(PRawMaterialStockIDetails);


                    //RawMaterialTrackInfo table update
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack()
                    {
                        RawMaterialId = item.RawMaterialId,
                        Quantity = item.Quantity,
                        IssueDate = DateTime.Now,
                        EntryDate = DateTime.Now,
                        IssueStatus = "StockIn",
                        EntryUserId = userId
                    };
                    modeltrk.Add(rawMaterialTrack);
                    
                }
                
                model.PRawMaterialStockIDetails = modelDetails;

                _pRawMaterialStockInfoRepository.Insert(model);
                IsSuccess = _pRawMaterialStockInfoRepository.Save();

                _rawMaterialTrackInfoRepository.InsertAll(modeltrk);
                IsSuccess = _rawMaterialTrackInfoRepository.Save();

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
