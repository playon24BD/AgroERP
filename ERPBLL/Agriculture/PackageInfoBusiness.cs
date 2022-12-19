using ERPBLL.Agriculture.Interface;

using ERPBO.Agriculture.DomainModels;

using ERPBLL.Common;

using ERPBO.Agriculture.DTOModels;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class PackageInfoBusiness : IPackageInfo
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly PackageInfoRepository _packageInfoRepository;
        private readonly PackageDetailsRepository _packageDetailsRepository;

        public PackageInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._packageInfoRepository = new PackageInfoRepository(this._agricultureUnitOfWork);
            this._packageDetailsRepository = new PackageDetailsRepository(this._agricultureUnitOfWork);
        }

        public bool SaveAgroProductPackageInfo(PackageInfoDTO packageInfoDTO, List<PackageDetailsDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;

            if (packageInfoDTO.PackageName != null && packageInfoDTO.StartDate != null && packageInfoDTO.EndDate != null && packageInfoDTO.TotalAmount != 0)
            {
                var PackageCodeG = "Pac-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");

                PackageInfo packageInfo = new PackageInfo
                {
                    PackageId = packageInfoDTO.PackageId,
                    PackageCode = PackageCodeG,
                    PackageName = packageInfoDTO.PackageName,
                    StartDate = packageInfoDTO.StartDate,
                    EndDate = packageInfoDTO.EndDate,
                    TotalAmount = packageInfoDTO.TotalAmount,
                    Status = "Status",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,

                };
                _packageInfoRepository.Insert(packageInfo);
                isSuccess = _packageInfoRepository.Save();

                List<PackageDetails> detailsList = new List<PackageDetails>();
                foreach (var item in details)
                {
                    PackageDetails packageDetails = new PackageDetails
                    {
                        FinishGoodProductId = item.FinishGoodProductId,
                        MeasurementId = item.MeasurementId,
                        FGRId = item.FGRId,
                        Quanity = item.Quanity,
                        Amount = item.Amount,
                        Status = item.Status,
                        EntryDate = DateTime.Now,
                        EntryUserId = userId,
                        PackageId = packageInfo.PackageId,
                    };
                    _packageDetailsRepository.Insert(packageDetails);
                }
                isSuccess = _packageDetailsRepository.Save();

            }

            return isSuccess;

        }

        public IEnumerable<PackageInfoDTO> GetPackageListView()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PackageInfoDTO>(QueryForPackageList()).ToList();
        }

        private string QueryForPackageList()
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"

select PackageId, PackageCode,PackageName,TotalAmount,StartDate,EndDate from tblPackageInfo

 
where 1=1 {0}

", Utility.ParamChecker(param));

            return query;

        }
    }
}
