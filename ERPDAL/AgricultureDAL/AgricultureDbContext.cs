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
        public AgricultureDbContext():base("Agriculture")
        {

        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        //}

        public DbSet<BankSetup> tblBankInfo { get; set; }
        public DbSet<RawMaterial> tblRawMaterialInfo { get; set; }
        public DbSet<DepotSetup> tblDepotInfo { get; set; }
        public DbSet<FinishGoodProduct> tblFinishGoodProductInfo { get; set; }
        public DbSet<FinishGoodSupplier> tblFinishGoodSupplierInfo { get; set; }
        //public DbSet<RawMaterial> tblRawMaterials { get; set; }


    }
}
