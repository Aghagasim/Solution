namespace WeekendTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialUPDATED2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Fullname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Fullname", c => c.String());
            DropColumn("dbo.Users", "Name");
        }
    }
}
