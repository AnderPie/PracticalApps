#region using statements
using Microsoft.AspNetCore.Identity; // To use IdentityUser
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Data;
using Northwind.EntityModels; // To use AddNorthwindContext()
#endregion

#region Instantiate builder and add services
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add dbcontext loading connectionString from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddNorthwindContext();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add identity management features
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC features
builder.Services.AddControllersWithViews();

/* 
 * Builder object has two commonly used objects:
 * Configuration: Contains merged values from appsettings.json, environment variables, command line arguments, etc.
 * Services: A collection of registered dependency services. Adding a DBContext with AddDbContext<DBContextObjectToAdd>(options => options.UseSqlServer(connectionString) is an example
 * of adding a service.
 */
#endregion

#region Instantiate app
var app = builder.Build();
#endregion

#region Configure HTTP Request pipeline, redirection, etc.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region Some Notes
// Map controller route is added as part of the MVC qapplication. It is very flexible.
/*
 * Route pattern has {} to define segments.
 * URL                          Controller      Action      ID
 * /                            Home            Index
 * /Muppet                      Muppet          Index       
 * /Kermit                      Muppet          Kermit
 * /Muppet/Kermit/Green         Muppet          Kermit      Green
 * etc.
 * 
 * If user hasn't entered these names in the URL, controller defaults to home, action to index
 * ID is optional as denoted by the ? in {id?}
 * 
 * If an incoming URL has a controller denoted, ASP.NET Core looks for a class decorated with the [Controller] attribute
 * that has the same name.
 * 
 * OR for a class which derrives from a [Controller] decorated class. 
 * 
 * Microsoft's ControllerBase class contains these useful properties:
 *      Request: Just the HTTP request, to get headers, query string parameters, cookies etc.
 *      Response: Just the HTTP response to get the headers, body of response as a writable stream, status code, cookies
 *      HttpContext: Everything about the current HTTP context including request and respnse, User object for Authentication/Authorization,
 *      collection of features enabled on server with middleware, etc.
 *      
 *There is no view support in ControllerBase BUT there is view support in Controller, which inherits from ControllerBase, IActionFilter, IFilterMetadata, IAsyncActionFilter, Idisposable
 */

/*
 * Controller has useful properties for working with views including
 * 
 * ViewData: A dictionary which gets disposed at the end of the current request/response, holding key value pairs that can be accessed in the view
 * ViewBag: A dynamic object which wraps the ViewData to provide a friendlier syntax for accessing and writing key-value pairs. ViewBag.Greeting = "Hello" instead of
 * ViewBag["Greeting"] = "Hello" for example.
 * TempData: Like view data, but it survives into the next request/response from the same visitor session. Useful for storing an initial request, doing a redirect, and then
 * reading the stored value from the original request in the context of executing the subsequent request
 * 
 * View: Returns a ViewResult after executing a view that renders a full response. return View("Index", model) returns a dynamically generated webpage for the specified model
 * PartialView: Returns a PartialViewResult, or a chunk of dynamically generated HTML. 
 * ViewComponent: Returns a ViewComponentResult after executing a component which dynamically generates HTML. You must select a component by specifying its type or name,
 * and it can take an object as an argument.
 * JSON: Returns a JsonResult containing a JSON-serialized object. This is handy for implementing simple web APIs 
 */
#endregion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();
#endregion
#region Run app: start web server and listen for HTTP requests
app.Run(); //Thread blocking method call which starts server and listens for HTTP requests
#endregion