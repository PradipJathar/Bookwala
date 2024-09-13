using Bookwala.DataAccess.Data;
using Bookwala.Models;
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

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();

                TempData["success"] = "Category created successfully.";

                return RedirectToAction("Index");
            }

            return View();
        }
        
        public IActionResult Edit(int? id) 
        { 
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                db.SaveChanges();

                TempData["success"] = "Category updated successfully.";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            TempData["success"] = "Category deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
