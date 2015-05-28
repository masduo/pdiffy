using System.Web.Mvc;
using PDiffy.Web.Infrastructure;

namespace PDiffy.Web.Features.Setup
{
	public class SetupController : Controller
	{
		public ActionResult Index()
		{
			if (!string.IsNullOrWhiteSpace(Environment.ImageStorePath) && !string.IsNullOrWhiteSpace(Environment.DataStorePath))
				return RedirectToAction("Index", "Difference");
			return View();
		}

		public ActionResult Setup(string imageStorePath, string dataStorePath)
		{
			Environment.ImageStorePath = imageStorePath;
			Environment.DataStorePath = dataStorePath;

			return RedirectToAction("Index", "Difference");
		}
	}
}