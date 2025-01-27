using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels;

namespace Northwind.web.Pages
{
    public class OrdersForCustomerModel : PageModel
    {
        private NorthwindContext _db;
        private string? _customerID;  // Need to add null checks
        public string? _customerName;
        public Order[] Orders { get; set; } = null!;
        public OrdersForCustomerModel(NorthwindContext db)
        {
            _db = db;
            
        }
        public void OnGet()
        {
            _customerID = HttpContext.Request.Query["id"];
            _customerName = _db.Customers?.Where(c=>c.CustomerId==_customerID)?.Select(c =>c.ContactName)?.First()?.ToString();
            ViewData["Title"] = $"Northwind B2B - Orders for {_customerID}";
            Orders = _db.Orders.Where(o=>o.CustomerId==_customerID).OrderBy(o => o.OrderDate).ToArray();
        }
    }
}
