namespace NewsFeed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NewsFeed",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeedName = c.String(nullable: false, maxLength: 50),
                        FeedUrl = c.String(nullable: false, maxLength: 255),
                        CategoryID = c.Int(nullable: false),
                        Subscribed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeed", "CategoryID", "dbo.Category");
            DropIndex("dbo.NewsFeed", new[] { "CategoryID" });
            DropTable("dbo.NewsFeed");
            DropTable("dbo.Category");
        }
    }
}
