namespace ERPDAL.AgricultureContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agriculture_RequistionRawMaterialInfoandDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRawMaterialRequistionDetails",
                c => new
                    {
                        RawMaterialRequisitionDetailsId = c.Long(nullable: false, identity: true),
                        RawMaterialRequisitionInfoId = c.Long(nullable: false),
                        RawMaterialId = c.Long(nullable: false),
                        IssueQuantity = c.Double(nullable: false),
                        RequisitionQuantity = c.Double(nullable: false),
                        UnitID = c.Long(nullable: false),
                        Type = c.String(),
                        Status = c.String(),
                        flag = c.String(),
                        Remarks = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialRequisitionDetailsId);
            
            CreateTable(
                "dbo.tblRawMaterialRequisitionInfo",
                c => new
                    {
                        RawMaterialRequisitionInfoId = c.Long(nullable: false, identity: true),
                        RawMaterialRequisitionCode = c.String(),
                        Type = c.String(),
                        Status = c.String(),
                        flag = c.String(),
                        Remarks = c.String(),
                        EntryDate = c.DateTime(),
                        EntryUserId = c.Long(),
                        UpdateDate = c.DateTime(),
                        UpdateUserId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RawMaterialRequisitionInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblRawMaterialRequisitionInfo");
            DropTable("dbo.tblRawMaterialRequistionDetails");
        }
    }
}
