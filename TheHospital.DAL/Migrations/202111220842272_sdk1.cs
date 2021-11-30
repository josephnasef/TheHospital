namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdk1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "XraysIamge", c => c.Binary());
            AddColumn("dbo.Visits", "analyzesIamge", c => c.Binary());
            DropColumn("dbo.Visits", "analyzes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visits", "analyzes", c => c.Boolean(nullable: false));
            DropColumn("dbo.Visits", "analyzesIamge");
            DropColumn("dbo.Visits", "XraysIamge");
        }
    }
}
