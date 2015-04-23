using Biggy.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PDiffy.Web.Data;
using PDiffy.Web.Infrastructure.Factories;

namespace PDiffy.Web.Infrastructure.Installers
{
	public class BiggyListInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<BiggyList<PageModel>>().UsingFactoryMethod(BiggyPageListFactory.Create).LifestylePerWebRequest());
		}
	}
}