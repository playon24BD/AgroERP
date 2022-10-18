namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AddUnitId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblFinishGoodRecipeDetails", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblFinishGoodRecipeInfo", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblMeasurement", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRawMaterialInfo", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRawMaterialIssueStockDetails", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRawMaterialIssueStockInfo", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRawMaterialStockDetail", "UnitId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRawMaterialStockInfo", "UnitId", c => c.Long(nullable: false));
            DropColumn("dbo.tblFinishGoodRecipeDetails", "FGRRawMaterUnit");
            DropColumn("dbo.tblFinishGoodRecipeInfo", "FGRUnit");
            DropColumn("dbo.tblMeasurement", "Unit");
            DropColumn("dbo.tblRawMaterialInfo", "Unit");
            DropColumn("dbo.tblRawMaterialIssueStockDetails", "Unit");
            DropColumn("dbo.tblRawMaterialIssueStockInfo", "Unit");
            DropColumn("dbo.tblRawMaterialStockDetail", "Unit");
            DropColumn("dbo.tblRawMaterialStockInfo", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "Unit", c => c.String());
            AddColumn("dbo.tblRawMaterialStockDetail", "Unit", c => c.String());
            AddColumn("dbo.tblRawMaterialIssueStockInfo", "Unit", c => c.String());
            AddColumn("dbo.tblRawMaterialIssueStockDetails", "Unit", c => c.String());
            AddColumn("dbo.tblRawMaterialInfo", "Unit", c => c.String());
            AddColumn("dbo.tblMeasurement", "Unit", c => c.String());
            AddColumn("dbo.tblFinishGoodRecipeInfo", "FGRUnit", c => c.String());
            AddColumn("dbo.tblFinishGoodRecipeDetails", "FGRRawMaterUnit", c => c.String());
            DropColumn("dbo.tblRawMaterialStockInfo", "UnitId");
            DropColumn("dbo.tblRawMaterialStockDetail", "UnitId");
            DropColumn("dbo.tblRawMaterialIssueStockInfo", "UnitId");
            DropColumn("dbo.tblRawMaterialIssueStockDetails", "UnitId");
            DropColumn("dbo.tblRawMaterialInfo", "UnitId");
            DropColumn("dbo.tblMeasurement", "UnitId");
            DropColumn("dbo.tblFinishGoodRecipeInfo", "UnitId");
            DropColumn("dbo.tblFinishGoodRecipeDetails", "UnitId");
        }
    }
}
