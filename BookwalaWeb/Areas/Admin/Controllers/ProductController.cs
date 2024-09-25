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
        private readonly IUnitOfWork _UnitOfWork;

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Product> products = _UnitOfWork.Product.GetAll(includeProperties: "Category").ToList();

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
                string wwwRootPath = _WebHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
                    {
                        // Delete old image

                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productViewModel.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _UnitOfWork.Product.Update(productViewModel.Product);
                }

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


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _UnitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = products });
        }

        public IActionResult Delete(int? id)
        {
            Product product = _UnitOfWork.Product.Get(m => m.Id == id);

            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            string oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _UnitOfWork.Product.Remove(product);
            _UnitOfWork.Save();

            return Json(new { success = true, message = "Delete Successfull." });
        }

        #endregion
    }
}
