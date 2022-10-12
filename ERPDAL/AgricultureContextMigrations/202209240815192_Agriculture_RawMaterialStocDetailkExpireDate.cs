namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RawMaterialStocDetailkExpireDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRawMaterialStockDetail", "ExpireDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRawMaterialStockDetail", "ExpireDate");
        }
    }
}
