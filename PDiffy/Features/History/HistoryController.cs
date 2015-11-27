using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;

namespace PDiffy.Features.History
{
	public partial class HistoryController : Controller
	{
		readonly IMediator _mediator;

		public HistoryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public virtual async Task<ActionResult> Index(Index.Query query)
		{
			var model = await _mediator.SendAsync(query);

			return View(MVC.History.Views.Index, model);
		}

		public virtual async Task<ActionResult> Learn(Learn.Command model)
		{
			await _mediator.SendAsync(model);

			return RedirectToAction(MVC.History.Index());
		}
	}
}