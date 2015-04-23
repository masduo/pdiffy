using System.Web.Mvc;

namespace PDiffy.Web.Features.Error
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