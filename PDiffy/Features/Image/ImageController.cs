using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;

namespace PDiffy.Features.Image
{
	public partial class ImageController : Controller
	{
		readonly IMediator _mediator;

		public ImageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> DifferenceImage(string name, DateTime? lastComparisonDate)
		{
			var model = await _mediator.SendAsync(new DifferenceImage.Query { Name = name });
			return File(model.ImageData, "image/png");
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> ComparisonImage(string name, DateTime? lastComparisonDate)
		{
			var model = await _mediator.SendAsync(new ComparisonImage.Query { Name = name });
			return File(model.ImageData, "image/png");
		}

		[OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
		public async Task<FileContentResult> OriginalImage(string name, DateTime? lastComparisonDate)
		{
			var model = await _mediator.SendAsync(new OriginalImage.Query { Name = name });
			return File(model.ImageData, "image/png");
		}
	}
}