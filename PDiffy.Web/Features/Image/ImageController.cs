using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using PDiffy.Web.Features.Shared;

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
        public FileContentResult DifferenceImage(string name, DateTime lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            if (string.IsNullOrWhiteSpace(page.DifferenceImagePath))
            {
                page.DifferenceImagePath = _imageStore.SaveImage(page.DifferenceImage, page.Name + ".diff");
                Data.Biggy.PageList.Update(page);
            }

            return File(Convert(page.DifferenceImage), "image/png");
        }

        [OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
        public FileContentResult ComparisonImage(string name, DateTime lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            return File(Convert(page.ComparisonImage), "image/png");
        }

        [OutputCache(Duration = 1800, VaryByParam = "name; lastComparisonDate")]
        public FileContentResult OriginalImage(string name, DateTime lastComparisonDate)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            return File(Convert(page.OriginalImage), "image/png");
        }


        static byte[] Convert(Bitmap image)
        {
            return (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
        }
    }
}