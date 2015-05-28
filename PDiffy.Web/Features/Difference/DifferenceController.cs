using System.Linq;
using System.Web.Mvc;
using PDiffy.Web.Features.Page;
using PDiffy.Web.Features.Shared;

namespace PDiffy.Web.Features.Difference
{
    public class DifferenceController : Controller
    {
        private readonly IImageStore _imageStore;

        public DifferenceController(IImageStore imageStore)
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
                HumanComparisonRequired = page.HumanComparisonRequired
            }).ToList();

            return View("Index", new DifferenceViewModel { Pages = pages, NewPage = new PageViewModel() });
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
            //_imageStore.DeleteImage(name);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll()
        {
            Data.Biggy.PageList.Clear();
            //_imageStore.DeleteImages();
            return RedirectToAction("Index");
        }
    }
}