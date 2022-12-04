namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_priceinde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCommisionOnProductSalesDetails", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("dbo.tblCommisionOnProductSalesDetails", "price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCommisionOnProductSalesDetails", "price");
            DropColumn("dbo.tblCommisionOnProductSalesDetails", "TotalAmount");
        }
    }
}
