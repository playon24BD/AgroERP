﻿using System;
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
        public AgricultureDbContext():base("Agriculture")
        {

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        //}

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

        //public DbSet<RawMaterial> tblRawMaterials { get; set; }


    }
}
