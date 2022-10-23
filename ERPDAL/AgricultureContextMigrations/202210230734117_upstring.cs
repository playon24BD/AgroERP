namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "Status", c => c.String());
            AlterColumn("dbo.tblMRawMaterialIssueStockDetails", "IssueStatus", c => c.String());
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "IssueStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "IssueStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.tblMRawMaterialIssueStockDetails", "IssueStatus", c => c.Long(nullable: false));
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "Status");
        }
    }
}
