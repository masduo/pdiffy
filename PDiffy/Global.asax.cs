using System;
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

		void Application_Error(object sender, EventArgs e)
		{
			if (Request.IsLocal)
				return;

			var lastError = Server.GetLastError();
			var httpStatusCode = 500;

			if ((lastError is HttpException))
				httpStatusCode = (lastError as HttpException).GetHttpCode();

			Response.Clear();
			Server.ClearError();
			Response.TrySkipIisCustomErrors = true;

			var path = Request.Path;
			Context.Server.TransferRequest(string.Format("~/Error/Http{0}", httpStatusCode), false);
			Context.RewritePath(path, false);
		}

		protected void Application_End()
		{
			_container.Dispose();
		}
    }
}
