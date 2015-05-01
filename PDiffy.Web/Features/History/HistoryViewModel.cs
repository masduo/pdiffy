using System.Collections.Generic;
using PDiffy.Web.Features.Page;
using PDiffy.Web.Features.Shared;

namespace PDiffy.Web.Features.History
{
	public class HistoryViewModel
	{
		public IEnumerable<Image> DifferenceImages { get; set; }
		public PageViewModel Page { get; set; }
	}
}