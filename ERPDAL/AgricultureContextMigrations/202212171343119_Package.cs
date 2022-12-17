namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Package : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblPackageDetails",
                c => new
                    {
                        PackageDetailsId = c.Long(nullable: false, identity: true),
                        FinishGoodProductId = c.Long(nullable: false),
                        MeasurementId = c.Long(nullable: false),
                        FGRId = c.Long(nullable: false),
                        Quanity = c.Double(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.String(),
                        Flag = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        PackageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PackageDetailsId);
            
            CreateTable(
                "dbo.tblPackageInfo",
                c => new
                    {
                        PackageId = c.Long(nullable: false, identity: true),
                        PackageCode = c.String(),
                        PackageName = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        TotalAmount = c.Double(nullable: false),
                        Status = c.String(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.PackageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblPackageInfo");
            DropTable("dbo.tblPackageDetails");
        }
    }
}
