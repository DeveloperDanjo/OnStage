using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using OnStage.Persistence.Concrete;
using OnStage.Presentation.CompositionRoot.Infrastructure;
using OnStage.Business.Concrete;
using Microsoft.AspNet.SignalR;
using AutofacMvc = Autofac.Integration.Mvc;
using OnStageInf = OnStage.Presentation.CompositionRoot.Infrastructure;

namespace OnStage.Presentation.CompositionRoot
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void InitializeAutofac()
        {
            // Make sure that the ApplicationModel assembly gets loaded. For some reason it doesn't always happen (?)
            Assembly.GetAssembly(typeof(OnStage.Presentation.ApplicationModel.Controllers.HomeController));
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(assemblies);
            builder.RegisterModelBinders(assemblies);

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ShowHandler)))
                .Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(ShowRepository)))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterModule(new EntityFrameworkModule());
            builder.RegisterModule(new SignalRHubModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacMvc.AutofacDependencyResolver(container));
            GlobalHost.DependencyResolver = new SignalRAutofacDependencyResolver(container);
        }

        public static void GenerateDefaultData()
        {
            DefaultDataCreator.CreateDefaultData();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHubs();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            InitializeAutofac();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            GenerateDefaultData();
        }
    }
}