using System.Linq;
using System.Web.Mvc;
using Biggy.Core;
using PDiffy.Web.Data;

namespace PDiffy.Web.Features.History
{
    public class HistoryController : Controller
    {
	    readonly BiggyList<PageModel> _pages;

	    public HistoryController(BiggyList<PageModel> pages)
	    {
		    _pages = pages;
	    }

	    public ActionResult Index(string name)
	    {
		    var page = _pages.Single(x => x.Name == name);

            return View();
        }
    }
}