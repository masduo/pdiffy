using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using MediatR;
using PDiffy.Features.Page;

namespace PDiffy.Text
{
	public class TextController : ApiController
	{
		readonly IMediator _mediator;

		public TextController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<JsonResult<Status>> Update(string name)
		{
			if (string.IsNullOrEmpty(name))
				return Json(Status.GetWrongInput(name));

			string text;
			using (var requestStream = await Request.Content.ReadAsStreamAsync())
			using (var streamReader = new StreamReader(requestStream, Encoding.Unicode, true))
				text = streamReader.ReadToEnd();

			await _mediator.SendAsync(new Text.Command { Name = name, Text = text });

			return Json(Status.Ok);
		}

		//[HttpGet]
		//public async Task<JsonResult<Status>> Update(string name, string imageUrl)
		//{
		//	await _mediator.SendAsync(new WebImage.Command { Name = name, Url = imageUrl });

		//	return Json(Status.Ok);
		//}
	}
}
