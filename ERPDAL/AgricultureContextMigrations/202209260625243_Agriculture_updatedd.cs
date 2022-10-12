namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_updatedd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblFinishGoodRecipeDetails", "ReceipeBatchCode", c => c.String());
            AddColumn("dbo.tblFinishGoodRecipeInfo", "ReceipeBatchCode", c => c.String());
            AddColumn("dbo.tblRawMaterialStockDetail", "RawMaterialSupplierId", c => c.Long(nullable: false));
            DropColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "RawMaterialSupplierId", c => c.Long(nullable: false));
            DropColumn("dbo.tblRawMaterialStockDetail", "RawMaterialSupplierId");
            DropColumn("dbo.tblFinishGoodRecipeInfo", "ReceipeBatchCode");
            DropColumn("dbo.tblFinishGoodRecipeDetails", "ReceipeBatchCode");
        }
    }
}
