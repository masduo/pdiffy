using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using PDiffy.Features.Page;
using PDiffy.Features.Shared;
using Environment = PDiffy.Infrastructure.Environment;

namespace PDiffy.Features.History
{
    public class HistoryController : Controller
    {
        private readonly IImageStore _imageStore;

        public HistoryController(IImageStore imageStore)
        {
            _imageStore = imageStore;
        }

        public ActionResult Index(string name)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            var historyViewModel = new HistoryViewModel
            {
                Page = new PageViewModel
                    {
                        Name = page.Name,
                        LastComparisonDate = page.LastComparisonDate,
                        ComparisonStillValid = page.ComparisonStillValid,
                        HumanComparisonRequired = page.HumanComparisonRequired
                    },
                DifferenceImages = _imageStore.Get(name, Environment.DifferenceId).Select(x => new HistoricalImage(x))
            };

            return View(historyViewModel);
        }

        public ActionResult Learn(string name, DateTime createdDate)
        {
            var differenceImage = _imageStore.Get(name, Environment.DifferenceId).Select(x => new HistoricalImage(x)).Single(x => x.CreatedDate == createdDate);

            Data.Biggy.KnownImageList.Add(new KnownImageModel
            {
                Name = name,
                CreatedDate = createdDate,
                ImagePath = _imageStore.Save(new Bitmap(differenceImage.ImagePath), name + "." + Environment.LearnId)
            });

            return RedirectToAction("Index", new { name });
        }
    }
}