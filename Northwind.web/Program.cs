using Northwind.EntityModels; // To use our database context!

#region Configure the web server host and services
// Adds model binding, auth, anti-forgery, and ASP.NET Core Razor Pages
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext(); //Register the Northwind database context class
var app = builder.Build();
#endregion

#region Configure the HTTP pipeline and routes
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    //app.UseDeveloperExceptionPage(); Executed automatically in .NET6 and greater
}

#region Adding an anonymous delegate as middleware
/*
 * Implementing an anonymous inline delegate as middleware to intercept HTTP request and responses.
 */

app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if (rep is not null)
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }
    if (context.Request.Path == "/bonjour")
    {
        // If this is the path, we terminate the pipeline rather than call next delegate
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }
    // We could modify the request before calling the next delegate
    await next();
    // We could modify the response after calling the next delegate
});



#endregion

app.UseHttpsRedirection(); // Forces use of more secure https if browser allows 

app.UseDefaultFiles(); //index.html, default.html etc.
app.UseStaticFiles(); // So that the static 'index.html' file we wrote can be served
app.MapRazorPages(); // To use our razor pages in pages
app.MapGet("/hello", () => $"Hello. Environment is {app.Environment.EnvironmentName}!");
#endregion


// Start the web server, host the website, and wait for requests.
app.Run(); // This is a thread blocking call
WriteLine("This executes after the web server has stopped!");

