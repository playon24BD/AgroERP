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
                    DepotId = rawMaterial.DepotId,
                    Status=rawMaterial.Status,
                    Unit=rawMaterial.Unit,
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
                material.DepotId = rawMaterial.DepotId;
                //material.ExpireDate = rawMaterial.ExpireDate;
                material.Status = rawMaterial.Status;
                material.Unit = rawMaterial.Unit;
                material.UpdateDate = rawMaterial.UpdateDate;
                material.UpdateUserId = rawMaterial.UpdateUserId;
                _rawMaterialRepository.Update(material);
            }
            IsSuccess = _rawMaterialRepository.Save();
            return IsSuccess;
            
        }
    }
}
