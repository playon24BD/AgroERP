namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_CommissionOnProductandHistoryTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCommissionOnProduct",
                c => new
                    {
                        CommissionOnProductId = c.Long(nullable: false, identity: true),
                        FGRId = c.Long(nullable: false),
                        FinishGoodProductId = c.Long(nullable: false),
                        CalenderYear = c.Int(nullable: false),
                        Credit = c.Double(nullable: false),
                        Cash = c.Double(nullable: false),
                        Quantity = c.Double(),
                        UnitId = c.Long(),
                        Remarks = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionOnProductId);
            
            CreateTable(
                "dbo.tblCommissionProductHistory",
                c => new
                    {
                        CommisionOnProductHistoryId = c.Long(nullable: false, identity: true),
                        CommissionOnProductId = c.Long(nullable: false),
                        FGRId = c.Long(nullable: false),
                        FinishGoodProductId = c.Long(nullable: false),
                        CalenderYear = c.Int(nullable: false),
                        Credit = c.Double(nullable: false),
                        Cash = c.Double(nullable: false),
                        Quantity = c.Double(),
                        UnitId = c.Long(),
                        Remarks = c.String(),
                        Status = c.String(),
                        Flag = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CommisionOnProductHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblCommissionProductHistory");
            DropTable("dbo.tblCommissionOnProduct");
        }
    }
}
