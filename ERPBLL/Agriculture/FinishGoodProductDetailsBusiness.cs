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
    public class FinishGoodProductDetailsBusiness :IFinishGoodProductionDetailsBusiness
    {
        private readonly FinishGoodProductionDetailsRepository _finishGoodProductionDetailsRepository;
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;

        
       public FinishGoodProductDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionDetailsRepository = new FinishGoodProductionDetailsRepository(this._agricultureUnitOfWork);

        }
        public IEnumerable<FinishGoodProductionDetails> GetFinishGoodProductionDetails(long orgId)
        {
            return _finishGoodProductionDetailsRepository.GetAll(fg=>fg.OrganizationId==orgId);
        }

        public IEnumerable<FinishGoodProductionDetails> GetFinishGoodProductionDetails(string finishGoodProductionBatch,long orgId)
        {
            return _finishGoodProductionDetailsRepository.GetAll(f => f.FinishGoodProductionBatch == finishGoodProductionBatch);
        }

        public FinishGoodProductionDetails GetFinishGoodProductionDetailsByAny(string any, long orgId)
        {
            throw new NotImplementedException();
        }

        public FinishGoodProductionDetails GetProductionInfoById(long id, long orgId)
        {
            throw new NotImplementedException();
        }

        public bool SaveFinishGoodDetails(List<FinishGoodProductionDetailsDTO >finishGoodProductionDetailsDTO,string finishGoodProductionBatch, long userId, long orgId)
        {

            bool IsSuccess = false;
            FinishGoodProductionDetails details = new FinishGoodProductionDetails();
            IList<FinishGoodProductionDetails> finishGoodProductionlist = new List<FinishGoodProductionDetails>();

            if (finishGoodProductionDetailsDTO.Count()>0)
            {
                foreach(var item in finishGoodProductionDetailsDTO)
                {
                    details.RawMaterialId = item.RawMaterialId;
                    details.FinishGoodProductionBatch = finishGoodProductionBatch;
                    details.FGRRawMaterQty = item.FGRRawMaterQty;
                    details.TotalQuantity = item.TotalQuantity;
                    details.RequiredQuantity = item.RequiredQuantity;
                    details.EntryDate = DateTime.Now;
                    details.OrganizationId = orgId;
                    details.EntryUserId = userId;
                    //details.Status = "Consumed";
                    details.Status = "Pending";
                    //finishGoodProductionlist.Add(details);
                    _finishGoodProductionDetailsRepository.Insert(details);
                    IsSuccess = _finishGoodProductionDetailsRepository.Save();
                }
                //_finishGoodProductionDetailsRepository.InsertAll(finishGoodProductionlist);

            }

            return IsSuccess;


        }
    }
}
