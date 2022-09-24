namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Nothing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialInfo", "ExpireDate", c => c.DateTime());
            DropColumn("dbo.tblRawMaterialStockInfo", "IssueStatus");
            DropColumn("dbo.tblRawMaterialStockInfo", "ExpireDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "ExpireDate", c => c.DateTime());
            AddColumn("dbo.tblRawMaterialStockInfo", "IssueStatus", c => c.String());
            DropColumn("dbo.tblRawMaterialInfo", "ExpireDate");
        }
    }
}
