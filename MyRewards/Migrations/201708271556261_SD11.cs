namespace MyRewards.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SD11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionGuids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Guid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SessionIdHashes");
        }
        
        public override void Down()
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
            
            DropTable("dbo.SessionGuids");
        }
    }
}
