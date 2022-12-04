namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datedelReturnsale : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblSalesReturn", "InvoiceDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblSalesReturn", "InvoiceDate", c => c.DateTime(nullable: false));
        }
    }
}
