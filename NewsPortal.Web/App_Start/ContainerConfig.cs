using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Common.IoC.Autofac;
using System.Web.Http;
using System.Web.Mvc;

namespace NewsPortal.Web
{
    public class ContainerConfig
    {
        public static void Configure()
        {
            AutofacService.Instance.AddAssemblyWithModules(typeof(NewsPortal.Integration.ComponentRegistrationModule).Assembly);
            AutofacService.Instance.AddAssemblyWithModules(typeof(ComponentRegistrationModule).Assembly);
            AutofacService.Instance.Initialize();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacService.Instance.Container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacService.Instance.Container));
        }
    }
}