using Bookwala.DataAccess.Repository.IRepository;
using Bookwala.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookwalaWeb.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            IEnumerable<Product> products = _UnitOfWork.Product.GetAll(includeProperties: "Category");

            return View(products);
        }

        public IActionResult Details(int productId)
        {
            Product product = _UnitOfWork.Product.Get(m => m.Id == productId, includeProperties: "Category");

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
