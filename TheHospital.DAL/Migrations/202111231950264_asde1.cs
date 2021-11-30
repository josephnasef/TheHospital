namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asde1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visits", "XraysImage", c => c.Binary());
            AddColumn("dbo.Visits", "analyzesImage", c => c.Binary());
            DropColumn("dbo.Visits", "XraysIamge");
            DropColumn("dbo.Visits", "analyzesIamge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visits", "analyzesIamge", c => c.Binary());
            AddColumn("dbo.Visits", "XraysIamge", c => c.Binary());
            DropColumn("dbo.Visits", "analyzesImage");
            DropColumn("dbo.Visits", "XraysImage");
        }
    }
}
