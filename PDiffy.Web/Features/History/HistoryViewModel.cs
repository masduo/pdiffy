using System.Collections.Generic;
using PDiffy.Web.Features.Page;

namespace PDiffy.Web.Features.History
{
	public class HistoryViewModel
	{
		public IEnumerable<Shared.Image> DifferenceImages { get; set; }
		public PageViewModel Page { get; set; }
	}
}