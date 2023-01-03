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
    public class RawMaterialBusiness : IRawMaterialBusiness
    {
        private readonly RawMaterialRepository _rawMaterialRepository;
        private readonly RMCategoriesRepository _rMCategoriesRepository;

        private readonly IAgricultureUnitOfWork _db;
        public RawMaterialBusiness( IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._db = agricultureUnitOfWork;
            this._rawMaterialRepository = new RawMaterialRepository(this._db);
            this._rMCategoriesRepository = new RMCategoriesRepository(this._db);
        }

        public RawMaterial GetRawMaterialById(long rawMaterialId, long orgId)
        {
            return _rawMaterialRepository.GetOneByOrg(r=>r.RawMaterialId==rawMaterialId && r.OrganizationId==orgId);
        }
        public IEnumerable<ReturnRawMaterialDTO> CheckStatus(long orgId)
        {
            return this._db.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForCheckStatuss(orgId)).ToList();
        }

        private string QueryForCheckStatuss(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;
            query = string.Format(@"SELECT Top 1 * FROM tblReturnRawMaterial where 1=1  and OrganizationId=9 order by ReturnRawMaterialId DESC", Utility.ParamChecker(param));

            return query;

        }
        public IEnumerable<ReturnRawMaterialDTO> GetRawMaterialApproved(long orgId,string status,long RawMaterialId)
        {
            return this._db.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForGetRawMaterialApproved(orgId, status, RawMaterialId)).ToList();
        }

        private string QueryForGetRawMaterialApproved(long orgId, string status, long rawMaterialId)
        {
            string query = string.Empty;
            string param = string.Empty;
            if (status != null && status != "")
            {
                param += string.Format(@"and rt.Status='{0}'", status);
            }
            if (rawMaterialId != 0)
            {
                param += string.Format(@"and rt.RawMaterialId='{0}'", rawMaterialId);
            }

            query = string.Format(@"	SELECT ri.RawMaterialName, * FROM tblReturnRawMaterial rt 
						inner join tblRawMaterialInfo ri on ri.RawMaterialId=rt.RawMaterialId
						where 1=1 {0} and rt.OrganizationId=9  ", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<RawMaterial> GetRawMaterialByOrgId(long orgId)
        {
            return _rawMaterialRepository.GetAll(des => des.OrganizationId == orgId).ToList();
        }

        public IEnumerable<RawMaterial> GetRawMaterials(long orgId)
        {
            try
            {
                return _rawMaterialRepository.GetAll(a => a.OrganizationId == orgId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveRawMaterial(RawMaterialDTO rawMaterial, long userId, long orgId)
        {
            bool IsSuccess = false;
            if (rawMaterial.RawMaterialId==0)
            {
                RawMaterial material = new RawMaterial()
                {
                    OrganizationId = orgId,
                    RawMaterialName = rawMaterial.RawMaterialName,
                    //ExpireDate = rawMaterial.ExpireDate,
                    //DepotId = rawMaterial.DepotId,
                    Status=rawMaterial.Status,
                    UnitId=rawMaterial.UnitId,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    RMCategorieId=rawMaterial.RMCategorieId,
                };

                _rawMaterialRepository.Insert(material);
            }
            else
            {
                RawMaterial material = new RawMaterial();
                material = GetRawMaterialById(rawMaterial.RawMaterialId,orgId);
                material.RawMaterialName = rawMaterial.RawMaterialName;
                material.Status = rawMaterial.Status;
                material.UnitId = rawMaterial.UnitId;
                material.RMCategorieId= rawMaterial.RMCategorieId;
                material.UpdateDate = rawMaterial.UpdateDate;
                material.UpdateUserId = rawMaterial.UpdateUserId;
                _rawMaterialRepository.Update(material);
            }
            IsSuccess = _rawMaterialRepository.Save();
            return IsSuccess;
            
        }

        public IEnumerable<ReturnRawMaterialDTO> CheckStatusApproved(long orgId)
        {
            return this._db.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForCheckStatusApproved(orgId)).ToList();
        }
        private string QueryForCheckStatusApproved(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;
            query = string.Format(@"SELECT * FROM tblReturnRawMaterial where 1=1  and OrganizationId=9 and
Status='Approved' order by ReturnRawMaterialId desc", Utility.ParamChecker(param));

            return query;

        }


        public IEnumerable<RMCategories> GetRMCategories()
        {
            return _rMCategoriesRepository.GetAll().ToList();
           
        }

        public IEnumerable<RawMaterialDTO> GetRawMaterialList(string rawMaterialName)
        {
            return this._db.Db.Database.SqlQuery<RawMaterialDTO>(QueryForRawMaterialList(rawMaterialName)).ToList();
        }

        private string QueryForRawMaterialList(string rawMaterialName)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(rawMaterialName))
            {
                param += string.Format(@"and rmi.RawMaterialName like '%{0}%'", rawMaterialName);
            }

            query = string.Format(@"
  

select c.RMCategorieId,u.UnitId, rmi.RawMaterialId,rmi.RawMaterialName,u.UnitName,rmi.Status,c.RMCategorieName from tblRawMaterialInfo rmi
inner join tblAgroUnitInfo u on u.UnitId=rmi.UnitId
left join tblRMCategories c on c.RMCategorieId=rmi.RMCategorieId

where 1=1 {0} order by rmi.RawMaterialId desc
", Utility.ParamChecker(param));

            return query;
        }



    }
}
