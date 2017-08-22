namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuthors : DbMigration
    {
        public override void Up()
        {
            DropColumn("news.Authors", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("news.Authors", "BirthDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
    }
}
