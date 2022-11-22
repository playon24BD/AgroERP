using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Agriculture.ReportModels;
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
        private readonly FinishGoodProductionDetailsRepository _finishGoodProductionDetailsRepository;
        private readonly MRawMaterialIssueStockDetailsRepository _mRawMaterialIssueStockDetailsRepository;
        private readonly IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness;
        private readonly IMRawMaterialIssueStockInfo _rawMaterialIssueStockInfoBusiness;
        private readonly IMRawMaterialIssueStockDetails _rawMaterialIssueStockDetailsBusiness;
        private readonly IRawMaterialStockInfo _rawMaterialStockInfo;
        private readonly IFinishGoodRecipeInfoBusiness _finishGoodRecipeInfoBusiness;
        public FinishGoodProductionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IMRawMaterialIssueStockInfo rawMaterialIssueStockInfoBusiness, IMRawMaterialIssueStockDetails rawMaterialIssueStockDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionInfoRepository = new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this._finishGoodProductionDetailsRepository = new FinishGoodProductionDetailsRepository(this._agricultureUnitOfWork);
            this._mRawMaterialIssueStockDetailsRepository = new MRawMaterialIssueStockDetailsRepository(this._agricultureUnitOfWork);
            this.finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId,int FGRID, long orgId)
        {
            //return _finishGoodProductionInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.FinishGoodProductId == FinishGoodProductInfoId);

            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductCheckQty(FinishGoodProductInfoId, FGRID, orgId)).ToList();
        }

        private string QueryForFinishGoodProductCheckQty(long finishGoodProductInfoId, int FGRID, long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            param += string.Format(@" and FI.OrganizationId={0}", orgId);
            if (finishGoodProductInfoId>0)
            {
                param += string.Format(@" and FI.FinishGoodProductId={0}", finishGoodProductInfoId);
            }
            
            if (FGRID > 0)
            {
                param += string.Format(@" and FI.FGRId={0}", FGRID);
            }

            query = string.Format(@"

SELECT  DISTINCT FI.FinishGoodProductId,FGPI.FinishGoodProductName,FI.ReceipeBatchCode,SUM(FI.TargetQuantity) AS TargetQuantitys,
ReturnQty=ISNull((SELECT SUM(sr.ReturnQuanity)  as ReturnQuanity FROM  tblSalesReturn sr 
where  sr.Status='ADJUST' and sr.FGRId =FI.FGRId),0),

SelesQty=ISNull((SELECT SUM (sa.Quanity) AS Quantity from tblProductSalesDetails sa
Where sa.FGRId=FI.FGRId and sa.Status is null
),0),

Dropqty=ISNull((SELECT SUM (sa.Quanity) AS Quantity from tblProductSalesDetails sa
Where sa.FGRId=FI.FGRId and sa.Status='Drop'
),0),

TargetQuantity=(SUM(FI.TargetQuantity)-ISNull((SELECT SUM (sa.Quanity) AS Quantity from tblProductSalesDetails sa
Where sa.FGRId=FI.FGRId and sa.Status is null),0)+ISNull((SELECT SUM(sr.ReturnQuanity)  as ReturnQuanity FROM  tblSalesReturn sr 
where  sr.Status='ADJUST' and sr.FGRId =FI.FGRId),0))

FROM FinishGoodProductionInfoes FI
inner join tblFinishGoodRecipeInfo FGR on FGR.FGRId=FI.FGRId
inner join [dbo].[tblFinishGoodProductInfo] FGPI on FGPI.FinishGoodProductId=FGR.FinishGoodProductId 
Where 1=1 and FI.Status='Approved'  {0}
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
                    Status = "Pending",
                    EntryDate = DateTime.Now,
                    EntryUserId = userId,
                    OrganizationId = orgId,
                    FGRId= receiID,


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

                        List<MRawMaterialIssueStockDetailsDTO> issuedetails = new List<MRawMaterialIssueStockDetailsDTO> { new MRawMaterialIssueStockDetailsDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId,RawMaterialId=item.RawMaterialId,Quantity=item.RequiredQuantity, UnitID =rawMaterialStockInfoId.UnitID

                        } };
                        rawMaterialIssueStockDetailsDTOList.AddRange(issuedetails);

                        List<MRawMaterialIssueStockInfoDTO> rawMaterialIssueStockInfos = new List<MRawMaterialIssueStockInfoDTO>() { new MRawMaterialIssueStockInfoDTO { RawMaterialIssueStockId = rawMaterialStockInfoId.RawMaterialIssueStockId, RawMaterialId = item.RawMaterialId, Quantity = item.RequiredQuantity, OrganizationId = orgId } };
                        rawMaterialIssueStockInfoList.AddRange(rawMaterialIssueStockInfos);

                    }



                    isSuccess = _rawMaterialIssueStockDetailsBusiness.SaveRawMaterialIssueDetails(rawMaterialIssueStockDetailsDTOList, userId, orgId, finishGoodProductionBatch);


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
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId and fgp.Status='Approved'),0) ,

SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId and sd.Status is null),0) ,

ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,

DropStock=isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId and sd.Status = 'Drop'),0) ,

CurrentStock=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId and fgp.Status='Approved'),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId and sd.Status is null),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0)

from FinishGoodProductionInfoes fgp
inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId
inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId
inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId
      where 1=1  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<FinishGoodStockReport> GetFinishGoodStockReport(long? productId, string fromDate, string toDate)
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodStockReport>(QueryFinishGoodStockReport(productId, fromDate, toDate));
        }

        private string QueryFinishGoodStockReport(long? productId, string fromDate, string toDate)
        {
            string query = string.Empty;
            string param = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            if (productId != 0 && productId > 0)
            {
                param += string.Format(@" and fgp.FinishGoodProductId={0}", productId);
            }

            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                param += string.Format(@" and Cast(fgp.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
            }



            else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
            {
                string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                FromDate += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
            {
                string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                ToDate += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", tDate);
            }

            query = string.Format(@"select Distinct todate='" + fromDate + "', fromDate='" + toDate + "', fgp.FinishGoodProductId ,fgp.FGRId ,CONCAT(p.FinishGoodProductName,' ', fr.FGRQty,' ( ',un.UnitName,')') as FinishGoodProductName ,convert(date, fgp.EntryDate) as EntryDate,fgp.FinishGoodProductionBatch,fr.ReceipeBatchCode,CONCAT( fr.FGRQty,' ( ',un.UnitName,')') AS ProductDetails,ProductionTotal =isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0) ,SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0) ,ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,CurrentPices=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) from FinishGoodProductionInfoes fgp inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId where 1=1 {0}", Utility.ParamChecker(param));
            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetPendingFinishGoodInfos(string name)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForPendingFinishGood(name)).ToList();

        }
        private string QueryForPendingFinishGood(string name)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (name != null && name != "")
            {
                param += string.Format(@" and p.FinishGoodProductName like '%{0}%'", name);
            }
            query = string.Format(@"
            SELECT a.EntryDate,r.FGRQty,a.FinishGoodProductInfoId,a.FinishGoodProductionBatch,a.ReceipeBatchCode,a.Quanity,
a.TargetQuantity,a.Status,a.Remarks,a.flag,a.OrganizationId,a.FGRId,
U.UnitName,p.FinishGoodProductName 

FROM FinishGoodProductionInfoes a
Inner Join tblFinishGoodProductInfo p on a.FinishGoodProductId=p.FinishGoodProductId

Inner Join tblFinishGoodRecipeInfo r on a.FGRId=r.FGRId
Inner Join tblAgroUnitInfo U on r.UnitId=U.UnitId
 where 1=1 and a.Status='Pending'  {0}",
            Utility.ParamChecker(param));
            return query;
        }

        public FinishGoodProductionInfo GetFGProductionInfoBybatchcode(string finishGoodProductionBatch)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(i => i.FinishGoodProductionBatch == finishGoodProductionBatch);
        }

        public bool UpdateProductionStatus(List<FinishGoodProductionDetailsDTO> finishGoodProductionDetailsDTOs, FinishGoodProductionInfoDTO finishGoodProductionInfoDTO, long userId, long orgId)
        {
            bool IsSuccess = false;

            if(finishGoodProductionInfoDTO.Status == "Approved")
            {
                FinishGoodProductionInfo info = new FinishGoodProductionInfo();
                info = getbatchcodebyid(finishGoodProductionInfoDTO.FinishGoodProductionBatch);
                info.Status= finishGoodProductionInfoDTO.Status;
                info.UpdateDate = DateTime.Now;
                info.UpdateUserId= userId;
                _finishGoodProductionInfoRepository.Update(info);
                
                if (_finishGoodProductionInfoRepository.Save())
                {
                    List<FinishGoodProductionDetails> details = new List<FinishGoodProductionDetails>();
                    FinishGoodProductionDetails details1 = new FinishGoodProductionDetails();
                    foreach (var item in finishGoodProductionDetailsDTOs)
                    {
                        details1 = getdetailsbatchcodebyid(item.FinishGoodProductDetailId);
                        details1.Status = "Consumed";
                        details1.UpdateDate = DateTime.Now;
                        details1.UpdateUserId = userId;
                        details.Add(details1);
                    }
                    _finishGoodProductionDetailsRepository.UpdateAll(details);
                 

                }
                if (_finishGoodProductionDetailsRepository.Save())
                {
                    List<MRawMaterialIssueStockDetails> rawMaterialIssueStockDetails = new List<MRawMaterialIssueStockDetails>();
                    MRawMaterialIssueStockDetails rawMaterialIssueStockDetails1 = new MRawMaterialIssueStockDetails();
                    foreach (var item in finishGoodProductionDetailsDTOs)
                    {
                        rawMaterialIssueStockDetails1 = getissueidbyrmidprobatch(item.FinishGoodProductionBatch, item.RawMaterialId);
                        rawMaterialIssueStockDetails1.IssueStatus = "StockOut";
                        rawMaterialIssueStockDetails.Add(rawMaterialIssueStockDetails1);

                    }
                    _mRawMaterialIssueStockDetailsRepository.UpdateAll(rawMaterialIssueStockDetails);

                }

               

                  IsSuccess = _mRawMaterialIssueStockDetailsRepository.Save();



                return IsSuccess;



            }
            else if (finishGoodProductionInfoDTO.Status == "Rejected")
            {
                FinishGoodProductionInfo info = new FinishGoodProductionInfo();
                info = getbatchcodebyid(finishGoodProductionInfoDTO.FinishGoodProductionBatch);
                info.Status = finishGoodProductionInfoDTO.Status;
                info.UpdateDate = DateTime.Now;
                info.UpdateUserId = userId;
                _finishGoodProductionInfoRepository.Update(info);

                if (_finishGoodProductionInfoRepository.Save())
                {
                    List<FinishGoodProductionDetails> details = new List<FinishGoodProductionDetails>();
                    FinishGoodProductionDetails details1 = new FinishGoodProductionDetails();
                    foreach (var item in finishGoodProductionDetailsDTOs)
                    {
                        details1 = getdetailsbatchcodebyid(item.FinishGoodProductDetailId);
                        details1.Status = "NotConsumed";
                        details1.UpdateDate= DateTime.Now;
                        details1.UpdateUserId= userId;
                        details.Add(details1);
                    }
                    _finishGoodProductionDetailsRepository.UpdateAll(details);


                }
                if (_finishGoodProductionDetailsRepository.Save())
                {
                    List<MRawMaterialIssueStockDetails> rawMaterialIssueStockDetails = new List<MRawMaterialIssueStockDetails>();
                    MRawMaterialIssueStockDetails rawMaterialIssueStockDetails1 = new MRawMaterialIssueStockDetails();
                    foreach (var item in finishGoodProductionDetailsDTOs)
                    {
                        rawMaterialIssueStockDetails1 = getissueidbyrmidprobatch(item.FinishGoodProductionBatch, item.RawMaterialId);
                        rawMaterialIssueStockDetails1.IssueStatus = "StockIn";
                        rawMaterialIssueStockDetails.Add(rawMaterialIssueStockDetails1);

                    }
                    _mRawMaterialIssueStockDetailsRepository.UpdateAll(rawMaterialIssueStockDetails);

                }



                IsSuccess = _mRawMaterialIssueStockDetailsRepository.Save();



                return IsSuccess;



            }
            else
            {
                return IsSuccess;

            }


   
        }


        public FinishGoodProductionInfo getbatchcodebyid(string FinishGoodProductionBatch)
        {
            return _finishGoodProductionInfoRepository.GetOneByOrg(a => a.FinishGoodProductionBatch == FinishGoodProductionBatch);
        }

        public FinishGoodProductionDetails getdetailsbatchcodebyid(long FinishGoodProductDetailId)
        {
            return _finishGoodProductionDetailsRepository.GetOneByOrg(ff => ff.FinishGoodProductDetailId == FinishGoodProductDetailId);
        }

        public MRawMaterialIssueStockDetails getissueidbyrmidprobatch(string FinishGoodProductionBatch, long RawMaterialId)
        {
            return _mRawMaterialIssueStockDetailsRepository.GetOneByOrg(df => df.FinishGoodProductionBatch == FinishGoodProductionBatch && df.RawMaterialId == RawMaterialId);
        }


        public IEnumerable<FinishGoodProductionDataReport> GetFinishGoodProductionReport(string ReceipeBatchCode)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionDataReport>(QueryForFinishGoodProductionReport(ReceipeBatchCode)).ToList();
        }

        public IEnumerable<FinishGoodProductionInfo> GetFinishGoodProductInfosall(long orgId)
        {
            return _finishGoodProductionInfoRepository.GetAll(c => c.OrganizationId == orgId).ToList();
        }

        //        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductInfosList(long? productId, string finishGoodProductionBatch)
        //        {
        //            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QuerySearchForFinishGoodProductInfoss(productId,finishGoodProductionBatch)).ToList();
        //        }


        private string QueryForFinishGoodProductionReport(string ReceipeBatchCode)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(ReceipeBatchCode))
            {
                param += string.Format(@"and fr.ReceipeBatchCode ='{0}'", ReceipeBatchCode);
            }

            query = string.Format(@"
     select Distinct fgp.FinishGoodProductId , fgp.FGRId , p.FinishGoodProductName,fr.ReceipeBatchCode,fr.FGRQty,un.UnitName,

ProductionTotal =isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId and fgp.Status='Approved'),0) ,

SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0) ,

ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr
where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,

CurrentStock=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp
where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId and fgp.Status='Approved'),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd
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

        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProduct(long orgId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetReceiveBatchCode(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForReceiveBatchCode(orgId)).ToList();
        }
        
        private string QueryForReceiveBatchCode(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"SELECT Top 1 * FROM tblFinishGoodProductInfo where OrganizationId=9 order by FinishGoodProductId DESC", Utility.ParamChecker(param));

            return query;
        }
    }
}

