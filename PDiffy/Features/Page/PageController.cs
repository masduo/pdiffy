using System.Drawing;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using MediatR;

namespace PDiffy.Features.Page
{
	public class PageController : ApiController
	{
		readonly IMediator _mediator;

		public PageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<JsonResult<Status>> Update(string name)
		{
			if (string.IsNullOrEmpty(name))
				return Json(Status.GetWrongInput(name));

			Bitmap image;
			using (var requestStream = await Request.Content.ReadAsStreamAsync())
				image = new Bitmap(requestStream);

			await _mediator.SendAsync(new Image.Command { Name = name, Image = image });

			return Json(Status.Ok);
		}

		[HttpGet]
		public async Task<JsonResult<Status>> Update(string name, string imageUrl)
		{
			await _mediator.SendAsync(new WebImage.Command { Name = name, Url = imageUrl });

			return Json(Status.Ok);
		}
	}
}
