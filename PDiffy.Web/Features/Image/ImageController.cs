using System;
using System.Linq;
using System.Web.Mvc;
using PDiffy.Web.Features.Shared;
using PDiffy.Web.Quarks.ImageExtensions;
using Environment = PDiffy.Web.Infrastructure.Environment;

namespace PDiffy.Web.Features.Image
{
    public class ImageController : Controller
    {
        private readonly IImageStore _imageStore;

        public ImageController(IImageStore imageStore)
        {
            _imageStore = imageStore;
        }

        [OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
        public FileContentResult DifferenceImage(string name, DateTime? lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            if (string.IsNullOrWhiteSpace(page.DifferenceImagePath))
            {
                page.DifferenceImagePath = _imageStore.Save(page.DifferenceImage, page.Name + "." + Environment.DifferenceId);
                Data.Biggy.PageList.Update(page);
            }

            return File(page.DifferenceImage.ToByteArray(), "image/png");
        }

        [OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
        public FileContentResult ComparisonImage(string name, DateTime? lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            return File(page.ComparisonImage.ToByteArray(), "image/png");
        }

        [OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
        public FileContentResult OriginalImage(string name, DateTime? lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            return File(page.OriginalImage.ToByteArray(), "image/png");
        }
    }
}