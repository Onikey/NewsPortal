using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Common.IoC.Autofac;
using Common.IoC.Common;

namespace NewsPortal.Web
{
    public class ComponentRegistrationModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutofacService.Instance)
            .As<IDependencyResolver>()
            .SingleInstance();

            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterApiControllers(typeof(Global).Assembly);
        }
    }
}