namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnsToNewsItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("news.NewsItems", "CreationDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AddColumn("news.NewsItems", "LastModificationDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("news.NewsItems", "LastModificationDate");
            DropColumn("news.NewsItems", "CreationDate");
        }
    }
}
