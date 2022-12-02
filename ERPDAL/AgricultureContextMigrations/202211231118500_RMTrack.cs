namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RMTrack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialTrackInfo", "RawMaterialIssueStockId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialTrackInfo", "RawMaterialIssueStockId");
        }
    }
}
