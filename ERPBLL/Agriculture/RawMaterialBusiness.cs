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
        private readonly IAgricultureUnitOfWork _db;
        public RawMaterialBusiness( IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._db = agricultureUnitOfWork;
            this._rawMaterialRepository = new RawMaterialRepository(this._db);
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
            return _rawMaterialRepository.GetAll(a=>a.OrganizationId==orgId);
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
                };

                _rawMaterialRepository.Insert(material);
            }
            else
            {
                RawMaterial material = new RawMaterial();
                material = GetRawMaterialById(rawMaterial.RawMaterialId,orgId);
                material.RawMaterialName = rawMaterial.RawMaterialName;
                //material.DepotId = rawMaterial.DepotId;
                //material.ExpireDate = rawMaterial.ExpireDate;
                material.Status = rawMaterial.Status;
                material.UnitId = rawMaterial.UnitId;
                material.UpdateDate = rawMaterial.UpdateDate;
                material.UpdateUserId = rawMaterial.UpdateUserId;
                _rawMaterialRepository.Update(material);
            }
            IsSuccess = _rawMaterialRepository.Save();
            return IsSuccess;
            
        }
    }
}
