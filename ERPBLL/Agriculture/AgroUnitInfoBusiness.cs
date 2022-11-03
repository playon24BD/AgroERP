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
    public class AgroUnitInfoBusiness:IAgroUnitInfo
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly AgroUnitInfoRepository _agroUnitInfoRepository;

        public AgroUnitInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = agricultureUnitOfWork;
            this._agroUnitInfoRepository = new AgroUnitInfoRepository(this._AgricultureUnitOfWork);
        }

        public IEnumerable<AgroUnitInfoDTO> GetAgroUnitInfos(long? unitId, long orgId)
        {
            return this._AgricultureUnitOfWork.Db.Database.SqlQuery<AgroUnitInfoDTO>(QueryForAgroUnitInfoss(unitId, orgId)).ToList();
        }

        private string QueryForAgroUnitInfoss(long? unitId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and d.OrganizationId={0}", orgId);
            if (unitId != null && unitId > 0)
            {
                param += string.Format(@" and d.DivisionId={0}", unitId);
            }

            query = string.Format(@"
           
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }
        

        public IEnumerable<AgroUnitInfo> GetAllAgroUnitInfo(long OrgId)
        {
            return _agroUnitInfoRepository.GetAll(a => a.OrganizationId == OrgId);
        }

        public bool SaveAgroUnitList(AgroUnitInfoDTO infoDTO, long userId, long orgId)
        {
            bool IsSuccess = false;

            if (infoDTO.UnitId == 0)
            {
                AgroUnitInfo agroUnitInfo = new AgroUnitInfo()
                {
                    OrganizationId=orgId,
                    UnitName=infoDTO.UnitName,
                    Status=infoDTO.Status,
                    EntryDate=DateTime.Now,
                    EntryUserId=userId,
                    
                };
                _agroUnitInfoRepository.Insert(agroUnitInfo);
            }
            else
            {
                AgroUnitInfo unitInfo = new AgroUnitInfo();
                unitInfo = GetAgroInfoById(infoDTO.UnitId, orgId);
                unitInfo.UnitName = infoDTO.UnitName;
                unitInfo.Status = infoDTO.Status;
                unitInfo.UpdateDate = DateTime.Now;
                unitInfo.UpdateUserId = userId;
                _agroUnitInfoRepository.Update(unitInfo);


            }
            IsSuccess = _agroUnitInfoRepository.Save();
            return IsSuccess;
        }

        public AgroUnitInfo GetAgroInfoById(long unitId, long orgId)
        {
            return _agroUnitInfoRepository.GetOneByOrg(r => r.UnitId == unitId && r.OrganizationId == orgId);
        }

        public AgroUnitInfo GetUnitId(string ProductUnit)
        {
            return _agroUnitInfoRepository.GetOneByOrg(r => r.UnitName == ProductUnit);
        }

        
    }
}
