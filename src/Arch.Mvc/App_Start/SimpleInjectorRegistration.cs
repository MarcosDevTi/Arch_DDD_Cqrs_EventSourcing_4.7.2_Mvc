using Arch.Infra.IoC;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Lifestyles;
using StackExchange.Profiling;
using StackExchange.Profiling.Mvc;
using System.Web;
using System.Web.Mvc;

namespace Arch.Mvc
{
    public class SimpleInjectorInitializer
    {
        private static Container _container;

        public static void Initialize(Container container)
        {
            _container = container;
            var hybrid = Lifestyle.CreateHybrid(() => HttpContext.Current != null,
                new WebRequestLifestyle(),
                new ThreadScopedLifestyle());

            _container.Options.DefaultScopedLifestyle = hybrid;

            InitializeContainer();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));
        }

        private static void InitializeContainer()
        {
            ArchBootstrapper.Register(_container);

            MiniProfiler.Configure(new MiniProfilerOptions()
            {
                RouteBasePath = "~/miniprofiler",
                PopupRenderPosition = RenderPosition.BottomLeft
            })
            .AddViewProfiling()
            .AddEntityFramework();

        }


    }
}