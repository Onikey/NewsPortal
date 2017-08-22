using Common.Abstractions.Entities;
using NewsPortal.Core.Model;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace NewsPortal.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext()
            : base(nameOrConnectionString: ConnectionStringName) { }

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["NewsPortalDb"] != null)
                {
                    return ConfigurationManager.ConnectionStrings["NewsPortalDb"].ToString();
                }

                return "DefaultConnection";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2").HasPrecision(0));

            modelBuilder.Types().Where(t => t.GetInterfaces().Contains(typeof(IHaveTrackingState))).Configure(x => x.Ignore("TrackingState"));

            modelBuilder.Entity<NewsItem>()
                .ToTable("NewsItems", "news")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Author>()
                .ToTable("Authors", "news")
                .HasKey(x => x.Id);
        }
    }

}