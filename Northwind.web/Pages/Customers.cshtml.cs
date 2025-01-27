using Microsoft.AspNetCore.Mvc; // To use the [BindProperty] and IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels; // To use NorthwindContext
// The code-behind file auto generated when we created a new Razor Page - Empty template
// Holds the C# used in the Suppliers.cshtml razor page



namespace Northwind.web.Pages
{
    public class CustomersModel : PageModel
    {
        private NorthwindContext _db;
        public CustomersModel(NorthwindContext db) { _db = db; }
        public IEnumerable<Customer>? Customers { get; set; }
        public IEnumerable<Order>? Orders { get; set; }

        [BindProperty] // This decorator connects HTML elements on the webpage with the Supplier class
        public Customer? Customer{ get; set; }

        public IActionResult OnPost()
        {
            if (Customer is not null && ModelState.IsValid)
            {
                _db.Customers.Add(Customer);
                _db.SaveChanges();
                return RedirectToAction("/customers");
            }
            else
            {
                return Page(); // Return to the original page
            }
        }
        //When an HTTP GET request is made to this webpage, OnGet() executes.
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Customers";
            Customers = _db.Customers.OrderBy(c=>c.Country).ThenBy(c=>c.CompanyName).ThenBy(c=>c.ContactName);
            Orders = _db.Orders.OrderBy(o => o.OrderDate);
        }

        
    }
}
