namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_detailsIdsales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCommisionOnProductSalesDetails", "ProductSalesDetailsId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCommisionOnProductSalesDetails", "ProductSalesDetailsId");
        }
    }
}
