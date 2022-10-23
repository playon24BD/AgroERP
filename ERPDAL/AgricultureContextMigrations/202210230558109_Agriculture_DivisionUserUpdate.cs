namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DivisionUserUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DivisionUsers",
                c => new
                    {
                        DivisionUserId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DivisionId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        Status = c.String(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DivisionUserId);
            
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "IssueStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.tblMRawMaterialIssueStockDetails", "IssueStatus", c => c.Long(nullable: false));
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblMRawMaterialIssueStockInfo", "Status", c => c.String());
            AlterColumn("dbo.tblMRawMaterialIssueStockDetails", "IssueStatus", c => c.String());
            DropColumn("dbo.tblMRawMaterialIssueStockInfo", "IssueStatus");
            DropTable("dbo.DivisionUsers");
        }
    }
}
