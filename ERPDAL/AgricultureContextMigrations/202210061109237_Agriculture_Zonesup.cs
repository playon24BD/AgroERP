namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Zonesup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblZoneInfos", "MobileNumber", c => c.String());
            AddColumn("dbo.tblZoneInfos", "ZoneAsignName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblZoneInfos", "ZoneAsignName");
            DropColumn("dbo.tblZoneInfos", "MobileNumber");
        }
    }
}
