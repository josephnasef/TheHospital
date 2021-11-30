namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdkl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kinds",
                c => new
                    {
                        KindId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.KindId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Kind_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Kinds", t => t.Kind_Id, cascadeDelete: true)
                .Index(t => t.Kind_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Kind_Id", "dbo.Kinds");
            DropIndex("dbo.Users", new[] { "Kind_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Kinds");
        }
    }
}
