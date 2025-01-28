using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels;
using System.Xml.Serialization;

namespace Northwind.web.Pages
{
    public class OrdersForCustomerModel : PageModel
    {
        private NorthwindContext _db;
        private string? _customerID;  // Need to add null checks
        public string? _customerName;
        public Order[] Orders { get; set; } = null!;
        //public List<OrderDetail> OrderDetails { get; set; }
        public OrderDetail[] OrderDetails { get; set; } = null!;
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
            // Seems inefficient, how else might I do this?
            foreach(Order o in Orders)
            {
                if(OrderDetails is null)
                {
                    OrderDetails = _db.OrderDetails.Where(z => z.OrderId == o.OrderId).OrderBy(z=>(float)z.UnitPrice*z.Quantity).ToArray();
                }
                else
                {
                    OrderDetails = OrderDetails.Concat(_db.OrderDetails.Where(z => z.OrderId == o.OrderId).ToArray()).ToArray();
                }
            }
        }

        public string GetProductName(int productID)
        {
            return _db.Products.Where(p=>p.ProductId == productID).Select(p=>p.ProductName).FirstOrDefault() ?? "Product not found.";
        }
    }
}
