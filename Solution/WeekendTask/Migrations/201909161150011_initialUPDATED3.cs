namespace WeekendTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialUPDATED3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPosts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserPosts", "Post_Id", "dbo.Posts");
            DropIndex("dbo.UserPosts", new[] { "User_Id" });
            DropIndex("dbo.UserPosts", new[] { "Post_Id" });
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropTable("dbo.UserPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Post_Id });
            
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserId" });
            CreateIndex("dbo.UserPosts", "Post_Id");
            CreateIndex("dbo.UserPosts", "User_Id");
            AddForeignKey("dbo.UserPosts", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserPosts", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
