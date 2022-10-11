﻿using ERPBLL.Agriculture.Interface;
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
    public class RegionSetupBusiness : IRegionSetup
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly RegionSetupRepository _regionSetupRepository;

        public RegionSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._regionSetupRepository = new RegionSetupRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<RegionSetup> GetAllRegionSetup(long OrgId)
        {
            return _regionSetupRepository.GetAll(x => x.OrganizationId == OrgId).ToList();
        }

        public RegionSetup GetRegionNamebyId(long regionId, long orgId)
        {
            return _regionSetupRepository.GetOneByOrg(x => x.RegionId == regionId && x.OrganizationId == orgId);
        }

        public bool SaveRegionInfo(List<RegionSetupDTO> detailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<RegionSetup> RegionSetup = new List<RegionSetup>();

            foreach (var item in detailsDTO)
            {
                if (item.RegionId == 0)
                {
                    RegionSetup regionSetups = new RegionSetup()
                    {
                        RegionId = item.RegionId,
                        OrganizationId = orgId,
                        DivisionId= item.DivisionId,
                        RegionName = item.RegionName,
                        

                        EntryUserId = userId,
                        //UpdateUserId = userId,
                        EntryDate = DateTime.Now,
                        //UpdateDate = DateTime.Now,
                        Status= "Active"

                    };
                    _regionSetupRepository.Insert(regionSetups);
                }


            }

            IsSuccess = _regionSetupRepository.Save();
            return IsSuccess;
        }
    }
}
