using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace PDiffy.Features.TextDifferences
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
			protected override async Task HandleCore(Command message)
			{
				Data.Biggy.TextComparisons.Remove(Data.Biggy.TextComparisons.Single(x => x.Name == message.Name));
			}
		}
	}
}