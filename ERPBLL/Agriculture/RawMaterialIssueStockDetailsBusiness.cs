using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class RawMaterialIssueStockDetailsBusiness : IRawMaterialIssueStockDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RawMaterialIssueStockDetailsRepository _rawMaterialIssueStockDetailsRepository;

        public RawMaterialIssueStockDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialIssueStockDetailsRepository = new RawMaterialIssueStockDetailsRepository(this._agricultureUnitOfWork);
            //this._rawMaterialStockInfo = rawMaterialStockInfo;
            //this._rawMaterialStockInfoRepositiory = new RawMaterialStockInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialIssueStockDetails> GetRawMaterialIssueStockDetailsById(long infoId, long orgId)
        {
            return _rawMaterialIssueStockDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.RawMaterialIssueStockId == infoId).ToList();
        }

        public bool SaveIssuerawMaterialStockDetail(long OrganizationId, long RawMaterialId, int Quantity, string Unit, DateTime? IssueDate, DateTime? EntryDate, long? EntryUserId, DateTime? UpdateDate, long? UpdateUserId, string Status, long RawMaterialIssueStockId)
        {
            List<RawMaterialIssueStockDetails> IssueRawMaterialStockDetail = new List<RawMaterialIssueStockDetails>();


            RawMaterialIssueStockDetails IssuestockDetails = new RawMaterialIssueStockDetails();

            IssuestockDetails.OrganizationId = OrganizationId;
            IssuestockDetails.RawMaterialId = RawMaterialId;
            //  var RawMaterialId = item.RawMaterialId;
            IssuestockDetails.Quantity = Quantity;
            IssuestockDetails.Unit = Unit;
            IssuestockDetails.IssueDate = IssueDate;
            IssuestockDetails.UpdateDate = DateTime.Now;
            IssuestockDetails.UpdateUserId = UpdateUserId;
            IssuestockDetails.Status = Status;
            IssuestockDetails.RawMaterialIssueStockId = RawMaterialIssueStockId;
            IssueRawMaterialStockDetail.Add(IssuestockDetails);

            //}

            _rawMaterialIssueStockDetailsRepository.InsertAll(IssueRawMaterialStockDetail);
            return _rawMaterialIssueStockDetailsRepository.Save();
        }
    }
}
