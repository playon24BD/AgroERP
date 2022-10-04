﻿
using ERPBO.Agriculture.DomainModels;
using ERPBO.ControlPanel.DomainModels;
using ERPDAL.ControlPanelDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPDAL.AgricultureDAL
{
    public class ZoneDetailRepository : AgricultureBaseRepository<ZoneDetail>
    {
        public ZoneDetailRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class ZoneRepository : AgricultureBaseRepository<Zone>
    {
        public ZoneRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class RawMaterialStockInfoRepository : AgricultureBaseRepository<RawMaterialStockInfo>
    {
        public RawMaterialStockInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class RawMaterialStockDetailRepository : AgricultureBaseRepository<RawMaterialStockDetail>
    {
        public RawMaterialStockDetailRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class RawMaterialSupplierRepository : AgricultureBaseRepository<RawMaterialSupplier>
    {
        public RawMaterialSupplierRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class BankSetupRepository : AgricultureBaseRepository<BankSetup>
    {
        public BankSetupRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class DepotSetupRepository : AgricultureBaseRepository<DepotSetup>
    {
        public DepotSetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }


    }

    public class RawMaterialRepository : AgricultureBaseRepository<RawMaterial>
    {
        public RawMaterialRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }

    public class FinishGoodProductRepository : AgricultureBaseRepository<FinishGoodProduct>
    {
        public FinishGoodProductRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }
    public class FinishGoodProductSupplierRepository : AgricultureBaseRepository<FinishGoodSupplier>
    {
        public FinishGoodProductSupplierRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }
    //Measurement
    public class MeasurmentRepository:AgricultureBaseRepository<MeasurementSetup>
    {
        public MeasurmentRepository(IAgricultureUnitOfWork agricultureUnitOfWork):base(agricultureUnitOfWork){}

    }

    public class FinishGoodRecipeInfoRepository : AgricultureBaseRepository<FinishGoodRecipeInfo>
    {
        public FinishGoodRecipeInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }
    public class FinishGoodRecipeDetailsRepository : AgricultureBaseRepository<FinishGoodRecipeDetails>
    {
        public FinishGoodRecipeDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }
    public class RawMaterialIssueStockInfoRepository : AgricultureBaseRepository<RawMaterialIssueStockInfo>
    {
        public RawMaterialIssueStockInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }
    public class RawMaterialIssueStockDetailsRepository : AgricultureBaseRepository<RawMaterialIssueStockDetails>
    {
        public RawMaterialIssueStockDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        {

        }

    }


    public class FinishGoodProductionInfoRepository:AgricultureBaseRepository<FinishGoodProductionInfo>
    {
        public  FinishGoodProductionInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) :base(agricultureUnitOfWork )
        { }           
    }

    public class FinishGoodProductionDetailsRepository : AgricultureBaseRepository<FinishGoodProductionDetails>
    {
        public FinishGoodProductionDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        { }
    }

}
