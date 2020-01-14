using Arch.Cqrs.Client.Command.ProductOnlyCqrs;
using Arch.Cqrs.Client.ProductOnlyCqrs;
using Arch.Infra.Shared.Cqrs;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProcessor _processor;
        public ProductController(IProcessor processor) => _processor = processor;

        public ActionResult Index() => View(_processor.Get(new GetProductsList(5)));

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(CreateProduct createProduct)
        {
            _processor.Send(createProduct);
            return RedirectToAction("Index");
        }
    }
}