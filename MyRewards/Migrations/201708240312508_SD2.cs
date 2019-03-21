namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vouchers", "Merchant_Id", "dbo.Merchants");
            DropIndex("dbo.Vouchers", new[] { "Merchant_Id" });
            DropColumn("dbo.Vouchers", "Merchant_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vouchers", "Merchant_Id", c => c.Int());
            CreateIndex("dbo.Vouchers", "Merchant_Id");
            AddForeignKey("dbo.Vouchers", "Merchant_Id", "dbo.Merchants", "Id");
        }
    }
}
