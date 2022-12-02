namespace ERPDAL.ControlPanelContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ControlPanel_changelongtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblApplicationUsers", "ZoneId", c => c.String());
            AlterColumn("dbo.tblApplicationUsers", "DivisionId", c => c.String());
            AlterColumn("dbo.tblApplicationUsers", "RegionId", c => c.String());
            AlterColumn("dbo.tblApplicationUsers", "AreaId", c => c.String());
            AlterColumn("dbo.tblApplicationUsers", "TerritoryId", c => c.String());
            AlterColumn("dbo.tblApplicationUsers", "StockiestId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblApplicationUsers", "StockiestId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "TerritoryId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "AreaId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "RegionId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "DivisionId", c => c.Long());
            AlterColumn("dbo.tblApplicationUsers", "ZoneId", c => c.Long());
        }
    }
}
