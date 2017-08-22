using NewsPortal.Core.Model;

namespace NewsPortal.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsPortal.Data.NewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewsPortal.Data.NewsContext context)
        {
            var author = Author.Create("Vitaliy", "Zubritsky", new DateTime(1996, 9, 6));
            var newsItem = NewsItem.Create("Title", "test content", author);

            context.Set<Author>().AddOrUpdate(x => x.LastName, author);
            context.SaveChanges();

            context.Set<NewsItem>().AddOrUpdate(x => x.Title, newsItem);
            context.SaveChanges();
        }
    }
}
