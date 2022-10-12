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
    public class AreaSetupBusiness : IAreaSetupBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AreaSetupInfoRepository _areaSetupInfoRepository;

        public AreaSetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._areaSetupInfoRepository = new AreaSetupInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<AreaSetupDTO> GetAllAreaSetup(long OrgId,string name)
       {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AreaSetupDTO>(QueryForRawAreaList(OrgId,name)).ToList();
        }

        private string QueryForRawAreaList(long orgId,string name)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and A.OrganizationId={0}", orgId);
            if (name != null && name!= "")
            {
                param += string.Format(@" and A.AreaName like '%{0}%'", name);
            }


                query = string.Format(@"
SELECT A.AreaId,A.AreaName,A.Status,A.RegionId,R.RegionName
From [Agriculture].[dbo].[tblAreaSetup] A
Inner JOIN [Agriculture].[dbo].[tblRegionInfos] R on A.RegionId=R.RegionId
            where 1=1  {0} ",
            Utility.ParamChecker(param));
            return query;
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

        public bool SaveAreaInfoUpdate(AreaSetupDTO areaDetailDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;

            AreaInfoSetup AreaUpdateList = new AreaInfoSetup();
            AreaUpdateList = GetAreaById(areaDetailDTOs.AreaId, orgId);
            AreaUpdateList.RegionId = areaDetailDTOs.RegionId;
            AreaUpdateList.AreaName = areaDetailDTOs.AreaName;
            AreaUpdateList.Status = areaDetailDTOs.Status;
            AreaUpdateList.OrganizationId = orgId;
            AreaUpdateList.UpdateDate = DateTime.Now;
            AreaUpdateList.UpdateUserId = userId;
            _areaSetupInfoRepository.Update(AreaUpdateList);
      
        IsSuccess = _areaSetupInfoRepository.Save();
            return IsSuccess;
        }

        public AreaInfoSetup GetAreaById(long areaId, long orgId)
        {
            return _areaSetupInfoRepository.GetOneByOrg(r => r.AreaId == areaId && r.OrganizationId == orgId);
        }
    }
}
