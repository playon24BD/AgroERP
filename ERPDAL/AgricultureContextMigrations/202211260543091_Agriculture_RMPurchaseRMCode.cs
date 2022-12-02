namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RMPurchaseRMCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblPRawMaterialStockDetail", "RmMRPCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblPRawMaterialStockDetail", "RmMRPCode");
        }
    }
}
