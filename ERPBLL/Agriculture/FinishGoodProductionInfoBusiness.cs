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
        private readonly IRawMaterialTrack _rawMaterialTrack;
        private readonly RawMaterialTrackInfoRepository _rawMaterialTrackInfoRepository;
        public FinishGoodProductionInfoBusiness(IAgricultureUnitOfWork agricultureUnitOfWork,IRawMaterialTrack rawMaterialTrackBusiness ,IFinishGoodProductionDetailsBusiness finishGoodProductionDetailsBusiness, IMRawMaterialIssueStockInfo rawMaterialIssueStockInfoBusiness, IMRawMaterialIssueStockDetails rawMaterialIssueStockDetailsBusiness, IRawMaterialStockInfo rawMaterialStockInfo, IFinishGoodRecipeInfoBusiness finishGoodRecipeInfoBusiness)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._finishGoodProductionInfoRepository = new FinishGoodProductionInfoRepository(this._agricultureUnitOfWork);
            this._finishGoodProductionDetailsRepository = new FinishGoodProductionDetailsRepository(this._agricultureUnitOfWork);
            this._mRawMaterialIssueStockDetailsRepository = new MRawMaterialIssueStockDetailsRepository(this._agricultureUnitOfWork);
            this._rawMaterialTrackInfoRepository = new RawMaterialTrackInfoRepository(this._agricultureUnitOfWork);
            this.finishGoodProductionDetailsBusiness = finishGoodProductionDetailsBusiness;
            this._rawMaterialIssueStockInfoBusiness = rawMaterialIssueStockInfoBusiness;
            this._rawMaterialIssueStockDetailsBusiness = rawMaterialIssueStockDetailsBusiness;
            this._rawMaterialStockInfo = rawMaterialStockInfo;
            this._finishGoodRecipeInfoBusiness = finishGoodRecipeInfoBusiness;
            this._rawMaterialTrack = rawMaterialTrackBusiness;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetCheckFinishGoodQuantity(long FinishGoodProductInfoId,int FGRID, long orgId , long MeasurementId)
        {
            //return _finishGoodProductionInfoRepository.GetOneByOrg(o => o.OrganizationId == orgId && o.FinishGoodProductId == FinishGoodProductInfoId);

            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductCheckQty(FinishGoodProductInfoId, FGRID, orgId, MeasurementId)).ToList();
        }

        private string QueryForFinishGoodProductCheckQty(long finishGoodProductInfoId, int FGRID, long orgId, long MeasurementId)
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
            if (MeasurementId > 0)
            {
                param += string.Format(@" and m.MeasurementId={0}", MeasurementId);
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
inner join tblMeasurement m on m.MeasurementId = FI.MeasurementId
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


            query = string.Format(@"SELECT TOP 1 * FROM FinishGoodProductionInfoes where  OrganizationId=9 ORDER BY FinishGoodProductInfoId DESC", Utility.ParamChecker(param));

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
                    MeasurementId= finishGoodProductionInfoDTO.MeasurementId,
                    MFGQuanity= finishGoodProductionInfoDTO.MFGQuanity


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
                   //List<RawMaterialTrack> rawMaterialTracks = new List<RawMaterialTrack>();
                   RawMaterialTrack rawMaterialTrack = new RawMaterialTrack();
                    foreach(var item in details)
                    {
                        rawMaterialTrack.RawMaterialId = item.RawMaterialId;
                        rawMaterialTrack.Quantity = item.RequiredQuantity;
                        rawMaterialTrack.IssueStatus = "Pending";
                        rawMaterialTrack.EntryDate = DateTime.Now;
                        rawMaterialTrack.EntryUserId = userId;
                        rawMaterialTrack.type = finishGoodProductionBatch;
                        rawMaterialTrack.IssueDate= DateTime.Now;
                        _rawMaterialTrackInfoRepository.Insert(rawMaterialTrack);
                        isSuccess= _rawMaterialTrackInfoRepository.Save();
         
                    }


                }
            }

            return isSuccess;
        }






        public IEnumerable<FinishGoodProductionInfoDTO> FinishgoodproductInOutreturnStockInfos(string ReceipeBatchCode, long? productId)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForFinishGoodProductStock(ReceipeBatchCode, productId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        private string QueryForFinishGoodProductStock(string ReceipeBatchCode, long? productId)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;


                if (ReceipeBatchCode != null && ReceipeBatchCode != "")
                {
                    param += string.Format(@" and fgp.ReceipeBatchCode like '%{0}%'", ReceipeBatchCode);
                }
                if (productId > 0 && productId != 0)
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
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<FinishGoodStockReport> GetFinishGoodStockReport(long? productId, string fromDate, string toDate)
        {
            try
            {
                return _agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodStockReport>(QueryFinishGoodStockReport(productId, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string QueryFinishGoodStockReport(long? productId, string fromDate, string toDate)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;
                string FromDate = string.Empty;
                string ToDate = string.Empty;

                if (productId != 0 && productId > 0)
                {
                    param += string.Format(@" and fgp.FinishGoodProductId={0}", productId);
                }

                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
                }
                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", tDate);
                }

                query = string.Format(@"select Distinct todate='" + fromDate + "', fromDate='" + toDate + "', fgp.FinishGoodProductId ,fgp.FGRId ,CONCAT(p.FinishGoodProductName,' ', fr.FGRQty,' ( ',un.UnitName,')') as FinishGoodProductName ,convert(date, fgp.EntryDate) as EntryDate,fgp.FinishGoodProductionBatch,fr.ReceipeBatchCode,CONCAT( fr.FGRQty,' ( ',un.UnitName,')') AS ProductDetails,ProductionTotal =isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0) ,SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0) ,ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,CurrentPices=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) from FinishGoodProductionInfoes fgp inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId where 1=1 {0}", Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetPendingFinishGoodInfos(string name)
        {
            try
            {
                return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForPendingFinishGood(name)).ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }
        private string QueryForPendingFinishGood(string name)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;


                if (name != null && name != "")
                {
                    param += string.Format(@" and p.FinishGoodProductName like '%{0}%'", name);
                }
                query = string.Format(@"
SELECT m.MeasurementName,a.EntryDate,r.FGRQty,a.FinishGoodProductInfoId,a.FinishGoodProductionBatch,a.ReceipeBatchCode,a.Quanity,
a.TargetQuantity,a.Status,a.Remarks,a.flag,a.OrganizationId,a.FGRId,
U.UnitName,p.FinishGoodProductName 

FROM FinishGoodProductionInfoes a
inner join tblMeasurement m on a.MeasurementId=m.MeasurementId
Inner Join tblFinishGoodProductInfo p on a.FinishGoodProductId=p.FinishGoodProductId

Inner Join tblFinishGoodRecipeInfo r on a.FGRId=r.FGRId
Inner Join tblAgroUnitInfo U on r.UnitId=U.UnitId
 where 1=1 and a.Status='Pending'  {0}",
                Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FinishGoodProductionInfo GetFGProductionInfoBybatchcode(string finishGoodProductionBatch)
        {
            try
            {
                return _finishGoodProductionInfoRepository.GetOneByOrg(i => i.FinishGoodProductionBatch == finishGoodProductionBatch);
            }
            catch (Exception)
            {
                return null;
            }
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
                    List<RawMaterialTrack> rawMaterialTracks = new List<RawMaterialTrack>();
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack();
                    foreach(var item in finishGoodProductionDetailsDTOs)
                    {
                        rawMaterialTrack = getrmtrackidbyrmidprobatch(item.FinishGoodProductionBatch, item.RawMaterialId);
                        rawMaterialTrack.IssueStatus = "StockOut";
                        rawMaterialTracks.Add(rawMaterialTrack);

                    }
                    _rawMaterialTrackInfoRepository.UpdateAll(rawMaterialTracks);

                }

               

                  IsSuccess = _rawMaterialTrackInfoRepository.Save();



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
                    List<RawMaterialTrack> rawMaterialTracks = new List<RawMaterialTrack>();
                    RawMaterialTrack rawMaterialTrack = new RawMaterialTrack();
                    foreach (var item in finishGoodProductionDetailsDTOs)
                    {
                        rawMaterialTrack = getrmtrackidbyrmidprobatch(item.FinishGoodProductionBatch, item.RawMaterialId);
                        rawMaterialTrack.IssueStatus = "None";
                        rawMaterialTracks.Add(rawMaterialTrack);

                    }
                    _rawMaterialTrackInfoRepository.UpdateAll(rawMaterialTracks);

                }



                IsSuccess = _rawMaterialTrackInfoRepository.Save();



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
                param += string.Format(@"and fgp.FinishGoodProductionBatch ='{0}'", ReceipeBatchCode);
            }

            query = string.Format(@"
     
     select Distinct fgp.FinishGoodProductId , fgp.FGRId ,fgp.FinishGoodProductionBatch, p.FinishGoodProductName,fr.ReceipeBatchCode,fgp.TargetQuantity,fr.FGRQty,un.UnitName



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

        public IEnumerable<FinishGoodProductionInfoDTO> GetProductionBatch(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryForProductionBatch(orgId)).ToList();
        }

        private string QueryForProductionBatch(long orgId)
        {
            string query = string.Empty;
            string param = string.Empty;

            query = string.Format(@"	SELECT top 1 * FROM FinishGoodProductionInfoes
where Status='Approved'
 order by FinishGoodProductInfoId desc", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodProductionAcceptDataReport> GetFinishGoodReportAccept(string FinishGoodProductionBatch, string returnDate)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionAcceptDataReport>(QueryForFinishGoodProductionReportDataSave(FinishGoodProductionBatch, returnDate)).ToList();
        }
        private string QueryForFinishGoodProductionReportDataSave(string FinishGoodProductionBatch, string returnDate)
        {
            string query = string.Empty;
            string param = string.Empty;

            if (!string.IsNullOrEmpty(FinishGoodProductionBatch))
            {
                param += string.Format(@"and fgpi.FinishGoodProductionBatch ='{0}'", FinishGoodProductionBatch);
            }

            if (!string.IsNullOrEmpty(returnDate))
            {
                string fDate = Convert.ToDateTime(returnDate).ToString("yyyy-MM-dd");

                param += string.Format(@" and Cast(fgpi.EntryDate as date)= '{0}' ", fDate);
            }

            

            query = string.Format(@"


 

 select distinct convert(date,fgpi.EntryDate)as EntryDate, fgpi.FinishGoodProductionBatch,fgp.FinishGoodProductName,fgpi.Quanity,UnitQuantity=CONVERT( varchar,fgpi.Quanity ) + Convert(varchar,u.UnitName),fgpi.TargetQuantity,fgpi.Status,rm.RawMaterialName,pd.RequiredQuantity from FinishGoodProductionInfoes fgpi
 inner join tblFinishGoodProductInfo fgp on fgpi.FinishGoodProductId=fgp.FinishGoodProductId
 inner join FinishGoodProductionDetails pd on fgpi.FinishGoodProductionBatch=pd.FinishGoodProductionBatch
inner join tblRawMaterialInfo rm on pd.RawMaterialId=rm.RawMaterialId
inner join tblAgroUnitInfo u on rm.UnitId=u.UnitId

--Where fgpi.Status='Approved'



Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodStockReportList(long? productId, string fromDate, string toDate)
        {
            try
            {
                return _agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryFinishGoodStockReportList(productId, fromDate, toDate));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string QueryFinishGoodStockReportList(long? productId, string fromDate, string toDate)
        {
            try
            {
                string query = string.Empty;
                string param = string.Empty;
                string FromDate = string.Empty;
                string ToDate = string.Empty;

                if (productId != 0 && productId > 0)
                {
                    param += string.Format(@" and fgp.FinishGoodProductId={0}", productId);
                }

                if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "" && !string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date) between '{0}' and '{1}'", fDate, tDate);
                }
                else if (!string.IsNullOrEmpty(fromDate) && fromDate.Trim() != "")
                {
                    string fDate = Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", fDate);
                }
                else if (!string.IsNullOrEmpty(toDate) && toDate.Trim() != "")
                {
                    string tDate = Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    param += string.Format(@" and Cast(fgp.EntryDate as date)='{0}'", tDate);
                }

                query = string.Format(@"select Distinct todate='" + fromDate + "', fromDate='" + toDate + "', fgp.FinishGoodProductId ,fgp.FGRId ,CONCAT(p.FinishGoodProductName,' ', fr.FGRQty,' ( ',un.UnitName,')') as FinishGoodProductName ,convert(date, fgp.EntryDate) as EntryDate,fgp.FinishGoodProductionBatch,fr.ReceipeBatchCode,CONCAT( fr.FGRQty,' ( ',un.UnitName,')') AS ProductDetails,ProductionTotal =isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0) ,SalesTotal =isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0) ,ReturnTotal = isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) ,CurrentPices=isnull((select sum(fgp.TargetQuantity) from FinishGoodProductionInfoes fgp where fgp.FinishGoodProductId = p.FinishGoodProductId and fgp.FGRId = fr.FGRId),0)-isnull(( select SUM(sd.Quanity) from tblProductSalesDetails sd where sd.FinishGoodProductInfoId = fgp.FinishGoodProductId and sd.FGRId = fgp.FGRId),0)+isnull(( select SUM(sr.ReturnQuanity) from tblSalesReturn sr where sr.FinishGoodProductInfoId = fgp.FinishGoodProductId and sr.FGRId = fgp.FGRId and sr.Status='ADJUST'),0) from FinishGoodProductionInfoes fgp inner join tblFinishGoodProductInfo p on fgp.FinishGoodProductId = p.FinishGoodProductId inner join tblFinishGoodRecipeInfo fr on fgp.FGRId = fr.FGRId inner join tblAgroUnitInfo un on fr.UnitId = un.UnitId where 1=1 {0}", Utility.ParamChecker(param));
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public RawMaterialTrack getrmtrackidbyrmidprobatch(string FinishGoodProductionBatch, long RawMaterialId)
        {
            return _rawMaterialTrackInfoRepository.GetOneByOrg(df => df.type == FinishGoodProductionBatch && df.RawMaterialId == RawMaterialId);

        }
        public IEnumerable<FinishGoodProductionInfoDTO> GetFinishGoodProductionListView()
        {
            return _agricultureUnitOfWork.Db.Database.SqlQuery<FinishGoodProductionInfoDTO>(QueryFinishGoodStockReportList());
        }

        private string QueryFinishGoodStockReportList()
        {
            string query = string.Empty;
            string param = string.Empty;
           

            

            query = string.Format(@"


select infoes.FinishGoodProductInfoId,infoes.FinishGoodProductionBatch,info.FinishGoodProductName,infoes.Quanity,infoes.EntryDate from FinishGoodProductionInfoes infoes
inner join tblFinishGoodProductInfo info on infoes.FinishGoodProductId=info.FinishGoodProductId


where 1=1 {0}", Utility.ParamChecker(param));
            return query;

        }
    }
}

