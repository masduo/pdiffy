using System.Net;
using System.Web.Mvc;

namespace PDiffy.Features.Error
{
	public partial class ErrorController : Controller
	{
		public virtual ActionResult Http404()
		{
			Response.StatusCode = (int)HttpStatusCode.NotFound;
			return View(MVC.Error.Views._404);
		}

		public virtual ActionResult Http500()
		{
			Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
			return View(MVC.Error.Views._500);
		}
	}
}