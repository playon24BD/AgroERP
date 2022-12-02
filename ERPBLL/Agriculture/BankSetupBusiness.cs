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
    public class BankSetupBusiness:IBankSetup
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly BankSetupRepository _bankSetupRepository;

        public BankSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._bankSetupRepository = new BankSetupRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<BankSetup> GetAllBankSetup(long OrgId)
        {
            return _bankSetupRepository.GetAll(a => a.OrganizationId == OrgId);
        }

        public BankSetup GetBankNameById(long bankId, long orgId)
        {
            return _bankSetupRepository.GetOneByOrg(a => a.BankId == bankId && a.OrganizationId == orgId);
        }

        public bool SaveBankInfo(BankSetupDTO infoDTO, long userId, long orgId)
        {
            bool saveSuccess = false;
            if (infoDTO.BankId == 0)
            {

                BankSetup bankInfo = new BankSetup()
                {
                    BankName = infoDTO.BankName,
                    MobileNumber = infoDTO.MobileNumber,
                    AccountNumber = infoDTO.AccountNumber,
                    Email = infoDTO.Email,
                    OrganizationId = orgId,
                    RoleId = infoDTO.RoleId,
                   //UpdateDate = DateTime.Now,
                   //UpdateUserId = infoDTO.UpdateUserId,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    Status = infoDTO.Status
                };

                _bankSetupRepository.Insert(bankInfo);
            }

            else
            {
                BankSetup bankSetup = new BankSetup();
                bankSetup = GetBankNameById(infoDTO.BankId, orgId);
                bankSetup.BankName = infoDTO.BankName;
                bankSetup.MobileNumber = infoDTO.MobileNumber;
                bankSetup.AccountNumber = infoDTO.AccountNumber;
                bankSetup.Email = infoDTO.Email;
                bankSetup.OrganizationId = orgId;
                bankSetup.RoleId = infoDTO.RoleId;
                bankSetup.UpdateDate = DateTime.Now;
                bankSetup.UpdateUserId = userId;
                bankSetup.EntryDate = bankSetup.EntryDate;
                bankSetup.EntryUserId = bankSetup.EntryUserId;
                bankSetup.Status = infoDTO.Status;

            }

            saveSuccess = _bankSetupRepository.Save();
            return saveSuccess;
        }
    }
}
