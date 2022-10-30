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
           select RR.ReturnRawMaterialId,RR.RawMaterialId,RM.RawMaterialName,RR.UnitId,UN.UnitName,RR.ReturnType,RR.EntryDate,RR.EntryUserId,RR.Status,RR.Quantity from tblReturnRawMaterial RR
inner join tblRawMaterialInfo RM
on RR.RawMaterialId=RM.RawMaterialId
inner join tblAgroUnitInfo UN
on RM.UnitId = UN.UnitId
            where 1=1  {0}",
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
    }
}
