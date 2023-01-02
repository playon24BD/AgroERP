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
        public IEnumerable<StockiestInfoDTO> GetStockiestCodess(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<StockiestInfoDTO>(QueryForStockiestInfossCodes(orgId)).ToList();
        }

        private string QueryForStockiestInfossCodes(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"
           SELECT Top 1 * FROM tblStockiestInfo where OrganizationId=9 order by StockiestId DESC",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<StockiestInfoDTO> GetStockiestInfos(long? stockiestId, long? territoryId, long orgId)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<StockiestInfoDTO>(QueryForStockiestInfoss(stockiestId, territoryId, orgId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForStockiestInfoss(long? stockiestId, long? territoryId, long orgId)
        {
            try
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
           select s.StockiestAddress,a.AreaId,s.StockiestId,t.TerritoryId,t.TerritoryName,s.StockiestName,s.StockiestCode,s.Status,a.AreaName,s.StockiestPhoneNumber,s.StockiestMail,s.StockiestTradeLicense,s.StockiestNID,s.StockiestContactPerson,s.StockiestContactPersonPHNumber,s.CreditLimit
from tblStockiestInfo s
inner join tblTerritoryInfos t on s.TerritoryId=t.TerritoryId
inner join tblAreaSetup a on s.AreaId=a.AreaId
            where 1=1 {0} order by s.StockiestId desc",
                Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveStockiestList(List<StockiestInfoDTO> infoDTO, long userId, long orgId)
        {
            bool isSuccess = false;
            //string StockiestCode = "";
            //var checkStockiestCodeValue = GetStockiestCodess(orgId).FirstOrDefault().StockiestCode;
            //if (checkStockiestCodeValue!="0")
            //{
            //    StockiestCode = "SC-" + checkStockiestCodeValue+ 1;
            //}
            //else
            //{
            //    StockiestCode = "SC-" + "100001";
            //}
            

            List<StockiestInfo> StockiestInfos = new List<StockiestInfo>();

            foreach (var item in infoDTO)
            {
                StockiestInfo stockiest = new StockiestInfo()
                {
                    OrganizationId = orgId,
                    StockiestName = item.StockiestName,
                    TerritoryId = item.TerritoryId,
                    Status = item.Status,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    AreaId=item.AreaId,
                    StockiestPhoneNumber= item.StockiestPhoneNumber,
                    StockiestContactPerson= item.StockiestContactPerson,
                    StockiestContactPersonPHNumber = item.StockiestContactPersonPHNumber,
                    StockiestMail = item.StockiestMail,
                    StockiestNID= item.StockiestNID,
                    StockiestTradeLicense= item.StockiestTradeLicense,
                    CreditLimit= item.CreditLimit,
                    StockiestAddress= item.StockiestAddress,
                    StockiestCode= item.StockiestCode

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
            info.AreaId= updateDTOs.AreaId;
            info.Status= updateDTOs.Status;
            //info.StockiestCode = updateDTOs.StockiestCode;
            info.StockiestPhoneNumber = updateDTOs.StockiestPhoneNumber;
            info.StockiestNID= updateDTOs.StockiestNID;
            info.StockiestMail= updateDTOs.StockiestMail;
            info.CreditLimit= updateDTOs.CreditLimit;
            info.StockiestTradeLicense = updateDTOs.StockiestTradeLicense;
            info.StockiestContactPersonPHNumber = updateDTOs.StockiestContactPersonPHNumber;
            info.StockiestContactPerson = updateDTOs.StockiestContactPerson;
            info.StockiestAddress= updateDTOs.StockiestAddress;
            _stockiestInfoRepository.Update(info);

            isSuccess = _stockiestInfoRepository.Save();
            return isSuccess;
        }
    }
}
