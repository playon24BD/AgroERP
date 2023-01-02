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
    public class AccessoriesInfoBusiness : IAccessoriesInfo
    {

        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly AccessoriesInfoRepository _accessoriesInfoRepository;

        public AccessoriesInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._accessoriesInfoRepository = new AccessoriesInfoRepository(this._agricultureUnitOfWork);
        }

        public IEnumerable<AccessoriesInfo> GetAllAccessories()
        {
            return _accessoriesInfoRepository.GetAll().ToList();
        }

        public IEnumerable<AccessoriesInfoDTO> GetAccessoriesList(string accessoriesName)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AccessoriesInfoDTO>(QueryForAccessoriceList(accessoriesName)).ToList();
        }

        private string QueryForAccessoriceList(string accessoriesName)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (!string.IsNullOrEmpty(accessoriesName))
            {
                param += string.Format(@"and AccessoriesName like '%{0}%'", accessoriesName);
            }

            query = string.Format(@"
select AccessoriesId,AccessoriesName,status from tblAccessoriesInfo
where 1=1 {0} order by AccessoriesId desc",
            Utility.ParamChecker(param));
            return query;
        }


        public bool SaveAccessoriesInfo(AccessoriesInfoDTO accessoriesInfoDTO, long userId)
        {
            bool IsSuccess = false;
            if (accessoriesInfoDTO.AccessoriesId == 0)
            {
                AccessoriesInfo accessoriesInfo = new AccessoriesInfo()
                {
                    AccessoriesName = accessoriesInfoDTO.AccessoriesName,
                    Status= accessoriesInfoDTO.Status,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId

                };

                _accessoriesInfoRepository.Insert(accessoriesInfo);
            }

            else
            {
                AccessoriesInfo accessories = new AccessoriesInfo();
                accessories = GetAccessoriesNamebyId(accessoriesInfoDTO.AccessoriesId);
                accessories.AccessoriesName = accessoriesInfoDTO.AccessoriesName;
                accessories.Status = accessoriesInfoDTO.Status;
                _accessoriesInfoRepository.Update(accessories);

            }


            IsSuccess = _accessoriesInfoRepository.Save();
            return IsSuccess;
        }

        public AccessoriesInfo GetAccessoriesNamebyId(long accessoriesId)
        {
            return _accessoriesInfoRepository.GetOneByOrg(a => a.AccessoriesId == accessoriesId);
        }

        public IEnumerable<AccessoriesInfo> GetAccessoriesInfoall()
        {
            return _accessoriesInfoRepository.GetAll().ToList();
        }
    }
}
