using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// The code-behind file auto generated when we created a new Razor Page - Empty template
// Holds the C# used in the Suppliers.cshtml razor page

namespace Northwind.web.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<string?> Suppliers { get; set; }

        //When an HTTP GET request is made to this webpage, OnGet() executes.
        public void OnGet()
        {
            ViewData["Title"] = "Northwind B2B - Suppliers";
            Suppliers = new[]
            {
                "Alpha Co", "Beta Limited", "Gamma Corp"
            };
        }
    }
}
