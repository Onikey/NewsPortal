using Autofac;
using Common.Abstractions.Repository;
using Common.DataAccess;
using Common.DataAccess.DataSources;
using Common.IoC.Autofac;
using NewsPortal.Contracts.Repositories;
using NewsPortal.Data;
using NewsPortal.Data.Repositories;

namespace NewsPortal.Integration
{
    public class ComponentRegistrationModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.ConfigureEntityFrameworkDataAccess(builder);
            this.RegisterRepositories(builder);
        }

        private void ConfigureEntityFrameworkDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<NewsContext>();

            //// **** Register EFDS for each source type ****
            builder.Register((x, y) => new EntityFrameworkDataSource(x.Resolve<NewsContext>()))
                .Keyed<IDataSource>(DataSourceType.NewsEF);

            builder.Register((x, y) => new GenericRepository(x.ResolveKeyed<IDataSource>(DataSourceType.NewsEF)))
                .Keyed<GenericRepository>(DataSourceType.NewsEF);

            builder.Register((x, y) => new GenericRepository(x.ResolveKeyed<IDataSource>(DataSourceType.NewsEF)))
                .Keyed<IGenericRepository>(DataSourceType.NewsEF)
                .As<IGenericRepository>();

            builder.Register((x, y) => new GenericRepository(x.ResolveKeyed<IDataSource>(DataSourceType.NewsEF)))
                .Keyed<IAsyncGenericRepository>(DataSourceType.NewsEF)
                .As<IAsyncGenericRepository>();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.Register((x, y) => new NewsItemRepository(x.ResolveKeyed<IAsyncGenericRepository>(DataSourceType.NewsEF)))
                .As<INewsItemRepository>();

            builder.Register((x, y) => new AuthorRepository(x.ResolveKeyed<IAsyncGenericRepository>(DataSourceType.NewsEF)))
                .As<IAuthorRepository>();
        }
    }
}
