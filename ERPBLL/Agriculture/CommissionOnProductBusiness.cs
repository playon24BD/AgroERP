using ERPBLL.Agriculture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPDAL.AgricultureDAL;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBLL.Common;

namespace ERPBLL.Agriculture
{
    public class CommissionOnProductBusiness : ICommissionOnProductBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly CommissionOnProductBusinessRepository _commissionOnProductBusinessRepository;
        private readonly ICommissionOnProductHistoryBusiness _commissionOnProductHistoryBusiness;
        public CommissionOnProductBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, ICommissionOnProductHistoryBusiness commissionOnProductHistoryBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._commissionOnProductBusinessRepository = new CommissionOnProductBusinessRepository(this._agricultureUnitOfWork);
            this._commissionOnProductHistoryBusiness = commissionOnProductHistoryBusiness;
        }

        public CommisionOnProduct GetCommisionOnProductbyId(long commissionOnProductId, long orgId)
        {
            return _commissionOnProductBusinessRepository.GetOneByOrg(c => c.CommissionOnProductId == commissionOnProductId && c.OrganizationId == orgId);
        }

        public IEnumerable<CommisionOnProduct> GetCommisionOnProducts(long orgId)
        {
            return _commissionOnProductBusinessRepository.GetAll(c => c.OrganizationId == orgId).ToList();
        }

        public bool IsExistsSameYearProduct(int year, long product,long orgId)
        {
            bool IsExist = false;

          var listofSameYearProduct=  _commissionOnProductBusinessRepository.GetAll(c =>c.CalenderYear==year && c.FinishGoodProductId==product && c.OrganizationId == orgId).ToList();

            if (listofSameYearProduct.Count()>0)
            {
                IsExist = true;
            }
            return IsExist;
        }

        public IEnumerable<CommisionOnProductDTO> GetAllCommisionOnProducts(long? product, int? year, long orgId)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<CommisionOnProductDTO>(string.Format(QueryForGellAllCommissionProduct(product,year,orgId)));
        }



        private string QueryForGellAllCommissionProduct(long? product, int? year, long orgId)
        {
            string param = string.Empty;
            string query = string.Empty;

            if (orgId>0)
            {
                param += string.Format(@"and cp.OrganizationId={0}", orgId);

            }
            if (product>0)
            {
                param += string.Format(@"and cp.FinishGoodProductId={0}", product);
            }
            if (year>0)
            {
                param += string.Format(@"and cp.CalenderYear={0}", year);
            }

            query = string.Format(@"SELECT cp.CommissionOnProductId,cp.FinishGoodProductId,cp.CalenderYear,cp.Credit,cp.Cash,cp.Status,cp.EntryDate,cp.EntryUserId,cp.Remarks,cp.UpdateUserId,cp.OrganizationId,cp.StartDate,cp.EndDate, fp.FinishGoodProductName
              FROM [dbo].[tblCommissionOnProduct] cp
              Inner Join tblFinishGoodProductInfo fp
              on cp.FinishGoodProductId=fp.FinishGoodProductId  where 1=1  {0}",
         Utility.ParamChecker(param));

            return query;
        }

        public bool SaveCommisionOnProductby(List<CommisionOnProductDTO> commisionOnProductDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;

            List<CommisionOnProduct> commisionOnProducts = new List<CommisionOnProduct>();
            List<CommisionOnProductHistoryDTO> commisionOnProductHistories = new List<CommisionOnProductHistoryDTO>();
            var action = "";

            if (commisionOnProductDTOs.Count() > 0)
            {
                foreach (var item in commisionOnProductDTOs)
                {
                    if (item.CommissionOnProductId == 0)
                    {
                        CommisionOnProduct commisionOnProduct = new CommisionOnProduct
                        {

                            FGRId = item.FGRId,
                            FinishGoodProductId = item.FinishGoodProductId,
                            CalenderYear = item.CalenderYear,
                            Cash = item.Cash,
                            Credit = item.Credit,
                            StartDate=item.StartDate,
                            EndDate=item.EndDate,
                            Status = "Active",
                            Remarks=item.Remarks,
                            EntryDate = DateTime.Now,
                            EntryUserId = userId,
                            OrganizationId = orgId,

                        };



                        commisionOnProducts.Add(commisionOnProduct);
                        action = "Insert";
                    }
                    else
                    {
                        var commission = this.GetCommisionOnProductbyId(item.CommissionOnProductId, orgId);

                        commission.FGRId = item.FGRId;
                        commission.FinishGoodProductId = item.FinishGoodProductId;
                        commission.CalenderYear = item.CalenderYear;
                        commission.Cash = item.Cash;
                        commission.Credit = item.Credit;
                        commission.StartDate = item.StartDate;
                        commission.EndDate = item.EndDate;
                        commission.Status = item.Status;
                        commission.Remarks= item.Remarks;

                        commission.UpdateDate = DateTime.Now;
                        commission.UpdateUserId = userId;
                        action = "Update";


                        if (commisionOnProductDTOs.Count() > 0)
                        {

                                CommisionOnProductHistoryDTO commisionOnProductHistory = new CommisionOnProductHistoryDTO
                                {

                                    FGRId = item.FGRId,
                                    FinishGoodProductId = item.FinishGoodProductId,
                                    CommissionOnProductId=item.CommissionOnProductId,
                                    CalenderYear = item.CalenderYear,
                                    Cash = item.Cash,
                                    Credit = item.Credit,
                                    StartDate=item.StartDate,
                                    EndDate=item.EndDate,
                                    Remarks = item.Remarks,
                                    EntryDate = DateTime.Now,
                                    EntryUserId = userId,
                                    OrganizationId = orgId,

                                };
                                commisionOnProductHistories.Add(commisionOnProductHistory);
  
                        }


                    }


                }

            }
            if (action == "Insert")
            {
                _commissionOnProductBusinessRepository.InsertAll(commisionOnProducts);
            }
            else
            {
                _commissionOnProductBusinessRepository.UpdateAll(commisionOnProducts);
            }

            IsSuccess = _commissionOnProductBusinessRepository.Save();

            if (IsSuccess)
            {
                if (action=="Insert")
                {
                    IsSuccess = _commissionOnProductHistoryBusiness.SaveCommissionOnProductHistoryWhenInsert(commisionOnProducts, userId, orgId);
                }
                else
                {
                    IsSuccess = _commissionOnProductHistoryBusiness.SaveCommissionOnProductHistory(commisionOnProductHistories, userId, orgId);
                }
            }

            return IsSuccess;
        }
    }
}
