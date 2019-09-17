namespace WeekendTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialUPDATED1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Post_Id" });
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Post_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Post_Id);
            
            DropColumn("dbo.Posts", "User_Id");
            DropColumn("dbo.Users", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Post_Id", c => c.Int());
            AddColumn("dbo.Posts", "User_Id", c => c.Int());
            DropForeignKey("dbo.UserPosts", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.UserPosts", "User_Id", "dbo.Users");
            DropIndex("dbo.UserPosts", new[] { "Post_Id" });
            DropIndex("dbo.UserPosts", new[] { "User_Id" });
            DropTable("dbo.UserPosts");
            CreateIndex("dbo.Users", "Post_Id");
            CreateIndex("dbo.Posts", "User_Id");
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Users", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "User_Id", "dbo.Users", "Id");
        }
    }
}
