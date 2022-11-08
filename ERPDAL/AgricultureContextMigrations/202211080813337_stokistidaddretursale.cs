namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stokistidaddretursale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblSalesReturn", "StockiestId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSalesReturn", "StockiestId");
        }
    }
}
