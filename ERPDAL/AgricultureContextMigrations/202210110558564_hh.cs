namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblZoneInfos", "Status", c => c.String());
            DropColumn("dbo.tblRegionInfos", "Remarks");
            DropColumn("dbo.tblRegionInfos", "MobileNumber");
            DropColumn("dbo.tblRegionInfos", "RegionAsignName");
            DropColumn("dbo.tblRegionInfos", "ZoneId");
            DropColumn("dbo.tblZoneInfos", "Remarks");
            DropColumn("dbo.tblZoneInfos", "MobileNumber");
            DropColumn("dbo.tblZoneInfos", "ZoneAsignName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblZoneInfos", "ZoneAsignName", c => c.String());
            AddColumn("dbo.tblZoneInfos", "MobileNumber", c => c.String());
            AddColumn("dbo.tblZoneInfos", "Remarks", c => c.String());
            AddColumn("dbo.tblRegionInfos", "ZoneId", c => c.Long(nullable: false));
            AddColumn("dbo.tblRegionInfos", "RegionAsignName", c => c.String());
            AddColumn("dbo.tblRegionInfos", "MobileNumber", c => c.String());
            AddColumn("dbo.tblRegionInfos", "Remarks", c => c.String());
            DropColumn("dbo.tblZoneInfos", "Status");
        }
    }
}
