using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace PDiffy.Features.TextDifferences
{
	public class Index
	{
		public class Query : IAsyncRequest<IList<Model>> { }

		public class Model
		{
			public string Name { get; set; }

			public DateTime? LastComparisonDate { get; set; }
			public bool HumanComparisonRequired { get; set; }

			public bool ComparisonExists { get; set; }
			public bool ComparisonStillValid { get; set; }

			public string OriginalText { get; set; }
			public string ComparisonText { get; set; }
			public string DifferenceText { get; set; }
		}

		public class QueryHandler : IAsyncRequestHandler<Query, IList<Model>>
		{
			public async Task<IList<Model>> Handle(Query message)
			{
				var pages = Data.Biggy.TextComparisons.Select(page => new Model
				{
					Name = page.Name,
					LastComparisonDate = page.LastComparisonDate,
					ComparisonStillValid = page.ComparisonStillValid,
					HumanComparisonRequired = page.HumanComparisonRequired,
					ComparisonExists = string.IsNullOrWhiteSpace(page.DifferenceText) == false,

					OriginalText = page.OriginalText,
					ComparisonText = page.ComparisonText,
					DifferenceText = page.DifferenceText,
				}).ToList();

				return pages;
			}
		}
	}
}