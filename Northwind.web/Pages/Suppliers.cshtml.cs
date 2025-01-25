using Microsoft.AspNetCore.Mvc; // To use the [BindProperty] and IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels; // To use NorthwindContext
// The code-behind file auto generated when we created a new Razor Page - Empty template
// Holds the C# used in the Suppliers.cshtml razor page



namespace Northwind.web.Pages
{
    public class SuppliersModel : PageModel
    {
        private NorthwindContext _db;
        public SuppliersModel(NorthwindContext db) { _db = db; }
        public IEnumerable<Supplier>? Suppliers { get; set; }

        [BindProperty] // This decorator connects HTML elements on the webpage with the Supplier class
        public Supplier? Supplier { get; set; }

        public IActionResult OnPost()
        {
            if (Supplier is not null && ModelState.IsValid)
            {
                _db.Suppliers.Add(Supplier);
                _db.SaveChanges();
                return RedirectToAction("/suppliers");
            }
            else
            {
                return Page(); // Return to the original page
            }
        }
        //When an HTTP GET request is made to this webpage, OnGet() executes.
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";
            Suppliers = _db.Suppliers.OrderBy(c=>c.Country).ThenBy(c=>c.CompanyName);
        }

        
    }
}
