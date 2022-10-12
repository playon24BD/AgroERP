namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DepoFinishProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblFinishGoodProductInfo", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.tblFinishGoodProductInfo", "EntryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblFinishGoodProductInfo", "EntryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tblFinishGoodProductInfo", "UpdateDate", c => c.DateTime(nullable: false));
        }
    }
}
