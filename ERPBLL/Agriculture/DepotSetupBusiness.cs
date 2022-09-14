using ERPBLL.Agriculture.Interface;
using ERPBLL.ControlPanel.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class DepotSetupBusiness:IDepotSetup
    {
        private readonly IAgricultureUnitOfWork _AgricultureUnitOfWork;
        private readonly DepotSetupRepository _depotSetupRepository;
        
        public DepotSetupBusiness(IAgricultureUnitOfWork AgricultureUnitOfWork)
        {
            this._AgricultureUnitOfWork = AgricultureUnitOfWork;
            this._depotSetupRepository = new DepotSetupRepository(this._AgricultureUnitOfWork);
        }

        public IEnumerable<DepotSetup> GetAllDepotSetup(long OrgId)
        {
            return _depotSetupRepository.GetAll(a=>a.OrganizationId==OrgId);
        }

        public DepotSetup GetDepotNamebyId(long depotId ,long orgId)
        {
            return _depotSetupRepository.GetOneByOrg(a=>a.DepotId==depotId && a.OrganizationId==orgId);
        
        }
        public bool SaveDepotInfo(DepotSetupDTO infoDTO, long orgId, long userId)
        {
            bool IsSuccess = false;
            if (infoDTO.DepotId == 0)
            {
                DepotSetup depot = new DepotSetup()
                {
                    OrganizationId = infoDTO.OrganizationId,
                    DepotName = infoDTO.DepotName,
                    RoleId = infoDTO.RoleId,
                    UpdateDate = DateTime.Now,
                    UpdateUserId = infoDTO.UpdateUserId,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    Status = infoDTO.Status

                };
                _depotSetupRepository.Insert(depot);

            }
            else
            {
                DepotSetup depot = new DepotSetup();
                depot = GetDepotNamebyId(infoDTO.DepotId, orgId);
                depot.DepotName = infoDTO.DepotName;
                depot.OrganizationId = infoDTO.OrganizationId;
                depot.UpdateDate = DateTime.Now;
                depot.UpdateUserId = infoDTO.UpdateUserId;
                depot.Status = infoDTO.Status;
                _depotSetupRepository.Update(depot);
            }

            IsSuccess = _depotSetupRepository.Save();
            return IsSuccess;

            //_depotSetupRepository.Insert(depot);
            //bool saveSuccess = false;
            //saveSuccess = _depotSetupRepository.Save();
            //return saveSuccess;
        }
    }
}
