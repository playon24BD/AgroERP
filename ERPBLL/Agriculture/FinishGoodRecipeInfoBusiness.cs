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
using System.Web.UI.WebControls;
using System.Windows.Controls;

namespace ERPBLL.Agriculture
{
    public class FinishGoodRecipeInfoBusiness : IFinishGoodRecipeInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodRecipeInfoRepository _finishGoodRecipeInfoRepository;
        private readonly IFinishGoodRecipeDetailsBusiness _fDetail;
        //, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness
        public FinishGoodRecipeInfoBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork, IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._fDetail = finishGoodRecipeDetailsBusiness;
            this._finishGoodRecipeInfoRepository = new FinishGoodRecipeInfoRepository(this._AgricultureUnitOfWork);

        }
        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByOrgId(long id, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(i => i.FGRId == id && i.OrganizationId == orgId);
        }
        public FinishGoodRecipeInfo GetReceipId(long ProductId, int ProductUnitQty, long UnitId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(r => r.FinishGoodProductId == ProductId && r.FGRQty == ProductUnitQty && r.UnitId == UnitId);
        }
        public FinishGoodRecipeInfo GetReceipbachcodeid(string ReceipeBatchCode)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(r => r.ReceipeBatchCode == ReceipeBatchCode);
        }
        public bool DeletefinishGoodRecipe(long id, long userId, long orgId)
        {
            _finishGoodRecipeInfoRepository.DeleteAll(i => i.FGRId == id && i.OrganizationId == orgId);
            return _finishGoodRecipeInfoRepository.Save();
        }
        public IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodReceipUnitQty(long finishGoodProductId, long orgId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodRecipeUnitQtyProductwise(finishGoodProductId, orgId)).ToList();
        }

        private string QueryForFinishGoodRecipeUnitQtyProductwise(long finishGoodProductId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;
            param += string.Format(@" and RI.OrganizationId={0}", orgId);
            if (finishGoodProductId > 0)
            {
                param += string.Format(@" and RI.FinishGoodProductId={0}", finishGoodProductId);
            }

            query = string.Format(@"SELECT DISTINCT RI.FGRId,concat(RI.FGRQty,'(',U.UnitName,')') as UnitQty
            FROM tblFinishGoodRecipeInfo RI
            INNER JOIN tblAgroUnitInfo U on RI.UnitId=U.UnitId	
            Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodUnitQty(long orgId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodRecipeUnitQty(orgId)).ToList();
        }

        private string QueryForFinishGoodRecipeUnitQty(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;
            param += string.Format(@" and RI.OrganizationId={0}", orgId);
            query = string.Format(@"SELECT DISTINCT RI.FGRId, concat(RI.FGRQty,'(',U.UnitName,')') as UnitQty
            FROM tblFinishGoodRecipeInfo RI
            INNER JOIN tblAgroUnitInfo U on RI.UnitId=U.UnitId	
            Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeInfoDTO> GetFinishGoodRecipeInfos(long orgId, long? ProductId)
        {
            try
            {
                return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodRecipeInfoss(orgId, ProductId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForFinishGoodRecipeInfoss(long orgId, long? productId)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                param += string.Format(@" and fgr.OrganizationId={0}", orgId);
                if (productId != null && productId > 0)
                {
                    param += string.Format(@" and fgr.FinishGoodProductId={0}", productId);
                }
                query = string.Format(@"SELECT fgr.FGRId,fgr.FinishGoodProductId,fg.FinishGoodProductName,
                  fgr.FGRQty,fgr.UnitId,unit.UnitName,fgr.OrganizationId 
                 FROM [Agriculture].dbo.tblFinishGoodRecipeInfo fgr
                 INNER JOIN [Agriculture].dbo.tblFinishGoodProductInfo fg on fgr.FinishGoodProductId=fg.FinishGoodProductId 
inner join tblAgroUnitInfo unit on unit.UnitId=fgr.UnitId	
Where 1=1  {0} order by fgr.FGRId desc", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveFinishGoodRecipe(FinishGoodRecipeInfoDTO info, List<FinishGoodRecipeDetailsDTO> details, long userId, long orgId)
        {
            bool IsSuccess = false;
            var ReceipeBatchCodes = "RecBC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            //var CheckDupliketReceipeProduct = GetAllFini();
            if (info.FGRId == 0)
            {
                FinishGoodRecipeInfo model = new FinishGoodRecipeInfo
                {
                    FinishGoodProductId = info.FinishGoodProductId,
                    ReceipeBatchCode = ReceipeBatchCodes,
                    FGRQty = info.FGRQty,
                    UnitId = info.UnitId,
                    OrganizationId = orgId,
                    Status = info.Status,
                    EUserId = userId,
                    EntryDate = DateTime.Now

                };
                List<FinishGoodRecipeDetails> modelDetails = new List<FinishGoodRecipeDetails>();

                foreach (var item in details)
                {
                    FinishGoodRecipeDetails FinishGoodRecipeDetails = new FinishGoodRecipeDetails()
                    {
                        RawMaterialId = item.RawMaterialId,
                        ReceipeBatchCode = ReceipeBatchCodes,
                        FGRRawMaterQty = item.FGRRawMaterQty,
                        UnitId = item.UnitId,
                        OrganizationId = orgId,
                        EUserId = userId,
                        EntryDate = DateTime.Now
                    };
                    modelDetails.Add(FinishGoodRecipeDetails);
                }
                model.FinishGoodRecipeDetails = modelDetails;

                _finishGoodRecipeInfoRepository.Insert(model);
                IsSuccess = _finishGoodRecipeInfoRepository.Save();
            }
            else
            {

                IsSuccess = _fDetail.updateFinishGoodRecipDetails(info, details, userId, orgId);

            }
            return IsSuccess;
        }



        public IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceif(long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(f => f.OrganizationId == orgId);
        }

        public IEnumerable<FinishGoodRecipeInfo> GetCheckDupliketReceipeProduct(long FinishGoodProductId, int FGRQty, long UnitId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(f => f.FinishGoodProductId == FinishGoodProductId && f.FGRQty == FGRQty && f.UnitId == UnitId);
        }
        //public IEnumerable<FinishGoodRecipeInfoDTO> GetAllFinishGoodReceif(long orgId)
        //{
        //    return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForFinishGoodProducts(orgId)).ToList();
        //}

        //        private string QueryForFinishGoodProducts(long orgId)
        //        {
        //            string query = string.Empty;
        //            string param = string.Empty;

        //            param += string.Format(@" and OrganizationId={0}", orgId);

        //            query = string.Format(@"SELECT fgr.FGRId,fgr.FinishGoodProductId,fg.FinishGoodProductName,
        //                  fgr.FGRQty,fgr.UnitId,unit.UnitName,fgr.OrganizationId 
        //                 FROM [Agriculture].dbo.tblFinishGoodRecipeInfo fgr
        //                 INNER JOIN [Agriculture].dbo.tblFinishGoodProductInfo fg on fgr.FinishGoodProductId=fg.FinishGoodProductId 
        //inner join tblAgroUnitInfo unit on unit.UnitId=fgr.UnitId	
        //Where 1=1 {0}", Utility.ParamChecker(param));

        //            return query;
        //        }

        public IEnumerable<FinishGoodRecipeInfo> GetAllFinishGoodReceipCode(long productId, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetAll(a => a.FinishGoodProductId == productId && a.OrganizationId == orgId).ToList();
        }



        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByFGID(long id, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(a => a.FinishGoodProductId == id && a.OrganizationId == orgId);
        }

        public FinishGoodRecipeInfo GetFinishGoodRecipeInfoOneByBatchCode(string receipeBatchCode, long orgId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(i => i.ReceipeBatchCode == receipeBatchCode && i.OrganizationId == orgId);
        }
        public FinishGoodRecipeInfo GetUnitId(string ReceipeBatchCode, double UnitId)
        {
            return _finishGoodRecipeInfoRepository.GetOneByOrg(i => i.ReceipeBatchCode == ReceipeBatchCode && i.UnitId == UnitId);
        }

        public IEnumerable<MeasurementSetupDTO> GetAllMEarusmentUnitQty(long FinishGoodProductInfoId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<MeasurementSetupDTO>(QueryForGetAllMEarusmentUnitQty(FinishGoodProductInfoId)).ToList();
        }


        private string QueryForGetAllMEarusmentUnitQty(long? FinishGoodProductInfoId)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (FinishGoodProductInfoId != null && FinishGoodProductInfoId > 0)
            {
                param += string.Format(@" and f.FinishGoodProductId={0}", FinishGoodProductInfoId);
            }
            query = string.Format(@" 
select  distinct p.PackageName,m.MeasurementId, f.FinishGoodProductId
from tblMeasurement m
 inner join PackageDetails p on m.MeasurementId = p.MeasurementId
 inner join tblAgroUnitInfo un on m.UnitId=un.UnitId
 inner join tblFinishGoodRecipeInfo r on r.UnitId = un.UnitId
 inner join tblFinishGoodProductInfo f on f.FinishGoodProductId = r.FinishGoodProductId
Where 1=1 {0} and r.FGRQty=m.PackSize and r.UnitId=m.UnitId"

, Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeInfoDTO> GetAllRecipeBYmeasurment(long FinishGoodProductId, long MeasurementId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(QueryForGetAllRecipiebyMeasurment(FinishGoodProductId, MeasurementId)).ToList();
        }
        private string QueryForGetAllRecipiebyMeasurment(long? FinishGoodProductId, long? MeasurementId)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (FinishGoodProductId != null && FinishGoodProductId > 0)
            {
                param += string.Format(@" and r.FinishGoodProductId={0}", FinishGoodProductId);
            }
            if (MeasurementId != null && MeasurementId > 0)
            {
                param += string.Format(@" and m.MeasurementId={0}", MeasurementId);
            }
            query = string.Format(@" 
select distinct m.MeasurementId,r.FinishGoodProductId,r.ReceipeBatchCode,r.FGRQty,u.UnitName,r.FGRId from tblFinishGoodRecipeInfo r
inner join tblFinishGoodProductInfo p on r.FinishGoodProductId=p.FinishGoodProductId
inner join  tblAgroUnitInfo u on r.UnitId=u.UnitId
inner join tblMeasurement m on u.UnitId= m.UnitId
where 1=1 {0} and r.FGRQty=m.PackSize and r.UnitId=m.UnitId and r.FinishGoodProductId=p.FinishGoodProductId"

, Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodRecipeInfoDTO> GetQTKGBYmeasurment(long FinishGoodProductId, long MeasurementId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodRecipeInfoDTO>(GetRacpbyPIDandMids(FinishGoodProductId, MeasurementId)).ToList();
        }

        private string GetRacpbyPIDandMids(long FinishGoodProductId, long MeasurementId)
        {



            string query = string.Empty;
            string param = string.Empty;


            if (FinishGoodProductId != null && FinishGoodProductId > 0)
            {
                param += string.Format(@" and r.FinishGoodProductId={0}", FinishGoodProductId);
            }
            if (MeasurementId != null && MeasurementId > 0)
            {
                param += string.Format(@" and m.MeasurementId={0}", MeasurementId);
            }
            query = string.Format(@" 
            select distinct concat(r.FGRQty,'(',u.UnitName,')') as QtyKG from tblFinishGoodRecipeInfo r
            inner join tblFinishGoodProductInfo p on r.FinishGoodProductId=p.FinishGoodProductId
            inner join  tblAgroUnitInfo u on r.UnitId=u.UnitId
            inner join tblMeasurement m on u.UnitId= m.UnitId
            where 1=1 {0} and r.FGRQty=m.PackSize and r.UnitId=m.UnitId and r.FinishGoodProductId=p.FinishGoodProductId", Utility.ParamChecker(param));
            return query;


        }


    }
}
