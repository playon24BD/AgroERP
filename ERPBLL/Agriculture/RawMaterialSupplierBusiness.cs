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
    public class RawMaterialSupplierBusiness:IRawMaterialSupplier
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly RawMaterialSupplierRepository _rawMaterialSupplierRepository;

        public RawMaterialSupplierBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = agricultureUnitOfWork;
            this._rawMaterialSupplierRepository = new RawMaterialSupplierRepository(this._AgricultureUnitOfWork);
        }

        public IEnumerable<RawMaterialSupplier> GetAllRawMaterialSupplierInfo(long OrgId)
        {
            try
            {
                return _rawMaterialSupplierRepository.GetAll(a => a.OrganizationId == OrgId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveRawMaterialSupplierName(RawMaterialSupplierDTO rawMaterialSupplier, long userId, long orgId)
        {
            bool IsSuccess = false;
            if (rawMaterialSupplier.RawMaterialSupplierId == 0)
            {
                RawMaterialSupplier rawMaterialSupplierInfo = new RawMaterialSupplier()
                {
                    OrganizationId = orgId,
                    RawMaterialSupplierName = rawMaterialSupplier.RawMaterialSupplierName,
                    MobileNumber = rawMaterialSupplier.MobileNumber,
                    Address = rawMaterialSupplier.Address,
                    RoleId = rawMaterialSupplier.RoleId,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    TradeLicense = rawMaterialSupplier.TradeLicense,
                    TIN= rawMaterialSupplier.TIN,
                    BIN = rawMaterialSupplier.BIN,
                    Status = rawMaterialSupplier.Status
                };

                _rawMaterialSupplierRepository.Insert(rawMaterialSupplierInfo);
            }
            else
            {
                RawMaterialSupplier rawMaterialSupplierInfo = new RawMaterialSupplier();
                rawMaterialSupplierInfo = GetrawMaterialSupplierById (rawMaterialSupplier.RawMaterialSupplierId, orgId);
                rawMaterialSupplierInfo.RawMaterialSupplierName = rawMaterialSupplier.RawMaterialSupplierName;
                rawMaterialSupplierInfo.MobileNumber = rawMaterialSupplier.MobileNumber;
                rawMaterialSupplierInfo.Address = rawMaterialSupplier.Address;
                rawMaterialSupplierInfo.TradeLicense = rawMaterialSupplier.TradeLicense;
                rawMaterialSupplierInfo.TIN = rawMaterialSupplier.TIN;
                rawMaterialSupplierInfo.BIN = rawMaterialSupplier.BIN;
                rawMaterialSupplierInfo.OrganizationId = orgId;
                rawMaterialSupplierInfo.Status = rawMaterialSupplier.Status;
                rawMaterialSupplierInfo.UpdateDate = DateTime.Now;
                rawMaterialSupplierInfo.UpdateUserId = userId;
                _rawMaterialSupplierRepository.Update(rawMaterialSupplierInfo);
            }
            IsSuccess = _rawMaterialSupplierRepository.Save();
            return IsSuccess;
        }

        public RawMaterialSupplier GetrawMaterialSupplierById(long rawMaterialSupplierId, long orgId)
        {
            return _rawMaterialSupplierRepository.GetOneByOrg(r => r.RawMaterialSupplierId == rawMaterialSupplierId && r.OrganizationId == orgId);
        }
    }
}
