namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_UpdateTablesdsds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId", c => c.Long(nullable: false));
            DropColumn("dbo.tblFinishGoodRecipeDetails", "ReceipeBatchCode");
            DropColumn("dbo.tblFinishGoodRecipeInfo", "ReceipeBatchCode");
            DropColumn("dbo.tblRawMaterialStockDetail", "RawMaterialSupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRawMaterialStockDetail", "RawMaterialSupplierId", c => c.Long(nullable: false));
            AddColumn("dbo.tblFinishGoodRecipeInfo", "ReceipeBatchCode", c => c.String());
            AddColumn("dbo.tblFinishGoodRecipeDetails", "ReceipeBatchCode", c => c.String());
            DropColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId");
        }
    }
}
