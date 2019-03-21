namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Settlements", "SettledOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settlements", "SettledOn", c => c.DateTime(nullable: false));
        }
    }
}
