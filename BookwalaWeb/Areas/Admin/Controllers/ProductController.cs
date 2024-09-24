using Bookwala.DataAccess.Repository.IRepository;
using Bookwala.Models;
using Bookwala.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Abstractions;
using System.Collections.Generic;

namespace BookwalaWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            List<Product> products = _UnitOfWork.Product.GetAll().ToList();

            return View(products);
        }
                

        public IActionResult Upsert(int? id) 
        {
            IEnumerable<SelectListItem> categoryList = _UnitOfWork.Category.GetAll().Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            });

            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = categoryList
            };

            if (id == null || id == 0)
            {
                // Create
                return View(productViewModel);
            }
            else
            {
                // Update
                productViewModel.Product = _UnitOfWork.Product.Get(m => m.Id == id);

                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file) 
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.Add(productViewModel.Product);
                _UnitOfWork.Save();

                TempData["success"] = "Prodcut created successfully.";

                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = _UnitOfWork.Category.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });

                return View(productViewModel);
            }
        }

                
        public IActionResult Delete(int? id) 
        { 
            if(id == null || id == 0)
            {
                return NotFound(); 
            }

            Product? product = _UnitOfWork.Product.Get(m => m.Id ==id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _UnitOfWork.Product.Get(m => m.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            _UnitOfWork.Product.Remove(product);
            _UnitOfWork.Save();

            TempData["success"] = "Product deleted successfully.";

            return RedirectToAction("Index");
        }

    }
}
