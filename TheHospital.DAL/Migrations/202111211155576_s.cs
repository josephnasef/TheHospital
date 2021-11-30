namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        VisitId = c.Int(nullable: false, identity: true),
                        PationtName = c.String(),
                        SoldierNum = c.Int(nullable: false),
                        Location = c.String(),
                        CaseDescription = c.String(),
                        DoctoreName = c.String(),
                        EnterDate = c.DateTime(nullable: false),
                        clinic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitId)
                .ForeignKey("dbo.clinics", t => t.clinic_Id, cascadeDelete: true)
                .Index(t => t.clinic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "clinic_Id", "dbo.clinics");
            DropIndex("dbo.Visits", new[] { "clinic_Id" });
            DropTable("dbo.Visits");
            DropTable("dbo.clinics");
        }
    }
}
