namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_SalesProductTableUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinishGoodProductionInfoes", "FGRId", c => c.Long(nullable: false));
            AddColumn("dbo.tblProductSalesDetails", "FGRId", c => c.Long(nullable: false));
            AddColumn("dbo.tblProductSalesDetails", "QtyKG", c => c.String());
            AddColumn("dbo.tblSalesReturn", "FGRId", c => c.Long(nullable: false));
            AddColumn("dbo.tblSalesReturn", "QtyKG", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSalesReturn", "QtyKG");
            DropColumn("dbo.tblSalesReturn", "FGRId");
            DropColumn("dbo.tblProductSalesDetails", "QtyKG");
            DropColumn("dbo.tblProductSalesDetails", "FGRId");
            DropColumn("dbo.FinishGoodProductionInfoes", "FGRId");
        }
    }
}
