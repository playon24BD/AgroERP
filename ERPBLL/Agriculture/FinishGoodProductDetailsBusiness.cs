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
    public class FinishGoodProductDetailsBusiness :IFinishGoodProductionDetailsBusiness
    {
        private readonly FinishGoodProductionDetailsRepository _finishGoodProductionDetailsRepository;
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;

        
       public FinishGoodProductDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionDetailsRepository = new FinishGoodProductionDetailsRepository(this._agricultureUnitOfWork);

        }

        public IEnumerable<FinishGoodProductionDetailsDTO> GetFinishGoodDetailsListView(string finishGoodProductionBatch, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionDetailsDTO>(QueryForFinishGoodProductionDetails(finishGoodProductionBatch, orgId)).ToList();
        }

        private string QueryForFinishGoodProductionDetails(string finishGoodProductionBatch, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(finishGoodProductionBatch))
            {
                param += string.Format(@" and d.FinishGoodProductionBatch ='{0}'", finishGoodProductionBatch);
            }

            query = string.Format(@"



select d.FinishGoodProductDetailId,d.FinishGoodProductionBatch, rm.RawMaterialName,d.FGRRawMaterQty,d.TotalQuantity,d.RequiredQuantity from FinishGoodProductionDetails d
inner join tblRawMaterialInfo rm on d.RawMaterialId=rm.RawMaterialId

Where 1=1 {0} ", Utility.ParamChecker(param));

            return query;
        }
        public IEnumerable<FinishGoodProductionDetails> GetFinishGoodProductionDetails(long orgId)
        {
            return _finishGoodProductionDetailsRepository.GetAll(fg=>fg.OrganizationId==orgId);
        }

        public IEnumerable<FinishGoodProductionDetails> GetFinishGoodProductionDetails(string finishGoodProductionBatch,long orgId)
        {
            try
            {
                return _finishGoodProductionDetailsRepository.GetAll(f => f.FinishGoodProductionBatch == finishGoodProductionBatch);
            }
            catch (Exception)
            {
                return null;
            }
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
