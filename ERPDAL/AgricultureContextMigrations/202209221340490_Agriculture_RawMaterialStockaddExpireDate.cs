namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialStockaddExpireDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialStockInfo", "IssueStatus", c => c.String());
            AddColumn("dbo.tblRawMaterialStockInfo", "ExpireDate", c => c.DateTime());
            DropColumn("dbo.tblRawMaterialInfo", "ExpireDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRawMaterialInfo", "ExpireDate", c => c.DateTime());
            DropColumn("dbo.tblRawMaterialStockInfo", "ExpireDate");
            DropColumn("dbo.tblRawMaterialStockInfo", "IssueStatus");
        }
    }
}
