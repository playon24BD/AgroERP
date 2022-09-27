namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AllUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FinishGoodProductionDetails", "FGRRawMaterQty", c => c.Double(nullable: false));
            AlterColumn("dbo.FinishGoodProductionDetails", "TotalQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.FinishGoodProductionDetails", "RequiredQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.FinishGoodProductionInfoes", "Quanity", c => c.Double(nullable: false));
            AlterColumn("dbo.FinishGoodProductionInfoes", "TargetQuantity", c => c.Double(nullable: false));
            AlterColumn("dbo.tblRawMaterialIssueStockDetails", "Quantity", c => c.Double(nullable: false));
            AlterColumn("dbo.tblRawMaterialIssueStockInfo", "Quantity", c => c.Double(nullable: false));
            AlterColumn("dbo.tblRawMaterialStockDetail", "Quantity", c => c.Double(nullable: false));
            AlterColumn("dbo.tblRawMaterialStockInfo", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblRawMaterialStockInfo", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tblRawMaterialStockDetail", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tblRawMaterialIssueStockInfo", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.tblRawMaterialIssueStockDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.FinishGoodProductionInfoes", "TargetQuantity", c => c.Int(nullable: false));
            AlterColumn("dbo.FinishGoodProductionInfoes", "Quanity", c => c.Int(nullable: false));
            AlterColumn("dbo.FinishGoodProductionDetails", "RequiredQuantity", c => c.Int(nullable: false));
            AlterColumn("dbo.FinishGoodProductionDetails", "TotalQuantity", c => c.Int(nullable: false));
            AlterColumn("dbo.FinishGoodProductionDetails", "FGRRawMaterQty", c => c.Int(nullable: false));
        }
    }
}
