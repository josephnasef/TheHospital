namespace TheHospital.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visits", "SoldierNum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visits", "SoldierNum", c => c.Int(nullable: false));
        }
    }
}
