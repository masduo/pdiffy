using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Biggy.Core;
using Castle.Core.Internal;
using PDiffy.Features.Shared;
using Environment = PDiffy.Infrastructure.Environment;

namespace PDiffy.Features.Page
{
    public class PageController : ApiController
    {
        readonly BiggyList<PageModel> _pages;
        private readonly IImageStore _imageStore;

        public PageController(IImageStore imageStore)
        {
            _pages = Data.Biggy.PageList;
            _imageStore = imageStore;
        }

        [HttpPost]
        public async Task<JsonResult<Status>> Update(string name)
        {
            var status = Status.Ok;
            try
            {
                Bitmap image;
                using (var requestStream = await Request.Content.ReadAsStreamAsync())
                    image = new Bitmap(requestStream);

                var page = _pages.SingleOrDefault(x => x.Name == name);

                if (page == null)
                    _pages.Add(new PageModel { Name = name, OriginalImagePath = _imageStore.Save(image, name + "." + Environment.OriginalId) });
                else if (!page.HumanComparisonRequired)
                {
                    page.ComparisonImagePath = _imageStore.Save(image, name + "." + Environment.ComparisonId);
                    await Task.Run(() => page.GenerateComparison());
                    _pages.Update(page);
                }
                else
                {
                    status = Status.HumanComparisonRequired;
                }
            }
            catch (Exception exception)
            {
                status = new Status
                {
                    Message = !exception.Message.IsNullOrEmpty() ? exception.Message : exception.InnerException.Message
                };
            }

            return Json(status);

        }

        [HttpGet]
        public async Task<JsonResult<Status>> Update(string name, string imageUrl)
        {
            var status = Status.Ok;
            try
            {
                var page = _pages.SingleOrDefault(x => x.Name == name);

                if (page == null)
                    _pages.Add(new PageModel { Name = name, OriginalImageUrl = imageUrl });
                else if (!page.HumanComparisonRequired)
                {
                    page.ComparisonImageUrl = imageUrl;
                    await Task.Run(() => page.GenerateComparison());
                    _pages.Update(page);
                }
                else
                    status = Status.HumanComparisonRequired;
            }
            catch (Exception exception)
            {
                status = new Status
                {
                    Message = !exception.Message.IsNullOrEmpty() ? exception.Message : exception.InnerException.Message
                };
            }

            return Json(status);
        }
    }
}
