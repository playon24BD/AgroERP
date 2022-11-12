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
    public class TerritorySetupBusiness : ITerritorySetup
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly TerritorySetupRepository _territorySetupRepository;

        public TerritorySetupBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._territorySetupRepository = new TerritorySetupRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<TerritorySetup> GetAllTerritorySetup(long OrgId)
        {
            return _territorySetupRepository.GetAll(x => x.OrganizationId == OrgId).ToList();
        }

        public IEnumerable<TerritorySetupDTO> GetTerritoryInfos(long orgId, string name, long? territoryId, long? areaId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<TerritorySetupDTO>(QueryForTerritory(orgId, name, territoryId, areaId)).ToList();
        }


        private string QueryForTerritory(long orgId, string name, long? territoryId, long? areaId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and t.OrganizationId={0}", orgId);
            if (territoryId != null && territoryId > 0)
            {
                param += string.Format(@" and t.TerritoryId={0}", territoryId);
            }
            if (areaId != null && areaId > 0)
            {
                param += string.Format(@" and t.AreaId={0}", areaId);
            }
            if (name != null && name != "")
            {
                param += string.Format(@" and t.TerritoryName like '%{0}%'", name);
            }
            query = string.Format(@"
           select t.OrganizationId,t.TerritoryName,a.AreaName,t.Status,t.TerritoryId,t.AreaId,t.OrganizationId from tblTerritoryInfos t
inner join tblAreaSetup a
on t.AreaId = a.AreaId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }



        public TerritorySetup GetTerritoryNamebyId(long territoryId, long orgId)
        {
            return _territorySetupRepository.GetOneByOrg(x => x.TerritoryId == territoryId && x.OrganizationId == orgId);
        }

        public bool SaveTerritoryInfo(List<TerritorySetupDTO> detailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<TerritorySetup> TerritorySetup = new List<TerritorySetup>();

            foreach (var item in detailsDTO)
            {
                if (item.TerritoryId == 0)
                {
                    TerritorySetup territorySetup = new TerritorySetup()
                    {
                        TerritoryId = item.TerritoryId,
                        OrganizationId = orgId,
                        AreaId = item.AreaId,
                        TerritoryName = item.TerritoryName,


                        EntryUserId = userId,
                        UpdateUserId = userId,
                        EntryDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = "Active"

                    };
                    _territorySetupRepository.Insert(territorySetup);
                }


            }

            IsSuccess = _territorySetupRepository.Save();
            return IsSuccess;
        }

        public bool SaveTerritoryInfoEdit(TerritorySetupDTO dTO, long userId, long orgId)
        {
            bool IsSuccess = false;

            TerritorySetup territorySetup = new TerritorySetup();

            territorySetup = GetTerritoryNamebyId(dTO.TerritoryId, orgId);
            territorySetup.TerritoryName = dTO.TerritoryName;
            territorySetup.AreaId = dTO.AreaId;
            territorySetup.Status = dTO.Status;
            territorySetup.UpdateUserId = userId;
            territorySetup.UpdateDate = DateTime.Now;
            territorySetup.OrganizationId = territorySetup.OrganizationId;
            territorySetup.EntryDate = territorySetup.EntryDate;
            territorySetup.EntryUserId = territorySetup.EntryUserId;




            IsSuccess = _territorySetupRepository.Save();


            return IsSuccess;
        }

        public IEnumerable<TerritorySetup> GetAllTerritoryByAreaID(long areaid)
        {
            return _territorySetupRepository.GetAll(x => x.AreaId == areaid).ToList();
        }
    }
}
