namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialV10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(maxLength: 50, unicode: false),
                        Birthdate = c.DateTime(nullable: false, storeType: "date"),
                        Residence = c.String(maxLength: 50, unicode: false),
                        ProfileImage = c.String(maxLength: 255, fixedLength: true, unicode: false),
                        Distance = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsActive = c.Int(nullable: false),
                        Role = c.Long(nullable: false),
                        CycleEvent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CycleEvent", t => t.CycleEvent_Id)
                .Index(t => t.CycleEvent_Id);
            
            CreateTable(
                "dbo.CycleEvent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 50, unicode: false),
                        EventStart = c.DateTime(nullable: false),
                        EventEnd = c.DateTime(nullable: false),
                        Location = c.String(nullable: false, maxLength: 50, unicode: false),
                        Difficulty = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        MakerId = c.Int(nullable: false),
                        BadgeId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badge", t => t.BadgeId)
                .ForeignKey("dbo.CycleRoute", t => t.RouteId)
                .ForeignKey("dbo.User", t => t.MakerId)
                .Index(t => t.MakerId)
                .Index(t => t.BadgeId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BadgeName = c.String(nullable: false, maxLength: 50, unicode: false),
                        BadgeImage = c.String(nullable: false, maxLength: 255, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CycleRoute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteName = c.String(nullable: false, maxLength: 50, unicode: false),
                        RouteType = c.String(maxLength: 50, unicode: false),
                        Difficulty = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        MakerId = c.Int(nullable: false),
                        BadgeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badge", t => t.BadgeId)
                .ForeignKey("dbo.User", t => t.MakerId)
                .Index(t => t.MakerId)
                .Index(t => t.BadgeId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Description = c.String(maxLength: 500, unicode: false),
                        MakerId = c.Int(nullable: false),
                        EventId = c.Int(),
                        RouteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CycleRoute", t => t.RouteId)
                .ForeignKey("dbo.CycleEvent", t => t.EventId)
                .ForeignKey("dbo.User", t => t.MakerId)
                .Index(t => t.MakerId)
                .Index(t => t.EventId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.RouteLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false, unicode: false),
                        RouteId = c.Int(nullable: false),
                        LastLocationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RouteLocation", t => t.LastLocationId)
                .ForeignKey("dbo.CycleRoute", t => t.RouteId)
                .Index(t => t.RouteId)
                .Index(t => t.LastLocationId);
            
            CreateTable(
                "dbo.Follower",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                        Date = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.User", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "MakerId", "dbo.User");
            DropForeignKey("dbo.Follower", "FollowerId", "dbo.User");
            DropForeignKey("dbo.Follower", "UserId", "dbo.User");
            DropForeignKey("dbo.CycleRoute", "MakerId", "dbo.User");
            DropForeignKey("dbo.CycleEvent", "MakerId", "dbo.User");
            DropForeignKey("dbo.Review", "EventId", "dbo.CycleEvent");
            DropForeignKey("dbo.User", "CycleEvent_Id", "dbo.CycleEvent");
            DropForeignKey("dbo.CycleRoute", "BadgeId", "dbo.Badge");
            DropForeignKey("dbo.RouteLocation", "RouteId", "dbo.CycleRoute");
            DropForeignKey("dbo.RouteLocation", "LastLocationId", "dbo.RouteLocation");
            DropForeignKey("dbo.Review", "RouteId", "dbo.CycleRoute");
            DropForeignKey("dbo.CycleEvent", "RouteId", "dbo.CycleRoute");
            DropForeignKey("dbo.CycleEvent", "BadgeId", "dbo.Badge");
            DropForeignKey("dbo.Account", "UserId", "dbo.User");
            DropIndex("dbo.Follower", new[] { "FollowerId" });
            DropIndex("dbo.Follower", new[] { "UserId" });
            DropIndex("dbo.RouteLocation", new[] { "LastLocationId" });
            DropIndex("dbo.RouteLocation", new[] { "RouteId" });
            DropIndex("dbo.Review", new[] { "RouteId" });
            DropIndex("dbo.Review", new[] { "EventId" });
            DropIndex("dbo.Review", new[] { "MakerId" });
            DropIndex("dbo.CycleRoute", new[] { "BadgeId" });
            DropIndex("dbo.CycleRoute", new[] { "MakerId" });
            DropIndex("dbo.CycleEvent", new[] { "RouteId" });
            DropIndex("dbo.CycleEvent", new[] { "BadgeId" });
            DropIndex("dbo.CycleEvent", new[] { "MakerId" });
            DropIndex("dbo.User", new[] { "CycleEvent_Id" });
            DropIndex("dbo.Account", new[] { "UserId" });
            DropTable("dbo.Follower");
            DropTable("dbo.RouteLocation");
            DropTable("dbo.Review");
            DropTable("dbo.CycleRoute");
            DropTable("dbo.Badge");
            DropTable("dbo.CycleEvent");
            DropTable("dbo.User");
            DropTable("dbo.Account");
        }
    }
}
