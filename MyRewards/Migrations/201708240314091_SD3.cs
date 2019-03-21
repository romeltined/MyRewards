namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vouchers", "Merchant_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vouchers", "Merchant_Id");
        }
    }
}
