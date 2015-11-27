using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;

namespace PDiffy.Features.Differences
{
	public partial class DifferencesController : Controller
    {
	    readonly IMediator _mediator;

	    public DifferencesController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

		public virtual async Task<ActionResult> Index(Index.Query query)
	    {
		    var model = await _mediator.SendAsync(query);

            return View(MVC.Differences.Views.Index, model);
        }

		public virtual async Task<ActionResult> Approve(Approve.Command model)
	    {
		    await _mediator.SendAsync(model);

		    return RedirectToAction(MVC.Differences.Index());
	    }

		public virtual async Task<ActionResult> Delete(Delete.Command model)
	    {
		    await _mediator.SendAsync(model);

			return RedirectToAction(MVC.Differences.Index());
	    }
    }
}