using System.Linq;
using System.Web.Mvc;
using PDiffy.Features.Page;
using PDiffy.Features.Shared;
using PDiffy.Infrastructure;

namespace PDiffy.Features.Differences
{
    public class DifferencesController : Controller
    {
        private readonly IImageStore _imageStore;

        public DifferencesController(IImageStore imageStore)
        {
            _imageStore = imageStore;
        }

        public ActionResult Index()
        {
            var pages = Data.Biggy.PageList.Select(page => new PageViewModel
            {
                Name = page.Name,
                LastComparisonDate = page.LastComparisonDate,
                ComparisonStillValid = page.ComparisonStillValid,
                HumanComparisonRequired = page.HumanComparisonRequired,
                ComparisonExists = !(string.IsNullOrWhiteSpace(page.ComparisonImageUrl) && string.IsNullOrWhiteSpace(page.ComparisonImagePath))
            }).ToList();

            return View("Index", new DifferencesViewModel { Pages = pages, NewPage = new PageViewModel() });
        }

        public ActionResult Approve(string name)
        {
            var page = Data.Biggy.PageList.Single(x => x.Name == name);

            page.Approve();

            Data.Biggy.PageList.Update(page);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string name)
        {
            Data.Biggy.PageList.Remove(Data.Biggy.PageList.Single(x => x.Name == name));
            _imageStore.Delete(name, new[] { Environment.OriginalId, Environment.ComparisonId, Environment.DifferenceId, Environment.LearnId });
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            Data.Biggy.PageList.Clear();
            _imageStore.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}