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

        public ProductionPerproductCostBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._productionPerproductCostRepository = new ProductionPerproductCostRepository(this._agricultureUnitOfWork);
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
            where 1=1  {0} ",
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



        public bool SaveProductionPerproductCost(ProductionPerproductCostDTO productionPerproductCostDTO, long userId)
        {
            bool IsSuccess = false;
            ProductionPerproductCost productionPerproductCost = new ProductionPerproductCost
            {
                FinishGoodProductId = productionPerproductCostDTO.FinishGoodProductId,
                FGRId = productionPerproductCostDTO.FGRId,
                PerProductRMtotalCost = productionPerproductCostDTO.PerProductRMtotalCost,
                PerProductOtherCost = productionPerproductCostDTO.PerProductOtherCost,
                PerProductMainCost = productionPerproductCostDTO.PerProductRMtotalCost + productionPerproductCostDTO.PerProductOtherCost,
                EntryDate = DateTime.Now,
                EntryUser=userId

            };
            _productionPerproductCostRepository.Insert(productionPerproductCost);
            IsSuccess = _productionPerproductCostRepository.Save();
            return IsSuccess;
        }
    }
}
