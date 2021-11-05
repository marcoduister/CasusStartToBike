namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "User_Role", c => c.Int(nullable: false));
            DropColumn("dbo.User", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Role", c => c.Long(nullable: false));
            DropColumn("dbo.User", "User_Role");
        }
    }
}
