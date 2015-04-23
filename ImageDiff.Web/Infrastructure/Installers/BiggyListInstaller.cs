using Biggy.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ImageDiff.Web.Data;
using ImageDiff.Web.Infrastructure.Factories;

namespace ImageDiff.Web.Infrastructure.Installers
{
	public class BiggyListInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<BiggyList<PageModel>>().UsingFactoryMethod(BiggyPageListFactory.Create).LifestylePerWebRequest());
		}
	}
}