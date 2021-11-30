namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sssss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kinds", "KindName", c => c.String());
            DropColumn("dbo.Kinds", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kinds", "Name", c => c.String());
            DropColumn("dbo.Kinds", "KindName");
        }
    }
}
