﻿using BookwalaWeb.Data;
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

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Display Order cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
