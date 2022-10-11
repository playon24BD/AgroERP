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
    public class AreaSetupBusiness : IAreaSetupBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AreaSetupInfoRepository _areaSetupInfoRepository;

        public AreaSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._areaSetupInfoRepository = new AreaSetupInfoRepository(this._agricultureUnitOfWork);
        }

        public bool SaveAreaInfo(List<AreaSetupDTO> areaDetailDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<AreaInfoSetup> AreaSetupList = new List<AreaInfoSetup>();

            foreach (var item in areaDetailDTOs)
            {
                if (item.AreaId == 0)
                {
                    AreaInfoSetup areaInfoSetup = new AreaInfoSetup()
                    {
                        OrganizationId = orgId,                                           
                        RegionId = item.RegionId,
                        AreaName = item.AreaName,
                        EntryUserId = userId,
                        //UpdateUserId = userId,
                        EntryDate = DateTime.Now,
                        //UpdateDate = DateTime.Now,
                        RoleId = 0,                      
                        Status="Active"
                    

                    };
                    _areaSetupInfoRepository.Insert(areaInfoSetup);
                }


            }

            IsSuccess = _areaSetupInfoRepository.Save();
            return IsSuccess;
        }
    }
}
