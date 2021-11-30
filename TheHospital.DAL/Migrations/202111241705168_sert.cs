namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "XraysState", c => c.Boolean(nullable: false));
            AddColumn("dbo.Visits", "analyzesState", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visits", "analyzesState");
            DropColumn("dbo.Visits", "XraysState");
        }
    }
}
