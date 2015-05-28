using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PDiffy.Web.Features.Shared;

namespace PDiffy.Web.Infrastructure.Installers
{
	public class SharedInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IImageStore>().ImplementedBy<ImageStore>().LifestyleTransient());
		}
	}
}