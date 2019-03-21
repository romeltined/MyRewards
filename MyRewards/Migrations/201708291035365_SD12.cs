namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settlements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Merchant_Id = c.Int(nullable: false),
                        MerchantName = c.String(),
                        Count = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SettledOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settlements");
        }
    }
}
