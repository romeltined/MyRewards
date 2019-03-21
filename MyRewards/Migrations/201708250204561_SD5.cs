namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MerchantManagers", newName: "Managers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Managers", newName: "MerchantManagers");
        }
    }
}
