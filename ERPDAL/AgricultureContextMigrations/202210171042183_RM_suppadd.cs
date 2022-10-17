namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RM_suppadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialSupplierInfo", "TradeLicense", c => c.String());
            AddColumn("dbo.tblRawMaterialSupplierInfo", "TIN", c => c.String());
            AddColumn("dbo.tblRawMaterialSupplierInfo", "BIN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialSupplierInfo", "BIN");
            DropColumn("dbo.tblRawMaterialSupplierInfo", "TIN");
            DropColumn("dbo.tblRawMaterialSupplierInfo", "TradeLicense");
        }
    }
}
