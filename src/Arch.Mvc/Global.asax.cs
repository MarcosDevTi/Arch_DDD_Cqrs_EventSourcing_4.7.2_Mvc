using SimpleInjector;
using StackExchange.Profiling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Arch.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorInitializer.Initialize(new Container());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler.StartNew();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Current?.Stop();
        }
    }
}
