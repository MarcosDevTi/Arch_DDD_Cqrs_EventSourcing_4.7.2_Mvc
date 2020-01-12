﻿
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
            var miniprofilerDb = "Data Source=(localdb)\\MSSQLLocalDB;Catalog=ArchDatabase4;Trusted_Connection=True;Application Name=Investment Manager MiniProfiler";

            MiniProfiler.Configure(new MiniProfilerOptions()
            {
                //Storage = new SqlServerStorage(miniprofilerDb),

                // Sets up the route to use for MiniProfiler resources:
                RouteBasePath = "~/miniprofiler",
                PopupRenderPosition = RenderPosition.BottomLeft
            })
            .AddViewProfiling()
            .AddEntityFramework();
           
        }

      
    }
}