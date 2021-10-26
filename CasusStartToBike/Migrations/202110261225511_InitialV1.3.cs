namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialV13 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CycleEvent", new[] { "RouteId" });
            AlterColumn("dbo.CycleEvent", "RouteId", c => c.Int());
            CreateIndex("dbo.CycleEvent", "RouteId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CycleEvent", new[] { "RouteId" });
            AlterColumn("dbo.CycleEvent", "RouteId", c => c.Int(nullable: false));
            CreateIndex("dbo.CycleEvent", "RouteId");
        }
    }
}
