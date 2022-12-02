namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_MeasurementUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMeasurement", "UnitKG", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblMeasurement", "UnitKG");
        }
    }
}
