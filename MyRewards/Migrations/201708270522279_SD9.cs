namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vouchers", "VoucherSpend_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vouchers", "VoucherSpend_Id");
        }
    }
}
