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
    public class FinishGoodProductBusiness : IFinishGoodProductBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodProductRepository _finishGoodProductRepository;

        public FinishGoodProductBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodProductRepository = new FinishGoodProductRepository(this._AgricultureUnitOfWork);
        }
        public FinishGoodProduct GetFinishGoodProductById(long FinishGoodProductId, long orgId)
        {
            return _finishGoodProductRepository.GetOneByOrg(r => r.FinishGoodProductId == FinishGoodProductId && r.OrganizationId == orgId);
        }
        public IEnumerable<FinishGoodProduct> GetAllProductInfo(long OrgId)
        {
            try
            {
                return _finishGoodProductRepository.GetAll(a => a.OrganizationId == OrgId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveFinishGoodProductName(FinishGoodProductDTO finishGoodProduct, long userId, long orgId)
        {
            bool IsSuccess = false;
            if (finishGoodProduct.FinishGoodProductId == 0)
            {
                FinishGoodProduct finishGoodProductName = new FinishGoodProduct()
                {
                    OrganizationId = orgId,
                    FinishGoodProductName = finishGoodProduct.FinishGoodProductName,
                    RoleId = finishGoodProduct.RoleId,
                    EntryDate = DateTime.Now,
                    EntryUser = userId,
                    Status = finishGoodProduct.Status
                };

                _finishGoodProductRepository.Insert(finishGoodProductName);
            }
            else
            {
                FinishGoodProduct finishGoodProductName = new FinishGoodProduct();
                finishGoodProductName = GetFinishGoodProductById(finishGoodProduct.FinishGoodProductId, orgId);
                finishGoodProductName.FinishGoodProductName = finishGoodProduct.FinishGoodProductName;
                finishGoodProductName.OrganizationId = orgId;
                finishGoodProductName.Status = finishGoodProduct.Status;
                finishGoodProductName.UpdateDate = DateTime.Now;
                finishGoodProductName.UpdateUser = userId;
                _finishGoodProductRepository.Update(finishGoodProductName);
            }
            IsSuccess = _finishGoodProductRepository.Save();
            return IsSuccess;
        }

        public IEnumerable<FinishGoodProduct> GetProductNameByOrgId(long orgId)
        {
            return _finishGoodProductRepository.GetAll(des => des.OrganizationId == orgId).ToList();
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetFGProductByDate(string EntryDate)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForGetFGProductByDate(EntryDate)).ToList();
        }

        private string QueryForGetFGProductByDate(string EntryDate)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (!string.IsNullOrEmpty(EntryDate) && EntryDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(EntryDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(f.EntryDate as date)='{0}'", fDate);
            }
            query = string.Format(@" 
select DISTINCT p.FinishGoodProductName,p.FinishGoodProductId from FinishGoodProductionInfoes f
inner join tblFinishGoodProductInfo p on f.FinishGoodProductId = p.FinishGoodProductId
where 1=1 {0} and f.ProductionOtherExpense = 0"

, Utility.ParamChecker(param));

            return query;
        }
    }
}
