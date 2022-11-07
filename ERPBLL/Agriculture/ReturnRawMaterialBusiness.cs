using ERPBLL.Agriculture.Interface;
using ERPBLL.Common;
using ERPBO.Agriculture.DomainModels;
using ERPBO.Agriculture.DTOModels;
using ERPBO.Common;
using ERPDAL.AgricultureDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Agriculture
{
    public class ReturnRawMaterialBusiness : IReturnRawMaterialBusiness
    {
        private readonly IAgricultureUnitOfWork _agricultureUnitOfWork;
        private readonly ReturnRawMaterialRepository _returnRawMaterialRepository;

        private readonly IRawMaterialStockDetail _rawMaterialStockDetail;

        public ReturnRawMaterialBusiness(IAgricultureUnitOfWork agricultureUnitOfWork, IRawMaterialStockDetail rawMaterialStockDetail)
        {
            this._agricultureUnitOfWork = agricultureUnitOfWork;
            this._returnRawMaterialRepository = new ReturnRawMaterialRepository(this._agricultureUnitOfWork);
            this._rawMaterialStockDetail = rawMaterialStockDetail;
        }

        public IEnumerable<ReturnRawMaterial> GetAllReturnRawMaterial()
        {
            return _returnRawMaterialRepository.GetAll().ToList();
        }

        public List<AgroDropdown> GetIssueRawMaterials(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroDropdown>(string.Format(@"SELECT ABC.RawMaterialName 'text',
                ABC.RawMaterialId 'value' FROM (
                SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,
                StockIN=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
                where t.IssueStatus ='StockIn' and t.RawMaterialId=RM.RawMaterialId),
                StockOut=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
                where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId),
                CurrentStock=(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
                where t.IssueStatus ='StockIn'and t.RawMaterialId=RM.RawMaterialId)-(SELECT sum(t.Quantity) FROM  tblMRawMaterialIssueStockDetails t
                where  t.IssueStatus ='StockOut'and t.RawMaterialId=RM.RawMaterialId)
                FROM  
                tblMRawMaterialIssueStockDetails t 
                INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
                inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
                ) ABC where (ABC.CurrentStock>0)")).ToList();
        }



        public IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialInfos(string name)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForReturnRawMaterial(name)).ToList();
        }


        private string QueryForReturnRawMaterial(string name)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (name != null && name != "")
            {
                param += string.Format(@" and RM.RawMaterialName like '%{0}%'", name);
            }
            query = string.Format(@"
           SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,t.Status,
TQuantity=(SELECT sum(t.Quantity) FROM  tblReturnRawMaterial t
where t.ReturnType='Damage' and t.Status='Pending' and t.RawMaterialId=RM.RawMaterialId)

FROM  
tblReturnRawMaterial t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
where 1=1 and  t.ReturnType='Damage' and t.Status='Pending' {0}",
            Utility.ParamChecker(param));
            return query;
        }



        public bool SaveRawMaterialReturnInfo(List<ReturnRawMaterialDTO> detailsDTO, long userId, long orgId)
        {
            bool IsSuccess = false;
            List<ReturnRawMaterial> returnRawMaterials = new List<ReturnRawMaterial>();
            foreach (var item in detailsDTO)
            {
                if (item.ReturnRawMaterialId == 0)
                {
                    ReturnRawMaterial returnRawMaterial = new ReturnRawMaterial()
                    {
                        OrganizationId = orgId,
                        ReturnRawMaterialId = item.ReturnRawMaterialId,
                        Status = "Pending",
                        EntryUserId = userId,
                        EntryDate = DateTime.Now,
                        Quantity = item.Quantity,
                        UnitId = item.UnitId,
                        ReturnType = item.ReturnType,
                        RawMaterialId =item.RawMaterialId

                    };
                    _returnRawMaterialRepository.Insert(returnRawMaterial);
                }
            }
            IsSuccess = _returnRawMaterialRepository.Save();
            return IsSuccess;
            
        }



        public IEnumerable<ReturnRawMaterial> GetReturnRawMaterialBYRMId(long Id, string ReturnType, string Status)
        {
            return _returnRawMaterialRepository.GetAll(j => j.RawMaterialId == Id && j.ReturnType == ReturnType && j.Status == Status).ToList();
        }

        public bool updateReturnStatus(List<ReturnRawMaterialDTO> returnRawMaterialDTOs, long userId, long orgId)
        {
            bool IsSuccess = false;

            List<ReturnRawMaterial> returnRawMaterials = new List<ReturnRawMaterial>();
            ReturnRawMaterial returnRawMaterial = new ReturnRawMaterial();
            foreach (var item in returnRawMaterialDTOs)
            {
                returnRawMaterial = GetReturnsById(item.ReturnRawMaterialId);
                returnRawMaterial.Status = "Approved";
                returnRawMaterials.Add(returnRawMaterial);
            }
            _returnRawMaterialRepository.UpdateAll(returnRawMaterials);
            IsSuccess = _returnRawMaterialRepository.Save();

            //List<FinishGoodRecipeDetails> finishGoodRecipeDetails = new List<FinishGoodRecipeDetails>();
            //FinishGoodRecipeDetails finishGoodRecipeDetail = new FinishGoodRecipeDetails();
            //foreach (var item in finishGoodRecipeDetailDTO)
            //{
            //    finishGoodRecipeDetail = GetFinishGoodRecipeDetailsById(item.FGRDetailsId, orgId);
            //    finishGoodRecipeDetail.FGRRawMaterQty = item.FGRRawMaterQty;
            //    finishGoodRecipeDetail.UpdateDate = DateTime.Now;
            //    finishGoodRecipeDetail.UpUserId = userId;
            //    finishGoodRecipeDetails.Add(finishGoodRecipeDetail);

            //}
            //_finishGoodRecipeDetailsRepository.UpdateAll(finishGoodRecipeDetails);
            //IsSuccess = _finishGoodRecipeDetailsRepository.Save();

            return IsSuccess;

        }

        public ReturnRawMaterial GetReturnsById(long ReturnRawMaterialId)
        {

            return _returnRawMaterialRepository.GetOneByOrg(a => a.ReturnRawMaterialId == ReturnRawMaterialId);

        }

        public IEnumerable<ReturnRawMaterialDTO> GetallReturns(long?
            rawMaterialId, string returnType,string status)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForallReturnRM(rawMaterialId, returnType,status)).ToList();

        }

        private string QueryForallReturnRM(long? rawMaterialId, string returnType,string status)
        {
            string query = string.Empty;
            string param = string.Empty;



            if (rawMaterialId != 0)
            {
                //param += string.Format(@" and rm.RawMaterialName like '%{0}%'", name);
                param += string.Format(@" and rr.RawMaterialId ={0}", rawMaterialId);
            }
            if (returnType != null && returnType != "")
            {
                param += string.Format(@" and rr.ReturnType = '{0}'", returnType);
            }
            if (status != null && status != "")
            {
                param += string.Format(@" and rr.Status like '%{0}%'", status);
            }

            query = string.Format(@"	
 select rr.ReturnRawMaterialId,rr.RawMaterialId,rm.RawMaterialName,rr.Quantity,rr.UnitId,un.UnitName,rr.ReturnType, CONVERT(date,rr.EntryDate) AS EntryDate,rr.Status from tblReturnRawMaterial rr
 inner join tblRawMaterialInfo rm on rr.RawMaterialId = rm.RawMaterialId
 inner join tblAgroUnitInfo un on rr.UnitId = un.UnitId

Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForallReturnRM()).ToList();
        }

        private string QueryForallReturnRM()
        {
            string query = string.Empty;
            string param = string.Empty;


            query = string.Format(@"	
  select distinct rr.ReturnType from tblReturnRawMaterial rr
 inner join tblRawMaterialInfo rm on rr.RawMaterialId = rm.RawMaterialId
 inner join tblAgroUnitInfo un on rr.UnitId = un.UnitId

Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch1()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForallReturnRM1()).ToList();
        }
        
        private string QueryForallReturnRM1()
        {
            string query = string.Empty;
            string param = string.Empty;


            query = string.Format(@"	
  select distinct rm.RawMaterialName from tblReturnRawMaterial rr
 inner join tblRawMaterialInfo rm on rr.RawMaterialId = rm.RawMaterialId
 inner join tblAgroUnitInfo un on rr.UnitId = un.UnitId

Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }

        public IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialSearch2()
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForallReturnRM2()).ToList();
        }

        private string QueryForallReturnRM2()
        {
            string query = string.Empty;
            string param = string.Empty;


            query = string.Format(@"	
  select distinct rr.Status from tblReturnRawMaterial rr
 inner join tblRawMaterialInfo rm on rr.RawMaterialId = rm.RawMaterialId
 inner join tblAgroUnitInfo un on rr.UnitId = un.UnitId

Where 1=1 {0}", Utility.ParamChecker(param));

            return query;
        }


        public IEnumerable<ReturnRawMaterialDTO> GetReturnRawMaterialInfosId(long rawMaterialId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<ReturnRawMaterialDTO>(QueryForReturnRawMaterialee(rawMaterialId)).ToList();
        }


        private string QueryForReturnRawMaterialee(long rawMaterialId)
        {
            string query = string.Empty;
            string param = string.Empty;


            if (rawMaterialId != 0)
            {
                param += string.Format(@" and t.RawMaterialId '{0}'", rawMaterialId);
            }
            query = string.Format(@"
           SELECT Distinct RM.RawMaterialName,t.RawMaterialId,un.UnitName,t.Status,
TQuantity=(SELECT sum(t.Quantity) FROM  tblReturnRawMaterial t
where t.ReturnType='Damage' and t.Status='Pending' and t.RawMaterialId=RM.RawMaterialId)

FROM  
tblReturnRawMaterial t 
INNER JOIN tblRawMaterialInfo RM on t.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo un on RM.UnitId = un.UnitId
where 1=1 and  t.ReturnType='Damage' and t.Status='Pending' {0}",
            Utility.ParamChecker(param));
            return query;
        }

    }
}
