namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issueaddfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMRawMaterialIssueStockDetails", "FinishGoodProductionBatch", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblMRawMaterialIssueStockDetails", "FinishGoodProductionBatch");
        }
    }
}
