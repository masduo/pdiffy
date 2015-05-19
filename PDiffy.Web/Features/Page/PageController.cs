using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Biggy.Core;
using Castle.Core.Internal;
using PDiffy.Web.Data;
using PDiffy.Web.Features.Shared;

namespace PDiffy.Web.Features.Page
{
	public class PageController : ApiController
	{
		readonly BiggyList<PageModel> _pages;
		readonly IImageGenerator _imageGenerator;

		public PageController(IImageGenerator imageGenerator)
		{
			_pages = Data.Biggy.PageList;
			_imageGenerator = imageGenerator;
		}

		[HttpGet]
		public JsonResult<Status> Update(string name, string imageUrl, int build = 0) //TODO add error codes
		{
			var success = true;
			string message = null;
			try
			{
				var page = _pages.SingleOrDefault(x => x.Name == name);

				if (page == null)
					_pages.Add(new PageModel { Name = name, OriginalImageUrl = imageUrl, Build = build });
				else if (!page.HumanComparisonRequired)
				{
					page.ComparisonImageUrl = imageUrl;
					page.Build = build;
					_imageGenerator.GenerateComparison(page);
					_pages.Update(page);
				}
				else
				{
					success = false;
					message = "Human comparison is required before any more difference images can be generated";
				}
			}
			catch (Exception exception)
			{
				success = false;
				message = !exception.Message.IsNullOrEmpty() ? exception.Message : exception.InnerException.Message;
			}

			return Json(new Status { Success = success, Message = message });
		}
	}
}
