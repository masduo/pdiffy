using System.Linq;
using System.Web.Mvc;
using Biggy.Core;
using PDiffy.Web.Data;
using PDiffy.Web.Features.Page;
using PDiffy.Web.Features.Shared;

namespace PDiffy.Web.Features.History
{
	public class HistoryController : Controller
	{
		readonly BiggyList<PageModel> _pages;
		private readonly IImageGenerator _imageGenerator;

		public HistoryController(IImageGenerator imageGenerator)
		{
			_pages = Data.Biggy.PageList;
			_imageGenerator = imageGenerator;
		}

		public ActionResult Index(string name)
		{
			var page = _pages.Single(x => x.Name == name);

			var differenceImages = _imageGenerator.GenerateDifferences(page.Name);

			var historyViewModel = new HistoryViewModel
			{
				Page = new PageViewModel
					{
						Name = page.Name,
						LastComparisonDate = page.LastComparisonDate,
						ComparisonStillValid = page.ComparisonStillValid,
						HumanComparisonRequired = page.HumanComparisonRequired,
						OriginalImageUrl = page.OriginalImageUrl,
						ComparisonImageUrl = page.ComparisonImageUrl,
						Build = page.Build
					},
				DifferenceImages = differenceImages
			};

			return View(historyViewModel);
		}
	}
}