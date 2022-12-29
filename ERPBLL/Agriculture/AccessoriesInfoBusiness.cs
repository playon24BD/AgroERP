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

            IsSuccess = _accessoriesInfoRepository.Save();
            return IsSuccess;
        }
    }
}
