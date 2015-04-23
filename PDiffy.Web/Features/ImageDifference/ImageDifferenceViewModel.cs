using System.Collections.Generic;
using PDiffy.Web.Features.Page;

namespace PDiffy.Web.Features.ImageDifference
{
	public class ImageDifferenceViewModel
	{
		public IList<PageViewModel> Pages { get; set; }
		public PageViewModel NewPage { get; set; }
	}
}