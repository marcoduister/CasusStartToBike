namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialV11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "CycleEvent_Id", "dbo.CycleEvent");
            DropIndex("dbo.User", new[] { "CycleEvent_Id" });
            CreateTable(
                "dbo.CycleEventUsers",
                c => new
                    {
                        CycleEvent_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CycleEvent_Id, t.User_Id })
                .ForeignKey("dbo.CycleEvent", t => t.CycleEvent_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.CycleEvent_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.User", "CycleEvent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "CycleEvent_Id", c => c.Int());
            DropForeignKey("dbo.CycleEventUsers", "User_Id", "dbo.User");
            DropForeignKey("dbo.CycleEventUsers", "CycleEvent_Id", "dbo.CycleEvent");
            DropIndex("dbo.CycleEventUsers", new[] { "User_Id" });
            DropIndex("dbo.CycleEventUsers", new[] { "CycleEvent_Id" });
            DropTable("dbo.CycleEventUsers");
            CreateIndex("dbo.User", "CycleEvent_Id");
            AddForeignKey("dbo.User", "CycleEvent_Id", "dbo.CycleEvent", "Id");
        }
    }
}
