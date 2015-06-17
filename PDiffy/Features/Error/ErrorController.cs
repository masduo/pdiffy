using System.Web.Mvc;

namespace PDiffy.Features.Error
{
	public class ErrorController : Controller
	{
		public ActionResult NotFound()
		{
			return View("404");
		}

		public ActionResult ServiceUnavailable()
		{
			return View("500");
		}
	}
}