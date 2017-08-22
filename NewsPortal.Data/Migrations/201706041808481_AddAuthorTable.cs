namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("news.NewsItems", "AuthorId", c => c.Guid(nullable: false));
            CreateIndex("news.NewsItems", "AuthorId");

            CreateTable(
                "news.Authors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddForeignKey("news.NewsItems", "AuthorId", "news.Authors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("news.NewsItems", "AuthorId", "news.Authors");
            DropIndex("news.NewsItems", new[] { "AuthorId" });
            DropColumn("news.NewsItems", "AuthorId");
            DropTable("news.Authors");
        }
    }
}
