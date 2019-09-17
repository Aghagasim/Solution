namespace WeekendTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialUPDATED : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "Post_Id" });
            AddColumn("dbo.Posts", "User_Id", c => c.Int());
            AddColumn("dbo.Users", "Post_Id", c => c.Int());
            CreateIndex("dbo.Posts", "User_Id");
            CreateIndex("dbo.Users", "Post_Id");
            AddForeignKey("dbo.Users", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Posts", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Post_Id", c => c.Int());
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Users", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropColumn("dbo.Users", "Post_Id");
            DropColumn("dbo.Posts", "User_Id");
            CreateIndex("dbo.Posts", "Post_Id");
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
