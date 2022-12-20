namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Packageadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductSalesDetails", "PackageId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductSalesDetails", "PackageId");
        }
    }
}
