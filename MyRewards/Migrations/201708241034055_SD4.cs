namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MerchantManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Id = c.Int(nullable: false),
                        Merchant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.Merchant_Id)
                .Index(t => t.Merchant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MerchantManagers", "Merchant_Id", "dbo.Merchants");
            DropIndex("dbo.MerchantManagers", new[] { "Merchant_Id" });
            DropTable("dbo.MerchantManagers");
        }
    }
}
