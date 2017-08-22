using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NewsPortal.Web.Startup))]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace NewsPortal.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}