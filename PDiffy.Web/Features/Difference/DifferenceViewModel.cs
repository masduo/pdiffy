using System.Collections.Generic;
using PDiffy.Web.Features.Page;

namespace PDiffy.Web.Features.Difference
{
	public class DifferenceViewModel
	{
		public IReadOnlyCollection<PageViewModel> Pages { get; set; }
		public PageViewModel NewPage { get; set; }
	}
}