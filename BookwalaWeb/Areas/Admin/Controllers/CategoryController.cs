using Bookwala.DataAccess.Data;
using Bookwala.DataAccess.Repository.IRepository;
using Bookwala.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookwalaWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categories = _UnitOfWork.Category.GetAll().ToList();

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
                _UnitOfWork.Category.Add(category);
                _UnitOfWork.Save();

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

            Category? category = _UnitOfWork.Category.Get(m => m.Id == id);

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
                _UnitOfWork.Category.Update(category);
                _UnitOfWork.Save();

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

            Category? category = _UnitOfWork.Category.Get(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _UnitOfWork.Category.Get(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _UnitOfWork.Category.Remove(category);
            _UnitOfWork.Save();

            TempData["success"] = "Category deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
