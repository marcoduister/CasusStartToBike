namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RouteLocation", "Location", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteLocation", "Location", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
