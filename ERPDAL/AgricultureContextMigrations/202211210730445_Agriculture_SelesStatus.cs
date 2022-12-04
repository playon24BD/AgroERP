namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_SelesStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesDetails", "Status", c => c.String());
            AddColumn("dbo.tblProductSalesInfo", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesInfo", "Status");
            DropColumn("dbo.tblProductSalesDetails", "Status");
        }
    }
}
