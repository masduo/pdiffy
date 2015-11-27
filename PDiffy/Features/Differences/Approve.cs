using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Quarks;

namespace PDiffy.Features.Differences
{
	public class Approve
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
				var page = Data.Biggy.PageList.Single(x => x.Name == message.Name);

				page.OriginalImageUrl = page.ComparisonImageUrl;
				page.OriginalImagePath = page.ComparisonImagePath;
				page.ComparisonImageUrl = null;
				page.ComparisonImagePath = null;
				page.DifferenceImagePath = null;
				page.HumanComparisonRequired = false;
				page.LastComparisonDate = SystemTime.Now;

				Data.Biggy.PageList.Update(page);
			}
		}
	}
}