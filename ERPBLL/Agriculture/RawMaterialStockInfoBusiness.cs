using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialStockInfoBusiness : IRawMaterialStockInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialStockInfoRepository _rawMaterialStockInfoRepository;

        private readonly IRawMaterialStockDetail _rawMaterialStockDetail;


        public RawMaterialStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialStockDetail rawMaterialStockDetail)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialStockInfoRepository = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
            this._rawMaterialStockDetail = rawMaterialStockDetail;
        }
        public RawMaterialStockInfo GetRawMaterialIssueStockUnitById(long id, long orgId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == id && i.OrganizationId == orgId);
        }
        public IEnumerable<RawMaterialStockInfo> GetRawMaterialStockDetails(long orgId)
        {
            return _rawMaterialStockInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }

        public IEnumerable<RawMaterialStockInfoDTO> GetRawMaterialStockInfos(long orgId, long? rawMaterialId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialStockInfoDTO>(QueryForRawMaterialInfoss(orgId, rawMaterialId)).ToList();
        }
        private string QueryForRawMaterialInfoss(long orgId, long? rawMaterialId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and rms.OrganizationId={0}", orgId);
            if (rawMaterialId != null && rawMaterialId > 0)
            {
                param += string.Format(@" and rms.RawMaterialId={0}", rawMaterialId);
            }
            query = string.Format(@"select rms.RawMaterialStockId,
            rms.RawMaterialId,
            rm.RawMaterialName,
            rms.Quantity,
            rms.Unit,
            --rms.ExpireDate,
            (select CAST(rms.ExpireDate as date))'ExpireDate',
            rms.OrganizationId
            from [Agriculture].dbo.tblRawMaterialStockInfo rms
            inner join [Agriculture].dbo.tblRawMaterialInfo rm on   rms.RawMaterialId=rm.RawMaterialId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }
        public bool UpdateRawmaterialstockInfo(long id, double UpdateRawMaterialStock, double IssueRawMaterialStockQty, long orgId, string Unit, DateTime? EntryDate, long? EntryUserId)
        {
            bool IsSuccess = false;

            var rawmeterialinfoupdateqty = GetRawMaterialStockById(id, orgId);
            if (rawmeterialinfoupdateqty != null)
            {
                rawmeterialinfoupdateqty.Quantity = UpdateRawMaterialStock;
                rawmeterialinfoupdateqty.UpdateDate = DateTime.Now;
                rawmeterialinfoupdateqty.UpdateUserId = EntryUserId;
                _rawMaterialStockInfoRepository.Update(rawmeterialinfoupdateqty);

            }
           
            IsSuccess = _rawMaterialStockInfoRepository.Save();
            if (IsSuccess)
            {

                var rawMaterialStockDetailsUpdate = _rawMaterialStockDetail.updateRawmaterialstockdetails(id, UpdateRawMaterialStock, IssueRawMaterialStockQty, orgId, Unit, EntryDate, EntryUserId);

            }
            return IsSuccess;
        }





        public bool SaveRawMaterialStock(RawMaterialStockInfoDTO info, List<RawMaterialStockDetailDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;

            if (info.RawMaterialStockId == 0)
            {
                List<RawMaterialStockInfo> stockInfoAll = new List<RawMaterialStockInfo>();
                List<RawMaterialStockDetail> stockDetails = new List<RawMaterialStockDetail>();

                var BatchCodes = "BC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

                foreach (var item in details)
                {
                    var RawmaterialvalueCheck = RawMaterialStockInfoCheckValues(item.RawMaterialId);
                    if (RawmaterialvalueCheck != null)
                    {
                        RawMaterialStockDetail rawMaterial = new RawMaterialStockDetail()
                        {

                            OrganizationId = orgId,
                            RawMaterialId = item.RawMaterialId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            StockDate = DateTime.Now,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            RawMaterialStockId = item.RawMaterialStockId,
                            RawMaterialSupplierId=item.RawMaterialSupplierId,
                            Status = item.Status = "StockIn",
                            ExpireDate=item.ExpireDate,

                        };
                        stockDetails.Add(rawMaterial);
                        var RawMaterialStock = GetRawMaterialStockInfoQuantity(orgId, item.RawMaterialId);

                        RawMaterialStock.Quantity += item.Quantity;
                        _rawMaterialStockInfoRepository.Update(RawMaterialStock);

                        

                    }
                    else
                    {
                        RawMaterialStockInfo stockInfo = new RawMaterialStockInfo
                        {
                            BatchCode = BatchCodes,

                            RawMaterialId = item.RawMaterialId,
                            OrganizationId = orgId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            ExpireDate=item.ExpireDate,
                           
                            //IssueStatus=info.IssueStatus="Pending"
                        };
                        stockInfoAll.Add(stockInfo);
                        RawMaterialStockDetail rawMaterial = new RawMaterialStockDetail()
                        {

                            OrganizationId = orgId,
                            RawMaterialId = item.RawMaterialId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            StockDate = DateTime.Now,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            RawMaterialStockId = item.RawMaterialStockId,
                            RawMaterialSupplierId=item.RawMaterialSupplierId,
                            Status = item.Status = "StockIn",
                            ExpireDate = item.ExpireDate,

                        };
                        stockDetails.Add(rawMaterial);
                    }



                }
                //info.RawMaterialStockDetails = stockDetails;
                _rawMaterialStockInfoRepository.InsertAll(stockInfoAll);
                if (_rawMaterialStockInfoRepository.Save())
                {

                    foreach (var items in stockDetails)
                    {

                        var RawMaterialStockInfoid = RawMaterialStockInfoIdGet(items.OrganizationId, items.RawMaterialId);

                        isSuccess = _rawMaterialStockDetail.SaverawMaterialStockDetail(items.OrganizationId, items.RawMaterialId,items.RawMaterialSupplierId, items.Quantity, items.Unit, items.StockDate, items.EntryDate, items.EntryUserId, items.UpdateDate,items.ExpireDate,items.UpdateUserId, items.Status, RawMaterialStockInfoid.RawMaterialStockId);
                    }



                }
                // isSuccess = _rawMaterialStockInfoRepository.Save();
            }
            else
            {
                isSuccess = _rawMaterialStockDetail.updateRawMaterialStockDetails(info,details, userId, orgId);
            }

            return isSuccess;
        }

        public List<AgroDropdown> GetDepotRawMaterials(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroDropdown>(string.Format(@"Select RM.RawMaterialName 'text',
RMI.RawMaterialId 'value'
From Agriculture.dbo.tblRawMaterialStockInfo RMI
Inner Join [Agriculture].dbo.tblRawMaterialInfo RM on RMI.RawMaterialId =RM.RawMaterialId 
Where RMI.OrganizationId={0}", orgId)).ToList();
        }
        public RawMaterialStockInfo RawMaterialStockInfoCheckValues(long? RawMaterialId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == RawMaterialId);
        }
        public RawMaterialStockInfo GetRawMaterialStockById(long id, long orgId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(i => i.RawMaterialStockId == id && i.OrganizationId == orgId);
        }
        public bool DeleteRawMaterialStock(long id, long userId, long orgId)
        {
            _rawMaterialStockInfoRepository.DeleteAll(i => i.RawMaterialStockId == id && i.OrganizationId == orgId);
            return _rawMaterialStockInfoRepository.Save();
        }
        public RawMaterialStockInfo RawMaterialStockInfoIdGet(long orgId, long? RawMaterialId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(i => i.OrganizationId == orgId && i.RawMaterialId == RawMaterialId);
        }
        public List<RawMaterialStockInfoDTO> RawMaterialStockInfoIdGet(long orgId, string BatchCode)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialStockInfoDTO>(QueryForRawMaterialInfoGetId(orgId, BatchCode)).ToList();
        }
        private string QueryForRawMaterialInfoGetId(long orgId, string batchCode)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and rms.OrganizationId={0}", orgId);
            if (batchCode != null && batchCode != "")
            {
                param += string.Format(@" and rms.BatchCode='{0}'", batchCode);
            }
            query = string.Format(@"select rms.RawMaterialStockId,
            rms.RawMaterialId,
            rms.Quantity,
            rms.Unit,
            rms.OrganizationId
            from [Agriculture].dbo.tblRawMaterialStockInfo rms
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }
        public RawMaterialStockInfo GetRawMaterialStockInfoQuantity(long orgId, long RawMaterialId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.RawMaterialId == RawMaterialId);

        }
        public RawMaterialStockInfo GetCheckRawmeterislQuantity(long RawMaterialId, long orgId)
        {
            return _rawMaterialStockInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.RawMaterialId == RawMaterialId);

        }
        public IEnumerable<RawMaterialStockInfoDTO> GetCheckExpairDatewiseRawMaterials(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialStockInfoDTO>(QueryForCheckExpairDatewiseRawMaterials(orgId)).ToList();
        }
        private string QueryForCheckExpairDatewiseRawMaterials(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and rmsi.OrganizationId={0}", orgId);
            
            query = string.Format(@"SELECT rmsi.RawMaterialId,rmi.RawMaterialName               
FROM [Agriculture].dbo.tblRawMaterialStockInfo rmsi
INNER JOIN [Agriculture].dbo.tblRawMaterialInfo rmi on rmsi.RawMaterialId=rmi.RawMaterialId Where 1=1 and rmsi.ExpireDate>=Getdate() {0}", Utility.ParamChecker(param));

            return query;
        }
    }
}
