namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_Depo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "FinishGoodProductId", c => c.Long(nullable: false));
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "ProductId", c => c.Long(nullable: false));
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "FinishGoodProductId");
        }
    }
}
