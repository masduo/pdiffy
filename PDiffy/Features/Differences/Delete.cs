using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PDiffy.Data.Stores;
using PDiffy.Infrastructure;

namespace PDiffy.Features.Differences
{
	public class Delete
	{
		public class Validator : AbstractValidator<Command>
		{
			public Validator()
			{
				RuleFor(x => x.Name).NotEmpty();
			}
		}

		public class Command : IAsyncRequest
		{
			public string Name { get; set; }
		}

		public class CommandHandler : AsyncRequestHandler<Command>
		{
			readonly IImageStore _imageStore;

			public CommandHandler(IImageStore imageStore)
			{
				_imageStore = imageStore;
			}

			protected override async Task HandleCore(Command message)
			{
				Data.Biggy.PageList.Remove(Data.Biggy.PageList.Single(x => x.Name == message.Name));
				_imageStore.Delete(message.Name, Environment.AllImageTypes);
			}
		}
	}
}