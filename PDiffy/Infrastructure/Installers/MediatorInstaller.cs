using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MediatR;

namespace PDiffy.Infrastructure.Installers
{
	public class MediatorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IMediator>().ImplementedBy<Mediator>());
			container.Register(Classes.FromThisAssembly().BasedOn(typeof(IRequestHandler<,>)).WithService.AllInterfaces().LifestyleTransient());
			container.Register(Classes.FromThisAssembly().BasedOn(typeof(IAsyncRequestHandler<,>)).WithService.AllInterfaces().LifestyleTransient());
			container.Register(Classes.FromThisAssembly().BasedOn(typeof(INotificationHandler<>)).WithService.AllInterfaces().LifestyleTransient());
			container.Register(Classes.FromThisAssembly().BasedOn(typeof(IAsyncNotificationHandler<>)).WithService.AllInterfaces().LifestyleTransient());
			container.Register(Component.For<SingleInstanceFactory>().UsingFactoryMethod<SingleInstanceFactory>(k => t => k.Resolve(t)));
			container.Register(Component.For<MultiInstanceFactory>().UsingFactoryMethod<MultiInstanceFactory>(k => t => (IEnumerable<object>)k.ResolveAll(t)));
		}
	}
}