using System.Collections.Generic;
using ImageDiff.Web.Features.Page;

namespace ImageDiff.Web.Features.ImageDifference
{
	public class ImageDifferenceViewModel
	{
		public IList<PageViewModel> Pages { get; set; }
		public PageViewModel NewPage { get; set; }
	}
}