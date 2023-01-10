using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPBLL.Common;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
   public class FinishGoodRecipeDetailsBusiness : IFinishGoodRecipeDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodRecipeDetailsRepository _finishGoodRecipeDetailsRepository;
        private readonly FinishGoodRecipeInfoRepository _finishGoodRecipeInfoRepository;
        public FinishGoodRecipeDetailsBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodRecipeDetailsRepository = new FinishGoodRecipeDetailsRepository(this._AgricultureUnitOfWork);
            this._finishGoodRecipeInfoRepository = new FinishGoodRecipeInfoRepository(this._AgricultureUnitOfWork);
            //this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
        }
        public IEnumerable<FinishGoodRecipeDetailsDTO> FinishGoodRecipeMinQty(string RawMaterialIdList, long orgId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeDetailsDTO>(QueryForFinishGoodReceipeMinQty(RawMaterialIdList, orgId)).ToList();
        }

        private string QueryForFinishGoodReceipeMinQty(string rawMaterialIdList, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and OrganizationId={0}", orgId);
            if (rawMaterialIdList != null && rawMaterialIdList != "")
            {
                param += string.Format(@" And RawMaterialId in ({0})", rawMaterialIdList);
            }
            query = string.Format(@" SELECT MIN(FGRRawMaterQty) FGRRawMaterQty from tblFinishGoodRecipeDetails
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public FinishGoodRecipeDetails GetFinishGoodRecipeDetailsById(long fgDetailsId,long orgId)
        {
           return _finishGoodRecipeDetailsRepository.GetOneByOrg(a=>a.FGRDetailsId==fgDetailsId && a.OrganizationId==orgId);
        }

        public IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByInfoId(long infoId, long orgId)
        {
            try
            {
                return _finishGoodRecipeDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.FGRId == infoId).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool updateFinishGoodRecipDetails(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> finishGoodRecipeDetailDTO, long userId, long orgId)



        //public bool updateFinishGoodRecipDetails(List<FinishGoodRecipeDetailsDTO> finishGoodRecipeDetailDTO, long userId, long orgId)

        {
            bool IsSuccess = false;
            var reDetailInDb = GetRecipeById(info.FGRId, orgId);
            if (info.FGRId != 0)
            {
               
                    reDetailInDb.Status = info.Status;
                    reDetailInDb.UpUserId = userId;
                    reDetailInDb.UpdateDate = DateTime.Now;
                _finishGoodRecipeInfoRepository.Update(reDetailInDb);
                
            }
            IsSuccess = _finishGoodRecipeInfoRepository.Save();
            

            List<FinishGoodRecipeDetails> finishGoodRecipeDetails = new List<FinishGoodRecipeDetails>();
            FinishGoodRecipeDetails finishGoodRecipeDetail = new FinishGoodRecipeDetails();
            foreach (var item in finishGoodRecipeDetailDTO)
            {
                finishGoodRecipeDetail= GetFinishGoodRecipeDetailsById(item.FGRDetailsId, orgId);
                finishGoodRecipeDetail.FGRRawMaterQty = item.FGRRawMaterQty;
                finishGoodRecipeDetail.UpdateDate = DateTime.Now;
                finishGoodRecipeDetail.UpUserId = userId;
                finishGoodRecipeDetails.Add(finishGoodRecipeDetail);

            }
            _finishGoodRecipeDetailsRepository.UpdateAll(finishGoodRecipeDetails);
            IsSuccess = _finishGoodRecipeDetailsRepository.Save();

            return IsSuccess;
        }

       
        public FinishGoodRecipeInfo GetRecipeById(long fGRId, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(rd => rd.FGRId == fGRId && rd.OrganizationId == orgId).FirstOrDefault();
        }

        public IEnumerable<FinishGoodRecipeDetails> GetFinishGoodRecipeDetailsByBatchCode(string receipeBatchCode, long orgId)
        {
            return _finishGoodRecipeDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.ReceipeBatchCode == receipeBatchCode).ToList();
        }

        public IEnumerable<FinishGoodRecipeDetailsDTO> GetAgroReciprDetailsByInfoIdRMPrice(long FinishGoodProductId, string EntryDate)
        {
            try
            {
                return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeDetailsDTO>(QueryForGetAgroReciprDetailsByInfoIdRMPrice(FinishGoodProductId, EntryDate)).ToList();
            }


            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForGetAgroReciprDetailsByInfoIdRMPrice(long FinishGoodProductId, string EntryDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (FinishGoodProductId != 0 && FinishGoodProductId > 0)
            {
                param += string.Format(@" and i.FinishGoodProductId={0}", FinishGoodProductId);
            }
             if (!string.IsNullOrEmpty(EntryDate) && EntryDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(i.EntryDate as date)='{0}'", fDate);
            }


            query = string.Format(@"	
select t.RawMaterialId,t.RawMaterialName,ROUND(sum(t.RequiredQuantity),2) as RequiredQuantity ,t.UnitName,t.FinishGoodProductId,sum(t.RMPrice) as RMPrice,SUM(t.RMPriceTotal) RMPriceTotal  from (select i.FGRId,r.RawMaterialId,r.RawMaterialName,d.RequiredQuantity,u.UnitName,i.FinishGoodProductId,

RMPrice=  ROUND(ISNULL((SELECT sum(SR.SubTotal)/sum(SR.Quantity) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=d.RawMaterialId),0),2),

RMPriceTotal= ROUND(ISNULL((SELECT sum(SR.SubTotal)/sum(SR.Quantity) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=d.RawMaterialId),0) * d.RequiredQuantity,2)

from FinishGoodProductionInfoes i
inner join  FinishGoodProductionDetails d on i.FinishGoodProductionBatch = d.FinishGoodProductionBatch
inner join tblRawMaterialInfo r on r.RawMaterialId = d.RawMaterialId
inner join tblAgroUnitInfo u on u.UnitId = r.UnitId Where 1=1 {0} and i.ProductionOtherExpense = 0 )
t
group by t.RawMaterialId,t.RawMaterialName,t.UnitName,t.FinishGoodProductId", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeDetailsDTO> GetFGProductAmount(long FinishGoodProductId, string EntryDate)
        {
            try
            {
                return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeDetailsDTO>(QueryForGetFGProductAmount(FinishGoodProductId, EntryDate)).ToList();
            }

            catch (Exception)
            {
                return null;
            }

        }

        private string QueryForGetFGProductAmount(long FinishGoodProductId, string EntryDate)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (FinishGoodProductId > 0)
            {
                param += string.Format(@" and i.FinishGoodProductId={0}", FinishGoodProductId);
            }
             if (!string.IsNullOrEmpty(EntryDate) && EntryDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(i.EntryDate as date)='{0}'", fDate);
            }

            query = string.Format(@"

select t.FinishGoodProductId,ROUND(ISNULL( sum(t.ProductionRMPrice),0),2) as GrandTotal  from(select i.ProductionRMPrice,i.FinishGoodProductId from FinishGoodProductionInfoes i 
where 1=1 {0} and i.ProductionOtherExpense = 0)
t
group by t.FinishGoodProductId", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetAgroReciprDetailsByInfoIdRMPriceUsedSave(long FinishGoodProductId, string EntryDate)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForGetAgroReciprDetailsByInfoIdRMPriceUsedSave(FinishGoodProductId, EntryDate)).ToList();
        }
        private string QueryForGetAgroReciprDetailsByInfoIdRMPriceUsedSave(long FinishGoodProductId, string EntryDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (FinishGoodProductId != 0 && FinishGoodProductId > 0)
            {
                param += string.Format(@" and t.FinishGoodProductId={0}", FinishGoodProductId);
            }
            if (!string.IsNullOrEmpty(EntryDate) && EntryDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(t.EntryDate as date)='{0}'", fDate);
            }


            query = string.Format(@"	
select * from FinishGoodProductionInfoes t
where 1=1 {0} and t.ProductionOtherExpense = 0", Utility.ParamChecker(param));

            return query;
        }


    }
}
