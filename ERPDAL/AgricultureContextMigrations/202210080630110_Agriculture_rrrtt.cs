namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_rrrtt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialStockDetail", "StockIssueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialStockDetail", "StockIssueDate");
        }
    }
}
