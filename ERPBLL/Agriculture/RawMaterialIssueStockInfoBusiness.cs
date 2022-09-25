﻿using ERPBLL.Agriculture.Interface;
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

        public RawMaterialIssueStockInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialIssueStockDetailsBusiness rawMaterialIssueStockDetailsBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialIssueStockInfoRepository = new RawMaterialIssueStockInfoRepository(this._agricultureUnitOfWork);

            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;



        }
        public RawMaterialIssueStockInfo RawMaterialStockInfoCheckValues(long? RawMaterialId, long orgId)
        {
            return _rawMaterialIssueStockInfoRepository.GetOneByOrg(i => i.RawMaterialId == RawMaterialId && i.OrganizationId==orgId);
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
                            UpdateDate = DateTime.Now,
                            UpdateUserId = userId,
                            RawMaterialIssueStockId = item.RawMaterialIssueStockId,
                            Status =  "Pending"

                        };
                        IssuestockDetails.Add(IssuerawMaterials);
                        var IssueRawMaterialStock = RawMaterialStockInfoCheckValues(orgId, item.RawMaterialId);

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
                            UpdateDate = DateTime.Now,
                            UpdateUserId = userId,
                            RawMaterialIssueStockId = item.RawMaterialIssueStockId,
                            Status =  "Pending"

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
                        var IssueRawMaterialStockInfoid = RawMaterialStockInfoCheckValues(items.OrganizationId, items.RawMaterialId);

                        isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveIssuerawMaterialStockDetail(items.OrganizationId, items.RawMaterialId, items.Quantity, items.Unit, items.IssueDate, items.EntryDate, items.EntryUserId, items.UpdateDate, items.UpdateUserId, items.Status, IssueRawMaterialStockInfoid.RawMaterialIssueStockId);
                    }



                }
                // isSuccess = _rawMaterialStockInfoRepository.Save();
            }
            return isSuccess;
        }
    }
}
