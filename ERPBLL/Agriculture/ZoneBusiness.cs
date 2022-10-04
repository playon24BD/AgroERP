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
   public class ZoneBusiness:IZone
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ZoneRepository _zoneRepository;

        public ZoneBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._zoneRepository = new ZoneRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<ZoneDTO> GetZoneInfos(long orgId, long? ZoneId)
        {
            //return null;
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ZoneDTO>(QueryForZoneInfoss(orgId, ZoneId)).ToList();
        }

        private string QueryForZoneInfoss(long orgId, long? ZoneId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and OrganizationId={0}", orgId);
            if (ZoneId != null && ZoneId > 0)
            {
                param += string.Format(@" and ZoneId={0}", ZoneId);
            }
            query = string.Format(@"
            select ZoneName,DivisionId from tblZoneInfo
            where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public bool SaveZoneInfo(ZoneDTO zoneInfo, List<ZoneDetailDTO> zoneDetailDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;

            if (zoneInfo.ZoneId==0)
            {
                Zone infoList = new Zone()
                {
      
                    ZoneName= zoneInfo.ZoneName,
                    DivisionId=zoneInfo.DivisionId,
                    EntryDate=DateTime.Now,
                    EntryUserId=userId,
                    Status = "Active",
                    Remarks = zoneInfo.Remarks,
                    OrganizationId = orgId
                };
                List<ZoneDetail> zoneDetails = new List<ZoneDetail>();
                foreach(var item in zoneDetailDTOs)
                {
                    ZoneDetail zoneListDetail = new ZoneDetail()
                    {
                        DivisionId = item.DivisionId,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        Status="Active",
                        Remarks=item.Remarks,
                        OrganizationId=orgId
                    };

                    zoneDetails.Add(zoneListDetail);
                }

                infoList.ZoneDetailsTable = zoneDetails;
                _zoneRepository.Insert(infoList);
                IsSuccess = _zoneRepository.Save();
            }

            //List<Zone> zoneInfo = new List<Zone>();
            //if (zoneDetailDTOs.Count() > 0)
            //{
                //foreach (var z in zoneDetailDTOs)
                //{

                //    ZoneDetail zoneDetails = new ZoneDetail()
                //    {
                //        ZoneId=z.ZoneId,
                //        DivisionId = z.DivisionId,
                //        EntryDate = DateTime.Now,
                //        EntryUserId = userId



                //    };

                //    _zoneRepository.Insert(zoneDetails);
                //    IsSuccess = _zoneRepository.Save();

                //}

            //}


            return IsSuccess;
        }
    }
}
