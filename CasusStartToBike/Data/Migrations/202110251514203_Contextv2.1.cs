namespace CasusStartToBike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Contextv21 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Account");
            AddColumn("dbo.User", "CycleEvent_Id", c => c.Int());
            AlterColumn("dbo.Account", "FirstName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Account", "LastName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Account", "Residence", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Account", "UserId");
            CreateIndex("dbo.User", "CycleEvent_Id");
            AddForeignKey("dbo.User", "CycleEvent_Id", "dbo.CycleEvent", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "CycleEvent_Id", "dbo.CycleEvent");
            DropIndex("dbo.User", new[] { "CycleEvent_Id" });
            DropPrimaryKey("dbo.Account");
            AlterColumn("dbo.Account", "Residence", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Account", "LastName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Account", "FirstName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.User", "CycleEvent_Id");
            AddPrimaryKey("dbo.Account", new[] { "UserId", "FirstName", "LastName", "Birthdate", "Residence" });
        }
    }
}
