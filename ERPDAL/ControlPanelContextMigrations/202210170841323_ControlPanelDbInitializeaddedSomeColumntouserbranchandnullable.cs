namespace ERPDAL.ControlPanelContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlPanelDbInitializeaddedSomeColumntouserbranchandnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblApplicationUsers", "ZoneId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "DivisionId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "RegionId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "AreaId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "TerritoryId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "StockiestId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblApplicationUsers", "StockiestId", c => c.Long(nullable: false));
            AlterColumn("dbo.tblApplicationUsers", "TerritoryId", c => c.Long(nullable: false));
            AlterColumn("dbo.tblApplicationUsers", "AreaId", c => c.Long(nullable: false));
            AlterColumn("dbo.tblApplicationUsers", "RegionId", c => c.Long(nullable: false));
            AlterColumn("dbo.tblApplicationUsers", "DivisionId", c => c.Long(nullable: false));
            AlterColumn("dbo.tblApplicationUsers", "ZoneId", c => c.Long(nullable: false));
        }
    }
}
