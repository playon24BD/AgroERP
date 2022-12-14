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
            return _finishGoodRecipeDetailsRepository.GetAll(i => i.OrganizationId == orgId && i.FGRId == infoId).ToList();
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

        public IEnumerable<FinishGoodRecipeDetailsDTO> GetAgroReciprDetailsByInfoIdRMPrice(long FGRId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeDetailsDTO>(QueryForGetAgroReciprDetailsByInfoIdRMPrice(FGRId)).ToList();
        }

        private string QueryForGetAgroReciprDetailsByInfoIdRMPrice(long FGRId)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (FGRId != 0 && FGRId > 0)
            {
                param += string.Format(@" and ri.FGRId={0}", FGRId);
            }


            query = string.Format(@"	
select ri.FGRId,rd.FGRDetailsId,rm.RawMaterialName,rd.FGRRawMaterQty,un.UnitName, 
RMPrice= ISNULL((SELECT AVG(SR.UnitPrice) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=rd.RawMaterialId),0),

RMPriceTotal= ISNULL((SELECT AVG(SR.UnitPrice) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=rd.RawMaterialId),0)*rd.FGRRawMaterQty

from tblFinishGoodRecipeInfo ri
inner join tblFinishGoodRecipeDetails rd on ri.FGRId= rd.FGRId
inner join tblRawMaterialInfo rm on rm.RawMaterialId=rd.RawMaterialId
inner join tblAgroUnitInfo un on rm.UnitId=un.UnitId

                Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeDetailsDTO> GetFGProductAmount(long FGRID)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeDetailsDTO>(QueryForGetFGProductAmount(FGRID)).ToList();

        }

        private string QueryForGetFGProductAmount(long FGRID)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (FGRID > 0)
            {
                param += string.Format(@" and ri.FGRId={0}", FGRID);
            }

            query = string.Format(@"

select t.FGRId,ISNULL(sum(t.RMPriceTotal) ,0)as GrandTotal from ( select ri.FGRId,rd.FGRDetailsId,rm.RawMaterialName,rd.FGRRawMaterQty,un.UnitName, 
RMPrice= ISNULL((SELECT AVG(SR.UnitPrice) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=rd.RawMaterialId),0),

RMPriceTotal= ISNULL((SELECT AVG(SR.UnitPrice) from tblPRawMaterialStockDetail SR where SR.RawMaterialId=rd.RawMaterialId),0)*rd.FGRRawMaterQty

from tblFinishGoodRecipeInfo ri
inner join tblFinishGoodRecipeDetails rd on ri.FGRId= rd.FGRId
inner join tblRawMaterialInfo rm on rm.RawMaterialId=rd.RawMaterialId
inner join tblAgroUnitInfo un on rm.UnitId=un.UnitId
Where 1=1 {0}
) t
group by t.FGRId", Utility.ParamChecker(param));

            return query;
        }
    }
}
