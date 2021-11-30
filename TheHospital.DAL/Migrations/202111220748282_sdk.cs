namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "Xrays", c => c.Boolean(nullable: false));
            AddColumn("dbo.Visits", "analyzes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visits", "analyzes");
            DropColumn("dbo.Visits", "Xrays");
        }
    }
}
