using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ERPBO.Agriculture.DomainModels;
using ERPBO.ControlPanel.DomainModels;

namespace ERPDAL.AgricultureDAL
{
    public class AgricultureDbContext : DbContext
    {

        public AgricultureDbContext() : base("Agriculture")
        {

        }


        public DbSet<ProductionPerproductCost> tblProductionPerproductCost { get; set; } //e
        public DbSet<ProductPricingHistory> tblProductPricingHistory { get; set; } //e
        public DbSet<ProductPriceConfiguration> tblProductPriceConfiguration { get; set; } //e

        public DbSet<SalesReturn> tblSalesReturn { get; set; } //e


        public DbSet<SalesPaymentRegister> tblProductSalesPaymentHistory { get; set; } //e

        public DbSet<MRawMaterialIssueStockDetails> tblMRawMaterialIssueStockDetails { get; set; } //e
        public DbSet<MRawMaterialIssueStockInfo> tblMRawMaterialIssueStockInfo { get; set; } //e
        public DbSet<RawMaterialTrack> tblRawMaterialTrackInfo { get; set; } //e

        public DbSet<PRawMaterialStockInfo> tblPRawMaterialStockInfo { get; set; } //e
        public DbSet<PRawMaterialStockIDetails> tblPRawMaterialStockDetail { get; set; }//e

        public DbSet<AgroUnitInfo> tblAgroUnitInfo { get; set; }
        public DbSet<UserInfo> tblUserInfo { get; set; }
        public DbSet<StockiestInfo> tblStockiestInfo { get; set; }
        public DbSet<UserAssign> tblUserAssign { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        //}
        
        public DbSet<StockiestInfo> tblStockiestInfo { get; set; }

        public DbSet<DivisionInfo> tblDivisionInfo { get; set; }
        public DbSet<Zone> tblZoneInfo { get; set; }
        public DbSet<ZoneDetail> tblZoneDetail { get; set; }
        public DbSet<RawMaterialStockInfo> tblRawMaterialStockInfo { get; set; }
        public DbSet<RawMaterialStockDetail> tblRawMaterialStockDetail { get; set; }
        public DbSet<RawMaterialSupplier> tblRawMaterialSupplierInfo { get; set; }
        public DbSet<BankSetup> tblBankInfo { get; set; }
        public DbSet<RawMaterial> tblRawMaterialInfo { get; set; }
        public DbSet<DepotSetup> tblDepotInfo { get; set; }
        public DbSet<FinishGoodProduct> tblFinishGoodProductInfo { get; set; }
        public DbSet<FinishGoodSupplier> tblFinishGoodSupplierInfo { get; set; }
        public DbSet<MeasurementSetup> tblMeasurement { get; set; }
        public DbSet<FinishGoodRecipeInfo> tblFinishGoodRecipeInfo { get; set; }
        public DbSet<FinishGoodRecipeDetails> tblFinishGoodRecipeDetails { get; set; }

        public DbSet<RawMaterialIssueStockInfo> tblRawMaterialIssueStockInfo { get; set; }
        public DbSet<RawMaterialIssueStockDetails> tblRawMaterialIssueStockDetails { get; set; }
        public DbSet<FinishGoodProductionInfo> tblFinishGoodProductionInfos { get; set; }
        public DbSet<FinishGoodProductionDetails> tblFinishGoodProductionDetails { get; set; }
        public DbSet<ZoneSetup> tblZoneInfos { get; set; } //e
        public DbSet<RegionSetup> tblRegionInfos { get; set; } //e

        public DbSet<AreaInfoSetup> tblAreaSetup { get; set; } //e
        public DbSet<TerritorySetup> tblTerritoryInfos { get; set; } //e
        public DbSet<AgroProductSalesInfo> tblProductSalesInfo { get; set; }
        public DbSet<AgroProductSalesDetails> tblProductSalesDetails { get; set; }
        public DbSet<DivisionUser> tblDivisionUser { get; set; }
        public DbSet<DistributionUser> tblDistributionUser { get; set; }

        public DbSet<ZoneUser> tblZoneUser { get; set; }
        public DbSet<RegionUser> tblRezoneUser { get; set; }
        public DbSet<AreaUser> tblAreaUser { get; set; }
        public DbSet<TerritoryUser> tblTerryUser { get; set; }
        public DbSet<StockiestUser> tblStokiestUser { get; set; }

        public DbSet<ReturnRawMaterial> tblReturnRawMaterial { get; set; }
        public DbSet<RawMaterialRequisitionInfo> tblRawMaterialRequisitionInfo { get; set; }
        public DbSet<RawMaterialRequisitionDetails> tblRawMaterialRequisitionDetails { get; set; }
        public DbSet<CommisionOnProduct> tblCommisionOnProduct  { get; set; }
        public DbSet<CommisionOnProductHistory> tblCommissionProductHistory { get; set; }
        public DbSet<CommissionOnProductOnSales> tblCommissionOnProductOnSales { get; set; }
        public DbSet<CommisionOnProductSalesDetails> tblCommisionOnProductSalesDetails { get; set; }
        public DbSet<TerritorySetup> tblTerritoryInfos { get; set; } //e
        public DbSet<AreaInfoSetup> tblAreaSetup { get; set; } 




        //public DbSet<RawMaterial> tblRawMaterials { get; set; }


    }
}
