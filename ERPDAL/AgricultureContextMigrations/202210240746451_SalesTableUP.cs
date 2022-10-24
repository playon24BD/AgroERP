namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesTableUP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesInfo", "PaidAmount", c => c.Double(nullable: false));
            AddColumn("dbo.tblProductSalesInfo", "DueAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesInfo", "DueAmount");
            DropColumn("dbo.tblProductSalesInfo", "PaidAmount");
        }
    }
}
