using Arch.Mvc.Models;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITest _test;
        public HomeController(ITest test)
        {
            _test = test;
        }
        public ActionResult Index()
        {
            //_test.Add();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}