namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_tblMeasurement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblMeasurement",
                c => new
                    {
                        MeasurementId = c.Long(nullable: false, identity: true),
                        MeasurementName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        MasterCarton = c.Int(nullable: false),
                        InnerBox = c.Int(nullable: false),
                        PackSize = c.Double(nullable: false),
                        Unit = c.String(),
                        RoleId = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.MeasurementId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblMeasurement");
        }
    }
}
