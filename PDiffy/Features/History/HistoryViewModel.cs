using System.Collections.Generic;
using PDiffy.Features.Page;

namespace PDiffy.Features.History
{
	public class HistoryViewModel
	{
		public IEnumerable<HistoricalImage> DifferenceImages { get; set; }
		public PageViewModel Page { get; set; }
	}
}