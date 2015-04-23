using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ImageDiff.Web.Features.Tools;

namespace ImageDiff.Web.Infrastructure.Installers
{
	public class ToolInstallers : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IImageDiffTool>().ImplementedBy<ImageDiffTool>().LifestyleTransient(),
				Component.For<ICapture>().ImplementedBy<Capture>().LifestyleTransient());
		}
	}
}