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
    public class FinishGoodProductSupplierBusiness : IFinishGoodProductSupplierBusiness
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly FinishGoodProductSupplierRepository _finishGoodProductSupplierRepository;

        public FinishGoodProductSupplierBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._finishGoodProductSupplierRepository = new FinishGoodProductSupplierRepository(this._AgricultureUnitOfWork);
        }

        public FinishGoodSupplier GetFinishGoodProductSupplierById(long FinishGoodSupplierId, long orgId)
        {
            return _finishGoodProductSupplierRepository.GetOneByOrg(r => r.FinishGoodSupplierId == FinishGoodSupplierId && r.OrganizationId == orgId);
        }
        public IEnumerable<FinishGoodSupplier> GetAllProductSupplierInfo(long OrgId)
        {
            return _finishGoodProductSupplierRepository.GetAll(a => a.OrganizationId == OrgId);
        }

        public bool SaveFinishGoodProductSupplierName(FinishGoodSupplierDTO finishGoodProductSupplier, long userId, long orgId)
        {
            bool IsSuccess = false;
            if (finishGoodProductSupplier.FinishGoodSupplierId == 0)
            {
                FinishGoodSupplier finishGoodProductSupplierInfo = new FinishGoodSupplier()
                {
                    OrganizationId = orgId,
                    FinishGoodSupplierName = finishGoodProductSupplier.FinishGoodSupplierName,
                    MobileNumber = finishGoodProductSupplier.MobileNumber,
                    Address = finishGoodProductSupplier.Address,
                    RoleId = finishGoodProductSupplier.RoleId,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    Status = finishGoodProductSupplier.Status
                };

                _finishGoodProductSupplierRepository.Insert(finishGoodProductSupplierInfo);
            }
            else
            {
                FinishGoodSupplier finishGoodProductSupplierInfo = new FinishGoodSupplier();
                finishGoodProductSupplierInfo = GetFinishGoodProductSupplierById(finishGoodProductSupplier.FinishGoodSupplierId, orgId);
                finishGoodProductSupplierInfo.FinishGoodSupplierName = finishGoodProductSupplier.FinishGoodSupplierName;
                finishGoodProductSupplierInfo.MobileNumber = finishGoodProductSupplier.MobileNumber;
                finishGoodProductSupplierInfo.Address = finishGoodProductSupplier.Address;
                finishGoodProductSupplierInfo.OrganizationId = orgId;
                finishGoodProductSupplierInfo.Status = finishGoodProductSupplier.Status;
                finishGoodProductSupplierInfo.UpdateDate = DateTime.Now;
                finishGoodProductSupplierInfo.UpdateUserId = userId;
                _finishGoodProductSupplierRepository.Update(finishGoodProductSupplierInfo);
            }
            IsSuccess = _finishGoodProductSupplierRepository.Save();
            return IsSuccess;
        }
    }
    
}
