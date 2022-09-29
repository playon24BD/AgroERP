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
    public class RawMaterialIssueStockInfoBusiness : IRawMaterialIssueStockInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialIssueStockInfoRepository _rawMaterialIssueStockInfoRepository;
        private readonly IRawMaterialIssueStockDetailsBusiness _rawMaterialIssueStockDetailsBusiness;
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        private readonly IRawMaterialStockDetail _rawMaterialStockDetail;

        public RawMaterialIssueStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialIssueStockDetailsBusiness rawMaterialIssueStockDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IRawMaterialStockDetail rawMaterialStockDetail)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialIssueStockInfoRepository = new RawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);

            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._rawMaterialStockDetail = rawMaterialStockDetail;


        }

        public IEnumerable<RawMaterialIssueStockInfoDTO> RawMaterialStockIssueMinQty(string RawMaterialIdList, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialIssueStockInfoDTO>(QueryForRawMaterialIssueMinQty(RawMaterialIdList, orgId)).ToList();
        }

        private string QueryForRawMaterialIssueMinQty(string RawMaterialIdList, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and OrganizationId={0}", orgId);
            if (RawMaterialIdList != null && RawMaterialIdList != "")
            {
                param += string.Format(@" And RawMaterialId in ({0})", RawMaterialIdList);
            }
            query = string.Format(@" SELECT MIN(Quantity) Quantity from tblRawMaterialIssueStockInfo
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<RawMaterialIssueStockInfoDTO> GetRawMaterialIssueStockInfos(long orgId, long? rawMaterialId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<RawMaterialIssueStockInfoDTO>(QueryForRawMaterialIssueInfoss(orgId, rawMaterialId)).ToList();
        }

        private string QueryForRawMaterialIssueInfoss(long orgId, long? rawMaterialId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and rms.OrganizationId={0}", orgId);
            if (rawMaterialId != null && rawMaterialId > 0)
            {
                param += string.Format(@" and rms.RawMaterialId={0}", rawMaterialId);
            }
            query = string.Format(@"select rms.RawMaterialIssueStockId,
            rms.RawMaterialId,
            rm.RawMaterialName,
            rms.Quantity,
            rms.Unit,
            rms.OrganizationId
            from [Agriculture].dbo.tblRawMaterialIssueStockInfo rms
            inner join [Agriculture].dbo.tblRawMaterialInfo rm on   rms.RawMaterialId=rm.RawMaterialId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

            public RawMaterialIssueStockInfo RawMaterialStockInfoCheckValues(long? RawMaterialId, long orgId)
        {
            return _rawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == RawMaterialId && i.OrganizationId==orgId);
        }
        public RawMaterialIssueStockInfo RawMaterialStockIssueInfobyRawMaterialid(long rawMaterialId, long orgId)
        {
            return _rawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == rawMaterialId && i.OrganizationId == orgId);
        }


        public bool SaveProductIssueRawMaterialStock(RawMaterialIssueStockInfoDTO info, List<RawMaterialIssueStockDetailsDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;
          
            List<RawMaterialIssueStockInfo> IssuestockInfoAll = new List<RawMaterialIssueStockInfo>();
            List<RawMaterialIssueStockDetails> IssuestockDetails = new List<RawMaterialIssueStockDetails>();

            var BatchCodes = "PBC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

            if (info.RawMaterialIssueStockId == 0)
            {

                foreach (var item in details)
                {
                    var ProductIssueRawmaterialvalueCheck = RawMaterialStockInfoCheckValues(item.RawMaterialId,orgId);
                    if (ProductIssueRawmaterialvalueCheck != null)
                    {
                        RawMaterialIssueStockDetails IssuerawMaterials = new RawMaterialIssueStockDetails()
                        {
                            OrganizationId = orgId,
                            RawMaterialId = item.RawMaterialId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            IssueDate = DateTime.Now,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            RawMaterialIssueStockId = item.RawMaterialIssueStockId,
                            Status =  "StockIn"

                        };
                        IssuestockDetails.Add(IssuerawMaterials);
                        var IssueRawMaterialStock = RawMaterialStockInfoCheckValues(item.RawMaterialId,orgId);

                        IssueRawMaterialStock.Quantity += item.Quantity;
                        _rawMaterialIssueStockInfoRepository.Update(IssueRawMaterialStock);

                    }
                    else
                    {
                        RawMaterialIssueStockInfo IssuestockInfo = new RawMaterialIssueStockInfo
                        {
                            ProductBatchCode = BatchCodes,
                            OrganizationId = orgId,
                            FinishGoodProductId = 0,
                            RawMaterialId = item.RawMaterialId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId
                        };
                        IssuestockInfoAll.Add(IssuestockInfo);
                        RawMaterialIssueStockDetails rawMaterialIssue = new RawMaterialIssueStockDetails()
                        {

                            OrganizationId = orgId,
                            RawMaterialId = item.RawMaterialId,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            IssueDate = DateTime.Now,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            RawMaterialIssueStockId = item.RawMaterialIssueStockId,
                            Status = "StockIn"

                        };
                        IssuestockDetails.Add(rawMaterialIssue);
                    }



                }
                //info.RawMaterialStockDetails = stockDetails;
                _rawMaterialIssueStockInfoRepository.InsertAll(IssuestockInfoAll);
                if (_rawMaterialIssueStockInfoRepository.Save())
                {
                    
                    foreach (var items in IssuestockDetails)
                    {
                        var IssueRawMaterialStockInfoid = RawMaterialStockInfoCheckValues(items.RawMaterialId, items.OrganizationId);

                        isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveIssuerawMaterialStockDetail(items.OrganizationId, items.RawMaterialId, items.Quantity, items.Unit, items.IssueDate, items.EntryDate, items.EntryUserId, items.UpdateDate, items.UpdateUserId, items.Status, IssueRawMaterialStockInfoid.RawMaterialIssueStockId);

                        if (isSuccess)
                        {
                            var RawmaterialvalueCheck = _rawMaterialStockInfo.GetCheckRawmeterislQuantity(items.RawMaterialId, orgId);
                            var RawMaterialStockQty = RawmaterialvalueCheck.Quantity;
                            double IssueRawMaterialStockQty = items.Quantity;
                            double UpdateRawMaterialStock = RawMaterialStockQty - IssueRawMaterialStockQty;
                            var rawMaterialStockInfoUpdate = _rawMaterialStockInfo.UpdateRawmaterialstockInfo(RawmaterialvalueCheck.RawMaterialStockId, UpdateRawMaterialStock, IssueRawMaterialStockQty, orgId, items.Unit, items.EntryDate, items.EntryUserId);
                        }
                    }

                    

                }
                // isSuccess = _rawMaterialStockInfoRepository.Save();
            }
            return isSuccess;
        }

        public RawMaterialIssueStockInfo GetRawMaterialIssueStockById(long id, long orgId)
        {
            return _rawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialIssueStockId == id && i.OrganizationId == orgId);
        }

        public bool DeleteRawMaterialIssueStock(long id, long userId, long orgId)
        {
            _rawMaterialIssueStockInfoRepository.DeleteAll(i => i.RawMaterialIssueStockId == id && i.OrganizationId == orgId);
            return _rawMaterialIssueStockInfoRepository.Save();
        }

        public RawMaterialIssueStockInfo GetRawMaterialIssueStockUnitById(long id, long orgId)
        {
            return _rawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == id && i.OrganizationId == orgId);
        }

        //public IEnumerable<RawMaterialIssueStockInfo> GetRawMaterialIssueStockUnitById(long id, long orgId)
        //{
        //    return _rawMaterialIssueStockInfoRepository.GetAll(i => i.RawMaterialId == id && i.OrganizationId == orgId);
        //}
    }
}
