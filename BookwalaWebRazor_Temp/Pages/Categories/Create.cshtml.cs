using BookwalaWebRazor_Temp.Data;
using BookwalaWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwalaWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        ApplicationDbContext db;

        public CreateModel(ApplicationDbContext context)
        {
            db = context;
        }

        public Category Category { get; set; }


        public void OnGet()
        {
        }
    }
}
