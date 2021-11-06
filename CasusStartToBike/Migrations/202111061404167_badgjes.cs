namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badgjes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Badge_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Badge_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Badge", t => t.Badge_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Badge_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBadges", "Badge_Id", "dbo.Badge");
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.User");
            DropIndex("dbo.UserBadges", new[] { "Badge_Id" });
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropTable("dbo.UserBadges");
        }
    }
}
