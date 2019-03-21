namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionIdHashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        SessionHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SessionIdHashes");
        }
    }
}
