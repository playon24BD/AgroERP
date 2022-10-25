using ERPBLL.Agriculture.Interface;
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
        public List<AgroDropdown> GetIssueRawMaterials(long orgId)
        {
            return this._agricultureUnitOfWork.Db.Database.SqlQuery<AgroDropdown>(string.Format(@"Select DISTINCT RM.RawMaterialName 'text',
RMI.RawMaterialId 'value'
From Agriculture.dbo.tblMRawMaterialIssueStockDetails RMI
Inner Join [Agriculture].dbo.tblRawMaterialInfo RM on RMI.RawMaterialId =RM.RawMaterialId  
Where RMI.OrganizationId={0}", orgId)).ToList();
        }
    }
}
