namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_DepoMesurementnew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMeasurement", "FinishGoodProductId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblMeasurement", "FinishGoodProductId");
        }
    }
}
