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
    public class FinishGoodProductionInfoBusiness : IFinishGoodProductionInfoBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly FinishGoodProductionInfoRepository _finishGoodProductionInfoRepository;
        private readonly IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness;
        private readonly IMRawMaterialIssueStockInfo _rawMaterialIssueStockInfoBusiness;
        private readonly IMRawMaterialIssueStockDetails _rawMaterialIssueStockDetailsBusiness;
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        private readonly IFinishGoodRecipeInfoBusiness _finishGoodRecipeInfoBusiness;
        public FinishGoodProductionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IMRawMaterialIssueStockInfo rawMaterialIssueStockInfoBusiness, IMRawMaterialIssueStockDetails rawMaterialIssueStockDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionInfoRepository = new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this.finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId,string ProductUnitQty,long? CheckQty, long orgId)
        {
            //return _finishGoodProductionInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.FinishGoodProductId == FinishGoodProductInfoId);

            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductCheckQty(FinishGoodProductInfoId, ProductUnitQty, CheckQty, orgId)).ToList();
        }

        private string QueryForFinishGoodProductCheckQty(long finishGoodProductInfoId,string ProductUnitQty, long? CheckQty, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and FI.OrganizationId={0}", orgId);
            if (finishGoodProductInfoId>0)
            {
                param += string.Format(@" and FI.FinishGoodProductId={0}", finishGoodProductInfoId);
            }
            if (ProductUnitQty !=null)
            {
                param += string.Format(@" and FI.Quanity={0}", ProductUnitQty);
            }
            if (CheckQty >0)
            {
                param += string.Format(@" and FI.FGRId={0}", CheckQty);
            }

            query = string.Format(@"

SELECT  DISTINCT FI.FinishGoodProductId,FGPI.FinishGoodProductName,FI.ReceipeBatchCode,SUM(FI.TargetQuantity) AS TargetQuantitys,
ReturnQty=ISNull((SELECT SUM(sr.ReturnQuanity)  as ReturnQuanity FROM  tblSalesReturn sr 
where  sr.Status='ADJUST' and sr.FGRId =FI.FGRId),0),

SelesQty=ISNull((SELECT SUM (sa.Quanity) AS Quantity from tblProductSalesDetails sa
Where sa.FGRId=FI.FGRId
),0),
TargetQuantity=(SUM(FI.TargetQuantity)-ISNull((SELECT SUM (sa.Quanity) AS Quantity from tblProductSalesDetails sa
Where sa.FGRId=FI.FGRId),0)+ISNull((SELECT SUM(sr.ReturnQuanity)  as ReturnQuanity FROM  tblSalesReturn sr 
where  sr.Status='ADJUST' and sr.FGRId =FI.FGRId),0))

FROM FinishGoodProductionInfoes FI
inner join tblFinishGoodRecipeInfo FGR on FGR.FGRId=FI.FGRId
inner join [dbo].[tblFinishGoodProductInfo] FGPI on FGPI.FinishGoodProductId=FGR.FinishGoodProductId 
Where 1=1 {0}
Group by 
FI.FGRId,FGPI.FinishGoodProductName,FI.FinishGoodProductId,FGPI.FinishGoodProductName,FI.ReceipeBatchCode", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfos(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductInfoss(orgId)).ToList();
        }

        private string QueryForFinishGoodProductInfoss(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and FI.OrganizationId={0}", orgId);


            query = string.Format(@"SELECT DISTINCT FI.FinishGoodProductId,FP.FinishGoodProductName,FI.ReceipeBatchCode,SUM(FI.TargetQuantity) AS TargetQuantity

FROM FinishGoodProductionInfoes FI
INNER JOIN tblFinishGoodProductInfo FP on FI.FinishGoodProductId=FP.FinishGoodProductId
INNER JOIN tblFinishGoodRecipeInfo RI on FI.ReceipeBatchCode=RI.ReceipeBatchCode	
Where 1=1 and FI.ReceipeBatchCode=RI.ReceipeBatchCode  and FI.OrganizationId=9 Group by FP.FinishGoodProductName,FI.ReceipeBatchCode,FI.FinishGoodProductId", Utility.ParamChecker(param));

            return query;
        }


        public FinishGoodProductionInfo GetFinishGoodProductionByAny(string any, long orgId)
        {
            throw new NotImplementedException();
        }

        public FinishGoodProductionInfo Getcheckqty(long FinishGoodProductInfoId, string ProductUnitQty)
        {
            var puq = ProductUnitQty.Split('"');
            var qty = puq[0];
            double proqty =Convert.ToDouble( qty);

            return _finishGoodProductionInfoRepository.GetOneByOrg(f => f.FinishGoodProductId == FinishGoodProductInfoId && f.Quanity== proqty);
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductionInfo(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductInfossGetAll(orgId)).ToList();
            // return _finishGoodProductionInfoRepository.GetAll(a => a.OrganizationId == orgId);
        }

        private string QueryForFinishGoodProductInfossGetAll(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and a.OrganizationId={0}", orgId);


            query = string.Format(@"SELECT a.FinishGoodProductInfoId,a.FinishGoodProductionBatch,a.ReceipeBatchCode,a.Quanity,
a.TargetQuantity,a.Status,a.Remarks,a.flag,a.OrganizationId,a.FGRId,
U.UnitName,p.FinishGoodProductName 

FROM FinishGoodProductionInfoes a
Inner Join tblFinishGoodProductInfo p on a.FinishGoodProductId=p.FinishGoodProductId

Inner Join tblFinishGoodRecipeInfo r on a.FGRId=r.FGRId
Inner Join tblAgroUnitInfo U on r.UnitId=U.UnitId", Utility.ParamChecker(param));

            return query;
        }

        public FinishGoodProductionInfo GetProductionInfoById(long id, long orgId)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(f => f.FinishGoodProductInfoId == id);
        }

        public bool SaveFinishGoodInfo(FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, List<FinishGoodProductionDetailsDTO> details, long userId, long orgId)
        {
            bool isSuccess = false;

            var finishGoodProductionBatch = "Pro-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            var receiID= _finishGoodRecipeInfoBusiness.GetReceipbachcodeid(finishGoodProductionInfoDTO.ReceipeBatchCode).FGRId;

            if (finishGoodProductionInfoDTO.FinishGoodProductionInfoId == 0)
            {
                FinishGoodProductionInfo finishGoodProductionInfo = new FinishGoodProductionInfo
                {
                    FinishGoodProductId = finishGoodProductionInfoDTO.FinishGoodProductId,
                    ReceipeBatchCode = finishGoodProductionInfoDTO.ReceipeBatchCode,
                    FinishGoodProductionBatch = finishGoodProductionBatch,
                    Quanity = finishGoodProductionInfoDTO.Quanity,
                    TargetQuantity = finishGoodProductionInfoDTO.TargetQuantity,
                    Remarks = finishGoodProductionInfoDTO.Remarks,
                    Status = finishGoodProductionInfoDTO.Status,
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                    FGRId= receiID

                };
                _finishGoodProductionInfoRepository.Insert(finishGoodProductionInfo);

            }
            isSuccess = _finishGoodProductionInfoRepository.Save();
            if (isSuccess)
            {

                isSuccess = finishGoodProductionDetailsBusiness.SaveFinishGoodDetails(details, finishGoodProductionBatch, userId, orgId);

            }
            if (isSuccess)
            {
                //AbNormal
                if (details.Count() > 0)
                {

                    List<MRawMaterialIssueStockDetailsDTO> rawMaterialIssueStockDetailsDTOList = new List<MRawMaterialIssueStockDetailsDTO>();
                    List<MRawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfoList = new List<MRawMaterialIssueStockInfoDTO>();

                    foreach (var item in details)
                    {
                        //Test!

                        var rawMaterialStockInfoId = _rawMaterialIssueStockDetailsBusiness.GetRawMaterialIssueStockByMeterialId(item.RawMaterialId, orgId);

                        var checkRawMaterialStockValue = _rawMaterialStockInfo.GetCheckRawmeterislQuantity(item.RawMaterialId, orgId);
                        //var rawMaterialStockInfoId = _rawMaterialIssueStockInfoBusiness.GetRawMaterialIssueStockByMeterialId(item.RawMaterialId, orgId);

                        List<MRawMaterialIssueStockDetailsDTO> issuedetails = new List<MRawMaterialIssueStockDetailsDTO> { new MRawMaterialIssueStockDetailsDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId,RawMaterialId=item.RawMaterialId,Quantity=item.RequiredQuantity, UnitID =rawMaterialStockInfoId.UnitID

                        } };
                        rawMaterialIssueStockDetailsDTOList.AddRange(issuedetails);

                        List<MRawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfos = new List<MRawMaterialIssueStockInfoDTO>() { new MRawMaterialIssueStockInfoDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId, RawMaterialId = item.RawMaterialId, Quantity = item.RequiredQuantity, OrganizationId = orgId } };
                        rawMaterialIssueStockInfoList.AddRange(rawMaterialIssueStockInfos);

                    }


                    isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveRawMaterialIssueDetails(rawMaterialIssueStockDetailsDTOList, userId, orgId);

                    //if (isSuccess)
                    //{

                    //    isSuccess = _rawMaterialIssueStockInfoBusiness.UpdateProductIssueRawMaterialStock(rawMaterialIssueStockInfoList);

                    //}

                }
            }

            return isSuccess;
        }






        public IEnumerable<FinishGoodProductionInfoDTO> FinishgoodproductInOutreturnStockInfos(string ReceipeBatchCode, long? productId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductStock(ReceipeBatchCode, productId)).ToList();
        }


        private string QueryForFinishGoodProductStock(string ReceipeBatchCode, long? productId)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (ReceipeBatchCode != null && ReceipeBatchCode != "")
            {
                param += string.Format(@" and fgp.ReceipeBatchCode like '%{0}%'", ReceipeBatchCode);
            }
            if (productId>0 && productId != 0)
            {
                param += string.Format(@" and fgp.FinishGoodProductId ={0}", productId);
            }
            query = string.Format(@"
     select Distinct fgp.FinishGoodProductId , fgp.FGRId , p.FinishGoodProductName,fr.ReceipeBatchCode,fr.FGRQty,un.UnitName,

ProductionTotal =isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0) ,

SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0) ,

ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,

CurrentStock=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) 

from FinishGoodProductionInfoes fgp
inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId
inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId
inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId
      where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }


//        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfosList(long? productId, string finishGoodProductionBatch)
//        {
//            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QuerySearchForFinishGoodProductInfoss(productId,finishGoodProductionBatch)).ToList();
//        }

//        private string QuerySearchForFinishGoodProductInfoss(long? productId, string finishGoodProductionBatch)
//        {
//            string query = string.Empty;
//            string param = string.Empty;

//            //param += string.Format(@" and FI.OrganizationId={0}", orgId);

//            if (productId != null && productId > 0)
//            {
//                param += string.Format(@" and s.FinishGoodProductId={0}", productId);
//            }
//            if (!string.IsNullOrEmpty(finishGoodProductionBatch))
//            {
//                param += string.Format(@"and s.FinishGoodProductionBatch like '%{0}%'", finishGoodProductionBatch);
//            }

//            query = string.Format(@"
//--select distinct s.FinishGoodProductId, o.FinishGoodProductName,
//--s.FinishGoodProductionBatch,s.Quanity,s.TargetQuantity
//--from FinishGoodProductionInfoes s
//--inner join tblFinishGoodProductInfo o on s.FinishGoodProductId=o.FinishGoodProductId
//select s.FinishGoodProductId, o.FinishGoodProductName, s.FinishGoodProductionBatch,r.ReceipeBatchCode,a.UnitName,s.Quanity,s.TargetQuantity,s.Status
//from FinishGoodProductionInfoes s
//inner join tblFinishGoodProductInfo o on s.FinishGoodProductId=o.FinishGoodProductId
//inner join tblFinishGoodRecipeInfo r on r.FGRId=s.FGRId
//inner join tblAgroUnitInfo a on r.UnitId=a.UnitId

//where 1=1 {0}", Utility.ParamChecker(param));

//            return query;
//        }

    }
}

