using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FluentValidation;
using MediatR;
using PDiffy.Features.Shared;
using Quarks;

namespace PDiffy.Text
{
	public class Text
	{
		public class Validator : AbstractValidator<Command>
		{
			public Validator()
			{
				RuleFor(x => x.Name).NotEmpty();
				RuleFor(x => x.Text).NotNull();
			}
		}

		public class Command : IAsyncRequest
		{
			public string Name { get; set; }
			public string Text { get; set; }
		}

		public class CommandHandler : AsyncRequestHandler<Command>
		{
			protected override async Task HandleCore(Command message)
			{
				var textDiffPage = Data.Biggy.TextDiffPageList.SingleOrDefault(x => x.Name == message.Name);

				if (textDiffPage == null)
				{
					Data.Biggy.TextDiffPageList.Add(
						new Data.TextDiffPage
						{
							Name = message.Name,
							OriginalText = message.Text
						});
				}
				else if (textDiffPage.HumanComparisonRequired == false)
				{
					textDiffPage.ComparisonText = message.Text;

					await Task.Run(() =>
					{
						//TODO: write a wrapper around the diff_match_patch, then inject wrapper here
						var differences = new  diff_match_patch().diff_main(textDiffPage.OriginalText, textDiffPage.ComparisonText);

						if (differences.Any())
						{
							textDiffPage.HumanComparisonRequired = true;
							textDiffPage.DifferenceText = HttpUtility.HtmlEncode(new diff_match_patch().diff_prettyHtml(differences));
						}
						else
							textDiffPage.ComparisonText = string.Empty;

						textDiffPage.LastComparisonDate = SystemTime.Now;
					});

					Data.Biggy.TextDiffPageList.Update(textDiffPage);
				}
			}
		}
	}
}