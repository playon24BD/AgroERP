namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_ProductSalesDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesDetails", "ReceipeBatchCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesDetails", "ReceipeBatchCode");
        }
    }
}
