namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialV12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CycleEvent", new[] { "BadgeId" });
            DropIndex("dbo.CycleRoute", new[] { "BadgeId" });
            AlterColumn("dbo.CycleEvent", "BadgeId", c => c.Int());
            AlterColumn("dbo.CycleRoute", "BadgeId", c => c.Int());
            CreateIndex("dbo.CycleEvent", "BadgeId");
            CreateIndex("dbo.CycleRoute", "BadgeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CycleRoute", new[] { "BadgeId" });
            DropIndex("dbo.CycleEvent", new[] { "BadgeId" });
            AlterColumn("dbo.CycleRoute", "BadgeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CycleEvent", "BadgeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CycleRoute", "BadgeId");
            CreateIndex("dbo.CycleEvent", "BadgeId");
        }
    }
}
