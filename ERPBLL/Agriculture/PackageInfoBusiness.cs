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

        public IEnumerable<PackageInfoDTO> GetPackageListView(string packageCode, long? packageId, string fromDate, string toDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<PackageInfoDTO>(QueryForPackageList(packageCode,packageId,fromDate,toDate)).ToList();
        }

        private string QueryForPackageList(string packageCode, long? packageId, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (packageId != null && packageId > 0)
            {
                param += string.Format(@" and PackageId={0}", packageId);
            }

            if (!string.IsNullOrEmpty(packageCode))
            {
                param += string.Format(@"and PackageCode like '%{0}%'", packageCode);
            }

            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(StartDate as date) between '{0}' and '{1}'", fDate, tDate);
                //param += string.Format(@" and between Cast(StartDate as date) = '{0}' and Cast(EndDate as date) = '{1}'", fDate, tDate);

            }
            if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(EndDate as date) between '{0}' and '{1}'", fDate, tDate);
                

            }

            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(StartDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(EndDate as date)='{0}'", tDate);
            }



            query = string.Format(@"

select PackageId, PackageCode,PackageName,TotalAmount,StartDate,EndDate from tblPackageInfo

 
where 1=1 {0}

", Utility.ParamChecker(param));

            return query;

        }

        public IEnumerable<PackageInfoDTO> GetAllPackageName(long orgId)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<PackageInfoDTO>(QueryForAllPackageName(orgId)).ToList();
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryForAllPackageName(long orgId)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;

                query = string.Format(@"SELECT * FROM tblPackageInfo g
where 1=1 and g.EndDate > GETDATE()	
", Utility.ParamChecker(param));

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
