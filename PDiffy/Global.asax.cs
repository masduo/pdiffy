using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using PDiffy.Infrastructure.Factories;
using PDiffy.Infrastructure.WebApiResolver;

namespace PDiffy
{
    public class MvcApplication : HttpApplication
    {
		static IWindsorContainer _container;

        protected void Application_Start()
        {
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
            AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			_container = new WindsorContainer().Install(FromAssembly.This());
			var controllerFactory = new WindsorControllerFactory(_container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
			GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(_container);
        }

		protected void Application_End()
		{
			_container.Dispose();
		}
    }
}
