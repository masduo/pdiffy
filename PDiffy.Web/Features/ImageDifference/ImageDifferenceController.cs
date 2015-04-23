using System;
using System.Linq;
using System.Web.Mvc;
using Biggy.Core;
using PDiffy.Web.Data;
using PDiffy.Web.Features.Page;
using PDiffy.Web.Features.Shared;
using Quarks;

namespace PDiffy.Web.Features.ImageDifference
{
	public class ImageDifferenceController : Controller
	{
		readonly IImageGenerator _imageGenerator;
		readonly BiggyList<PageModel> _pages;

		public ImageDifferenceController(IImageGenerator imageGenerator, BiggyList<PageModel> pages)
		{
			_imageGenerator = imageGenerator;
			_pages = pages;
		}

		public ActionResult Index()
		{
			var pages = _pages.Select(page => new PageViewModel
			{
				Name = page.Name,
				Url = page.Url,
				LastComparisonDate = page.LastComparisonDate,
				ComparisonStillValid = page.ComparisonStillValid,
				HumanComparisonRequired = page.HumanComparisonRequired,
				OriginalImageUrl = page.OriginalImageUrl,
				ComparisonImageUrl = page.ComparisonImageUrl,
				Build = page.Build
			}).ToList();

			return View("Index", new ImageDifferenceViewModel { Pages = pages, NewPage = new PageViewModel() });
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public FileContentResult DifferenceImage(string name, DateTime lastComparisonDate)
		{
			var page = _pages.Single(x => x.Name == name);
			var data = _imageGenerator.GenerateDifference(page);

			return File(data, "image/png");
		}

		public ActionResult Approve(string name)
		{
			var page = _pages.Single(x => x.Name == name);

			page.OriginalImageUrl = page.ComparisonImageUrl;
			page.ComparisonImageUrl = null;
			page.HumanComparisonRequired = false;
			page.LastComparisonDate = SystemTime.Now;

			_pages.Update(page);

			return RedirectToAction("Index");
		}

		public ActionResult Delete(string name)
		{
			_pages.Remove(_pages.Single(x => x.Name == name));
			return RedirectToAction("Index");
		}

		public ActionResult DeleteAll()
		{
			_pages.Clear();
			return RedirectToAction("Index");
		}
	}
}