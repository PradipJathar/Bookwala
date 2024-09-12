using BookwalaWeb.Data;
using BookwalaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookwalaWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create() 
        { 
            return View();
        }
    }
}
