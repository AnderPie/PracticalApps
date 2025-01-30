using Microsoft.AspNetCore.Mvc; // To use Controller, IActionResult
using Northwind.Mvc.Models; // To use ErrorViewModel
using System.Diagnostics; // To use Activity
using Northwind.EntityModels; // To use NorthwindDB context
using Microsoft.EntityFrameworkCore; // To use Include method

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly NorthwindContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, NorthwindContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult ProductDetail(int? id) // ASP.NET core uses model binding to match the id passed in the route parameter to the named id in this method.
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
            }
            Product? model = _db.Products.Include(p=>p.Category).SingleOrDefault(p=>p.ProductId==id);
            if (model == null)
            {
                return NotFound($"ProduftID {id} not found.");
            }

            return View(model); // Pass model to view and then return the result.
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new(
                VisitorCount: Random.Shared.Next(1, 1001),
                Categories: _db.Categories.ToList(),
                Products: _db.Products.ToList()
            );
            /* demonstration of logging
            _logger.LogError("Oh NOOOOOOOOOOOOOOOOOO");
            _logger.LogWarning("This is your first warning!");
            _logger.LogWarning("This is your second warning!");
            _logger.LogInformation("I am in the Index method of the HomeController");
            */
            return View(model); // Pass the model to the view
            /*
             * Remember the view search convention: when a View() is called, it looks for the Views folder for a subfolder
             * with the same name as the current controller. In this case, Home. Then looks for a file with the name of the current
             * action, Index. 
             * It also checks the Shared folder and the Pages folder.
             */
        }

        // This action will handle GET and other requests except POST
        public IActionResult ModelBinding()
        {
            return View();
        }

        [HttpPost] // This action methodwill handle POST requests
        public IActionResult ModelBinding(Thing thing)
        {
            HomeModelBindingViewModel model = new(
                Thing: thing, HasErrors: !ModelState.IsValid,
                ValidationErrors: ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage)
                );
            return View(model); // Show the model bound thing
        }

        [Route("Private")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
