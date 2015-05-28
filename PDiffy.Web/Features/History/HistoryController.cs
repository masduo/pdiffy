using System;
using System.Drawing;
using System.Globalization;
using System.IO;
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
	    private readonly IImageStore _imageStore;
	    readonly BiggyList<PageModel> _pages;

		public HistoryController(IImageStore imageStore)
		{
		    _imageStore = imageStore;
		    _pages = Data.Biggy.PageList;
		}

	    public ActionResult Index(string name)
		{
			var page = _pages.Single(x => x.Name == name);

			var differenceImages = _imageStore.GetImages(name, "diff").Select(x => new Shared.Image
			{
				CreatedDate =
					DateTime.ParseExact(Path.GetFileName(x).Split('.')[2], "yyyyMMdd-HHmmss", CultureInfo.InvariantCulture),
				ImageString = "data:image/png;base64," + Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(new Bitmap(x), typeof(byte[])))
			});

			var historyViewModel = new HistoryViewModel
			{
				Page = new PageViewModel
					{
						Name = page.Name,
						LastComparisonDate = page.LastComparisonDate,
						ComparisonStillValid = page.ComparisonStillValid,
						HumanComparisonRequired = page.HumanComparisonRequired
					},
				DifferenceImages = differenceImages
			};

			return View(historyViewModel);
		}
	}
}