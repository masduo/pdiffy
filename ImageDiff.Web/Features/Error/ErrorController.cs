using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageDiff.Web.Features.Error
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