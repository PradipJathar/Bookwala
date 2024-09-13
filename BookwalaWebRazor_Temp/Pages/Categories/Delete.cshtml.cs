using BookwalaWebRazor_Temp.Data;
using BookwalaWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwalaWebRazor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        ApplicationDbContext db;

        public DeleteModel(ApplicationDbContext context)
        {
            db = context;
        }

        [BindProperty]
        public Category Category { get; set; }


        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            Category? category = db.Categories.Find(Category.Id);

            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
