namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "news.NewsItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("news.NewsItems");
        }
    }
}
