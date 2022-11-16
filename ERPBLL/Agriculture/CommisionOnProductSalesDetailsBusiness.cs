using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using System;
using System.Collections.Generic;
using ERPDAL.AgricultureDAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class CommisionOnProductSalesDetailsBusiness : ICommisionOnProductSalesDetailsBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductOnSalesDetailsBusinessRepository _commissionSalesDetailsRepository;
        private readonly ICommissionOnProductBusiness _commissionOnProductBusiness;


        public CommisionOnProductSalesDetailsBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, ICommissionOnProductBusiness commissionOnProductBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductBusiness = commissionOnProductBusiness;
            this._commissionSalesDetailsRepository = new CommissionOnProductOnSalesDetailsBusinessRepository(this._agricultureUnitOfWork);

        }
        public IEnumerable<CommisionOnProductSalesDetails> GetCommisionOnProductSalesDetails(long orgId)
        {
            return _commissionSalesDetailsRepository.GetAll(c => c.OrganizationId == orgId).ToList();
        }

        public CommisionOnProductSalesDetails GetCommisionOnProductSalesDetailsbyId(long commisionOnProductSalesDetailsId, long orgId)
        {
            return _commissionSalesDetailsRepository.GetOneByOrg(d => d.CommissionOnProductSalesDetailsId == commisionOnProductSalesDetailsId && d.OrganizationId == orgId);
        }

        private CommisionOnProductSalesDetails GetCommisionOnProductSalesbyInfoId(long info)
        {
            return _commissionSalesDetailsRepository.GetOneByOrg(d => d.CommissionOnProductSalesDetailsId == info);
        }

        private IEnumerable<CommisionOnProductSalesDetails> GetCommisionOnInfoId(long InfoId, long orgId)
        {
            return _commissionSalesDetailsRepository.GetAll(d => d.CommissionOnProductOnSalesId == InfoId && d.OrganizationId == orgId);
        }

        public bool SaveCommisionOnProductSalesDetails(List<AgroProductSalesDetails> onProductSalesDetailsDTO, long id, string flag, long userId, long orgId)
        {
            bool isSuccess = false;
            List<CommisionOnProductSalesDetails> productSalesDetails = new List<CommisionOnProductSalesDetails>();
            if (onProductSalesDetailsDTO.Count() > 0)
            {
                foreach (var item in onProductSalesDetailsDTO)
                {
                    CommisionOnProductSalesDetails commisionOnProductSalesDetails = new CommisionOnProductSalesDetails()
                    {
                        FinishGoodProductId = item.FinishGoodProductInfoId,

                        CommissionOnProductOnSalesId = id,
                        PaymentMode = flag,


                        Credit = (flag == "Credit") ? _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Credit : 0,

                        Cash = (flag == "Cash") ? _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Cash : 0,
                        //TotalCommission = item.TotalCommission,
                        //Status = item.Status,

                        TotalCommission = ((item.Price * item.Quanity) - item.DiscountTk) * ((flag == "Credit") ? _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Credit : _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Cash) / 100,
                        Remarks = "Insert",
                        EntryUserId = userId,
                        EntryDate = DateTime.Now,
                        //OrganizationId=orgId,this time 0 Processing work
                    };

                    productSalesDetails.Add(commisionOnProductSalesDetails);
                }
                _commissionSalesDetailsRepository.InsertAll(productSalesDetails);
                isSuccess = _commissionSalesDetailsRepository.Save();
            }
            return isSuccess;
        }


        public bool UpdateCommisionOnProductSalesDetails(List<AgroProductSalesDetails> onProductSalesDetailsDTO, long id, string flag, long userId, long orgId)
        {
            bool isSuccess = false;
            flag = "Cash";
            List<CommisionOnProductSalesDetails> productSalesDetails = new List<CommisionOnProductSalesDetails>();

            var salesDetails = this.GetCommisionOnInfoId(id, 0).ToList();
            if (salesDetails.Count() > 0)
            {
                foreach (var did in salesDetails)
                {

                    foreach (var item in onProductSalesDetailsDTO)
                    {
                        var detailsCommission = GetCommisionOnProductSalesbyInfoId(did.CommissionOnProductSalesDetailsId);
                        detailsCommission.Credit = 0;
                        detailsCommission.Cash = (flag == "Cash") ? _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Cash : 0;
                        detailsCommission.TotalCommission = ((item.Price * item.Quanity) - item.DiscountTk) * ((flag == "Credit") ? _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Credit : _commissionOnProductBusiness.GetCommisionOByProductId(item.FinishGoodProductInfoId, orgId).Cash) / 100;
                        detailsCommission.PaymentMode = flag;
                        detailsCommission.Remarks = "Update";
                        detailsCommission.UpdateDate = DateTime.Now;
                        detailsCommission.UpdateUserId = userId;
                        productSalesDetails.Add(detailsCommission);
                    }
                }
                _commissionSalesDetailsRepository.UpdateAll(productSalesDetails);
                isSuccess = _commissionSalesDetailsRepository.Save();
            }
            return isSuccess;
        }
    }
}
