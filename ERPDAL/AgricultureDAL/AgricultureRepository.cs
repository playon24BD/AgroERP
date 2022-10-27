
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


    public class SalesPaymentRegisterRepository : AgricultureBaseRepository<SalesPaymentRegister>
    {
        public SalesPaymentRegisterRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }

    }
    //e
    public class ReturnRawMaterialRepository : AgricultureBaseRepository<ReturnRawMaterial>
    {
        public ReturnRawMaterialRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }

    }
    //e
    public class RawMaterialTrackInfoRepository : AgricultureBaseRepository<RawMaterialTrack>
    {
        public RawMaterialTrackInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    //e
    public class MRawMaterialIssueStockInfoRepository : AgricultureBaseRepository<MRawMaterialIssueStockInfo>
    {
        public MRawMaterialIssueStockInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    //e
    public class MRawMaterialIssueStockDetailsRepository : AgricultureBaseRepository<MRawMaterialIssueStockDetails>
    {
        public MRawMaterialIssueStockDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }


    //e
    public class PRawMaterialStockInfoRepository : AgricultureBaseRepository<PRawMaterialStockInfo>
    {
        public PRawMaterialStockInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    //e
    public class PRawMaterialStockIDetailsRepository : AgricultureBaseRepository<PRawMaterialStockIDetails>
    {
        public PRawMaterialStockIDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }



    public class AgroUnitInfoRepository : AgricultureBaseRepository<AgroUnitInfo>
    {
        public AgroUnitInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class UserInfoRepository : AgricultureBaseRepository<UserInfo>
    {
        public UserInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class StockiestInfoRepository : AgricultureBaseRepository<StockiestInfo>
    {
        public StockiestInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class AreaSetupInfoRepository : AgricultureBaseRepository<AreaInfoSetup>
    {
        public AreaSetupInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class DivisionInfoRepository : AgricultureBaseRepository<DivisionInfo>
    {
        public DivisionInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
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

    //e
    public class ZoneSetupRepository : AgricultureBaseRepository<ZoneSetup>
    {
        public ZoneSetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }
    }

    //e
    public class RegionSetupRepository : AgricultureBaseRepository<RegionSetup>
    {
        public RegionSetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }
    }
    //e
    public class TerritorySetupRepository : AgricultureBaseRepository<TerritorySetup>
    {
        public TerritorySetupRepository(IAgricultureUnitOfWork AgricultureUnitOfWork) : base(AgricultureUnitOfWork) { }
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
    public class UserAssignRepository : AgricultureBaseRepository<UserAssign>
    {
        public UserAssignRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork)
        { }
    }

    public class AgroProductSalesInfoRepository : AgricultureBaseRepository<AgroProductSalesInfo>
    {
        public AgroProductSalesInfoRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class AgroProductSalesDetailsRepository : AgricultureBaseRepository<AgroProductSalesDetails>
    {
        public AgroProductSalesDetailsRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class DistributionUserBusinessRepository : AgricultureBaseRepository<DistributionUser>
    {
        public DistributionUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class DivisionUserBusinessRepository : AgricultureBaseRepository<DivisionUser>
    {
        public DivisionUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class ZoneUserBusinessRepository : AgricultureBaseRepository<ZoneUser>
    {
        public ZoneUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class RegionUserBusinessRepository : AgricultureBaseRepository<RegionUser>
    {
        public RegionUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class AreaUserBusinessRepository : AgricultureBaseRepository<AreaUser>
    {
        public AreaUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class TerritoryUserBusinessRepository : AgricultureBaseRepository<TerritoryUser>
    {
        public TerritoryUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class StockiestUserBusinessRepository : AgricultureBaseRepository<StockiestUser>
    {
        public StockiestUserBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }

    public class RawMaterialRequisitionInfoBusinessRepository : AgricultureBaseRepository<RawMaterialRequisitionInfo>
    {
        public RawMaterialRequisitionInfoBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
    public class RawMaterialRequisitionDetailsBusinessRepository : AgricultureBaseRepository<RawMaterialRequisitionDetails>
    {
        public RawMaterialRequisitionDetailsBusinessRepository(IAgricultureUnitOfWork agricultureUnitOfWork) : base(agricultureUnitOfWork) { }
    }
}
