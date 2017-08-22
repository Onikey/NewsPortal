using Common.IoC.Autofac;
using Common.IoC.Common;
using System.Web.Http;

namespace NewsPortal.Web.Controllers.Api
{
    public class BaseController : ApiController
    {
        protected IDependencyResolver DependencyResolver
        {
            get
            {
                return AutofacService.Instance.Resolve<IDependencyResolver>();
            }
        }
    }
}
