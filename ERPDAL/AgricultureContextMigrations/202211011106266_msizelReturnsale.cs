namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class msizelReturnsale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblSalesReturn", "MeasurementSize", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSalesReturn", "MeasurementSize");
        }
    }
}
