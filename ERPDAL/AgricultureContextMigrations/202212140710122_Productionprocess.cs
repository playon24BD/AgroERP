namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productionprocess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinishGoodProductionInfoes", "MFGQuanity", c => c.Double(nullable: false));
            AddColumn("dbo.FinishGoodProductionInfoes", "MeasurementId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinishGoodProductionInfoes", "MeasurementId");
            DropColumn("dbo.FinishGoodProductionInfoes", "MFGQuanity");
        }
    }
}
