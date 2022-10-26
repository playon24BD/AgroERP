namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_AgroProductSales : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblProductSalesDetails", "FinishGoodProductInfoId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblProductSalesDetails", "FinishGoodProductInfoId", c => c.String());
        }
    }
}
