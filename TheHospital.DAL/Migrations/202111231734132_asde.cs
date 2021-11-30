namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asde : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "KindName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "KindName");
        }
    }
}
