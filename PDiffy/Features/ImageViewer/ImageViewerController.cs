using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using MediatR;

namespace PDiffy.Features.ImageViewer
{
	public partial class ImageViewerController : Controller
	{
		readonly IMediator _mediator;

		public ImageViewerController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> DifferenceImage(string name, string page, string site)
		{
			var model = await _mediator.SendAsync(new DifferenceImage.Query { Name = name, Page = page, Site = site });
			return File(model.ImageData, "image/png");
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> ComparisonImage(string name, string page, string site)
		{
			var model = await _mediator.SendAsync(new ComparisonImage.Query { Name = name, Page = page, Site = site });
			return File(model.ImageData, "image/png");
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> OriginalImage(string name, string page, string site)
		{
			var model = await _mediator.SendAsync(new OriginalImage.Query { Name = name, Page = page, Site = site });
			return File(model.ImageData, "image/png");
		}
	}
}