namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblDepotInfo", "UpdateDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tblDepotInfo", "EntryDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tblRawMaterialInfo", "UpdateDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tblRawMaterialInfo", "EntryDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.tblRawMaterialInfo", "ExpireDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblRawMaterialInfo", "ExpireDate", c => c.DateTime());
            AlterColumn("dbo.tblRawMaterialInfo", "EntryDate", c => c.DateTime());
            AlterColumn("dbo.tblRawMaterialInfo", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.tblDepotInfo", "EntryDate", c => c.DateTime());
            AlterColumn("dbo.tblDepotInfo", "UpdateDate", c => c.DateTime());
        }
    }
}
