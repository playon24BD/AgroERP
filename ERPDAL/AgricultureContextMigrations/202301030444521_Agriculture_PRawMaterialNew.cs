namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_PRawMaterialNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblPRawMaterialStockDetail", "CQty", c => c.Double(nullable: false));
            AddColumn("dbo.tblPRawMaterialStockDetail", "CPrice", c => c.Double(nullable: false));
            AddColumn("dbo.tblPRawMaterialStockDetail", "CUnitName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblPRawMaterialStockDetail", "CUnitName");
            DropColumn("dbo.tblPRawMaterialStockDetail", "CPrice");
            DropColumn("dbo.tblPRawMaterialStockDetail", "CQty");
        }
    }
}
