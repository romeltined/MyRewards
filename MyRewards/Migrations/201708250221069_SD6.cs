namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Managers", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Managers", "Username");
        }
    }
}
