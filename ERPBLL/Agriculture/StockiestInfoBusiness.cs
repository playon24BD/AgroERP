﻿using ERPBLL.Agriculture.Interface;
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
    public class StockiestInfoBusiness:IStockiestInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly StockiestInfoRepository _stockiestInfoRepository;
        public StockiestInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._stockiestInfoRepository = new StockiestInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<StockiestInfo> GetAllStockiestSetup(long OrgId)
        {
            return _stockiestInfoRepository.GetAll(o => o.OrganizationId == OrgId).ToList();
        }

        public StockiestInfo GetStockiestInfoById(long stockiestId, long orgId)
        {
            return _stockiestInfoRepository.GetOneByOrg(r => r.StockiestId == stockiestId && r.OrganizationId == orgId);
        }

        public IEnumerable<StockiestInfoDTO> GetStockiestInfos(long? stockiestId, long? territoryId, long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<StockiestInfoDTO>(QueryForStockiestInfoss(stockiestId, territoryId, orgId)).ToList();
        }

        private string QueryForStockiestInfoss(long? stockiestId, long? territoryId, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and s.OrganizationId={0}", orgId);
            if (stockiestId != null && stockiestId > 0)
            {
                param += string.Format(@" and s.StockiestId={0}", stockiestId);
            }
            if (territoryId != null && territoryId > 0)
            {
                param += string.Format(@" and t.TerritoryId={0}", territoryId);
            }

            query = string.Format(@"
           select s.StockiestId,t.TerritoryId,t.TerritoryName,s.StockiestName,s.Status
from tblStockiestInfo s
inner join tblTerritoryInfos t on s.TerritoryId=t.TerritoryId
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveStockiestList(List<StockiestInfoDTO> infoDTO, long userId, long orgId)
        {
            bool isSuccess = false;

            List<StockiestInfo> StockiestInfos = new List<StockiestInfo>();

            foreach (var item in infoDTO)
            {
                StockiestInfo stockiest = new StockiestInfo()
                {
                    OrganizationId = orgId,
                    StockiestName = item.StockiestName,
                    TerritoryId = item.TerritoryId,
                    Status = "Active",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                };
                _stockiestInfoRepository.Insert(stockiest);
            }

            isSuccess = _stockiestInfoRepository.Save();
            return isSuccess;
        }

        public bool UpdateStockiestList(StockiestInfoDTO updateDTOs, long userId, long orgId)
        {
            bool isSuccess = false;

            StockiestInfo info = new StockiestInfo();
            info = GetStockiestInfoById(updateDTOs.StockiestId, orgId);
            info.StockiestName = updateDTOs.StockiestName;
            info.TerritoryId = updateDTOs.TerritoryId;
            info.Status = updateDTOs.Status;
            info.UpdateDate = DateTime.Now;
            info.UpdateUserId = userId;
            _stockiestInfoRepository.Update(info);

            isSuccess = _stockiestInfoRepository.Save();
            return isSuccess;
        }
    }
}