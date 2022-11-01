namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_typeinTractblandrequistionInfoIdandTypeinissueStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "RawMaterialRequisitionInfoId", c => c.Long());
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "type", c => c.String());
            AddColumn("dbo.tblRawMaterialTrackInfo", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialTrackInfo", "type");
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "type");
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "RawMaterialRequisitionInfoId");
        }
    }
}
