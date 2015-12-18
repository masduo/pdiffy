using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using PDiffy.Features.Shared;
using Quarks;

namespace PDiffy.Features.Page
{
	public class WebImage
	{
		public class Validator : AbstractValidator<Image.Command>
		{
			public Validator()
			{
				RuleFor(x => x.Name).NotEmpty();
				RuleFor(x => x.Image).NotNull();
			}
		}

		public class Command : IAsyncRequest
		{
			public string Name { get; set; }
			public string Url { get; set; }
		}

		public class CommandHandler : AsyncRequestHandler<Command>
		{
			protected override async Task HandleCore(Command message)
			{
				var page = Data.Biggy.PageList.SingleOrDefault(x => x.Name == message.Name);

				if (page == null)
					Data.Biggy.PageList.Add(new Data.ImageResource { Name = message.Name, OriginalImageUrl = message.Url });
				else if (!page.HumanComparisonRequired)
				{
					page.ComparisonImageUrl = message.Url;
					await Task.Run(() =>
					{
						var equal = new ImageDiffTool().Compare(page.OriginalImage, page.ComparisonImage);

						if (!equal)
							page.HumanComparisonRequired = true;
						else
						{
							page.ComparisonImageUrl = null;
							page.ComparisonImagePath = null;
						}

						page.LastComparisonDate = SystemTime.Now;
					});
					Data.Biggy.PageList.Update(page);
				}
			}
		}
	}
}