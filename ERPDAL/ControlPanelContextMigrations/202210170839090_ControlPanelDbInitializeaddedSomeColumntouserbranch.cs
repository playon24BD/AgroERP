namespace ERPDAL.ControlPanelContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlPanelDbInitializeaddedSomeColumntouserbranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblApplicationUsers", "ZoneId", c => c.Long(nullable: false));
            AddColumn("dbo.tblApplicationUsers", "DivisionId", c => c.Long(nullable: false));
            AddColumn("dbo.tblApplicationUsers", "RegionId", c => c.Long(nullable: false));
            AddColumn("dbo.tblApplicationUsers", "AreaId", c => c.Long(nullable: false));
            AddColumn("dbo.tblApplicationUsers", "TerritoryId", c => c.Long(nullable: false));
            AddColumn("dbo.tblApplicationUsers", "StockiestId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblApplicationUsers", "StockiestId");
            DropColumn("dbo.tblApplicationUsers", "TerritoryId");
            DropColumn("dbo.tblApplicationUsers", "AreaId");
            DropColumn("dbo.tblApplicationUsers", "RegionId");
            DropColumn("dbo.tblApplicationUsers", "DivisionId");
            DropColumn("dbo.tblApplicationUsers", "ZoneId");
        }
    }
}
