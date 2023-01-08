namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearlyTarget : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "FromDate", c => c.DateTime());
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "ToDate", c => c.DateTime());
            AddColumn("dbo.tblStockiestWiseYearlyTarget", "FinishGoodProductId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "FinishGoodProductId");
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "ToDate");
            DropColumn("dbo.tblStockiestWiseYearlyTarget", "FromDate");
        }
    }
}
