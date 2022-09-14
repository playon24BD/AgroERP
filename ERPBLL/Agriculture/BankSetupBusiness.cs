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

        public bool SaveBankInfo(BankSetupDTO infoDTO, long userId, long orgId)
        {
            BankSetup bankInfo = new BankSetup()
            {
                BankName=infoDTO.BankName,
                MobileNumber=infoDTO.MobileNumber,
                Email=infoDTO.Email,
                OrganizationId=orgId,
                RoleId=infoDTO.RoleId,
                UpdateDate=DateTime.Now,
                UpdateUserId=infoDTO.UpdateUserId,
                EntryDate=DateTime.Now,
                EntryUserId=userId,
                Status=infoDTO.Status
            };

            _bankSetupRepository.Insert(bankInfo);
            bool saveSuccess = false;
            saveSuccess = _bankSetupRepository.Save();
            return saveSuccess;
        }
    }
}
