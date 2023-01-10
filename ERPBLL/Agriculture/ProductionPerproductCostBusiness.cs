using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{



    public class ProductionPerproductCostBusiness: IProductionPerproductCost
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ProductionPerproductCostRepository _productionPerproductCostRepository;
        private readonly FinishGoodProductionInfoRepository _finishGoodProductionInfoRepository;

        private readonly IFinishGoodRecipeDetailsBusiness _finishGoodRecipeDetailsBusiness;
        private readonly IFinishGoodProductionInfoBusiness _finishGoodProductionInfoBusiness;


        public ProductionPerproductCostBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IFinishGoodProductionInfoBusiness finishGoodProductionInfoBusiness,IFinishGoodRecipeDetailsBusiness finishGoodRecipeDetailsBusiness, FinishGoodProductionInfoRepository finishGoodProductionInfoRepository)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._productionPerproductCostRepository = new ProductionPerproductCostRepository(this._agricultureUnitOfWork);
            this._finishGoodProductionInfoRepository= new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this._finishGoodRecipeDetailsBusiness = finishGoodRecipeDetailsBusiness;
            this._finishGoodProductionInfoBusiness = finishGoodProductionInfoBusiness;
      
        }

        public IEnumerable<ProductionPerproductCostDTO> GetAllProductionPerproductCost(string name)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<ProductionPerproductCostDTO>(QueryForPerproductCost(name)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string QueryForPerproductCost(string name)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                if (name != null && name != "")
                {
                    param += string.Format(@" and fp.FinishGoodProductName like '%{0}%'", name);
                }


                query = string.Format(@"
select fp.FinishGoodProductName,fr.FGRQty,un.UnitName,pc.PerProductRMtotalCost,pc.PerProductOtherCost,pc.PerProductMainCost, concat(fr.FGRQty,un.UnitName) as QtyKG from tblProductionPerproductCost pc
inner join tblFinishGoodProductInfo fp on pc.FinishGoodProductId=fp.FinishGoodProductId
inner join tblFinishGoodRecipeInfo fr on pc.FGRId=fr.FGRId
inner join tblAgroUnitInfo un on fr.UnitId=un.UnitId
            where 1=1  {0} order by pc.ProductionPerproductCostId desc",
            Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ProductionPerproductCost GetProductionPerproductCostById(long FGRId)
        {
            return _productionPerproductCostRepository.GetOneByOrg(t=>t.FGRId== FGRId);
        }



        public bool SaveProductionPerproductCost(FinishGoodProductionInfoDTO productionPerproductCostDTO, long userId)
        {

            var FGD = _finishGoodRecipeDetailsBusiness.GetAgroReciprDetailsByInfoIdRMPriceUsedSave(productionPerproductCostDTO.FinishGoodProductId, productionPerproductCostDTO.EntryDate.ToString()).ToList();

            var tqty = FGD.Sum(c => c.TargetQuantity);

            var cnt = FGD.Count();

            var mqty = productionPerproductCostDTO.ProductionOtherExpense/tqty;

            bool IsSuccess = false;



            FinishGoodProductionInfo finishGoodProductionInfo = new FinishGoodProductionInfo();

            foreach (var item in FGD)
            {
               
                 finishGoodProductionInfo = _finishGoodProductionInfoBusiness.GetProductionInfpByperproductionId(item.FinishGoodProductInfoId);
                finishGoodProductionInfo.ProductionOtherExpense = (mqty * finishGoodProductionInfo.TargetQuantity);
                _finishGoodProductionInfoRepository.Update(finishGoodProductionInfo);
               
            }

            IsSuccess = _finishGoodProductionInfoRepository.Save();
            return IsSuccess;
        }
    }
}
