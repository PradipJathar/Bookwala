using BookwalaWebRazor_Temp.Data;
using BookwalaWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwalaWebRazor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        ApplicationDbContext db;

        public EditModel(ApplicationDbContext context)
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

            if (ModelState.IsValid)
            {
                db.Categories.Update(Category);
                db.SaveChanges();

                return RedirectToPage("Index");
            }

            return Page();            
        }

    }
}
