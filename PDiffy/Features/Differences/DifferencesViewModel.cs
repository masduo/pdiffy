using System.Collections.Generic;
using PDiffy.Features.Page;

namespace PDiffy.Features.Differences
{
	public class DifferencesViewModel
	{
		public IReadOnlyCollection<PageViewModel> Pages { get; set; }
		public PageViewModel NewPage { get; set; }
	}
}