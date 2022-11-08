namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_BoxQtyInSalesReturnandSalesDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesDetails", "BoxQuanity", c => c.Double(nullable: false));
            AddColumn("dbo.tblSalesReturn", "BoxQuanity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSalesReturn", "BoxQuanity");
            DropColumn("dbo.tblProductSalesDetails", "BoxQuanity");
        }
    }
}
