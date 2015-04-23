using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace ImageDiff.Web.Infrastructure.WebApiResolver
{
	public class WindsorDependencyResolver :  IDependencyResolver
	{
		private readonly IWindsorContainer _container;

		public WindsorDependencyResolver(IWindsorContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");
			_container = container;
		}
		
		public object GetService(Type serviceType)
		{
			return _container.Kernel.HasComponent(serviceType) ? _container.Kernel.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _container.Kernel.ResolveAll(serviceType).Cast<object>().ToArray();
		}

		public IDependencyScope BeginScope()
		{
			return new WindsorDependencyScope(_container.Kernel);
		}

		public void Dispose()
		{
			_container.Dispose();
		}
	}
}