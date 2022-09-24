namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialStockAddSupplierId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId");
        }
    }
}
