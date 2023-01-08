namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionCostfind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinishGoodProductionInfoes", "ProductionRMPrice", c => c.Double(nullable: false));
            AddColumn("dbo.FinishGoodProductionInfoes", "ProductionOtherExpense", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinishGoodProductionInfoes", "ProductionOtherExpense");
            DropColumn("dbo.FinishGoodProductionInfoes", "ProductionRMPrice");
        }
    }
}
