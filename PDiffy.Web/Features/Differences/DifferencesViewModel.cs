using System.Collections.Generic;
using PDiffy.Web.Features.Page;

namespace PDiffy.Web.Features.Differences
{
	public class DifferencesViewModel
	{
		public IReadOnlyCollection<PageViewModel> Pages { get; set; }
		public PageViewModel NewPage { get; set; }
	}
}