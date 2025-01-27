The following is work undertaken to learn C# through use of Mark Price's C#12 and .NET 8 Modern Cross-Platform Development Fundamentals.  
# Exercise 13
13.1 - Test your knowledge

##  List six method names that can be specified in an HTTP Request
GET:  Asks for a representation of specified resource
POST: Submits an entity to the specified resource, often changing state in the server
HEAD: Identical to GET but without a response body
PUT: Replace all current representation of specified resource with request content
DELETE: Deletes a specified resource
CONNECT: Etablishes a tunnel to the server identified by the specified resource
PATCH: Applies modification to a resource

## List six status codes and their descriptions that can be returned in an HTTP response.
200 - OK
404 - Resource not found
307 - Temporary Redirect
403 - Forbidden (client does not have rights)
401 -  Unauthorized (client must authenticate)
291 - Successfully created new resource
500 - Internal Server Error
501 - Not implemented exception
502 - Bad Gateway
503 - Service unavailable
504 - Gateway timeout 
## In ASP.NET core what is the Program class used for?
Establish a database context
Configure web host and HTTP pipeline and routes
Add desired anonymous delegates
Tell the app to UseStaticFiles(), MapRazorPages(), UseDefaultFiles() etc.

Remember that the app is the result of:

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWhatYouNeedToAdd (often .AddRazorPages() and .AddSomeDBContext)
var app = builder.Build();
## What does the acronym HSTS stand for and what does it do?
HSTS stands for HTTP Strict Transport Security, it forces all communication over HTTPS
and prevents the visitor from using untrusted/invalid certificates. 
## How do you enable static HTML pages for a website?
app.UseStaticFiles();  where app is the output of WebApplication.CreateBuilder(args).Build();
## How do you mix C# code in the middle of HTML to create a dynamic page?
You use @ to specify the C# to inject. 
## How can you define shared layouts for Razor Pages?
You specify a shared layout, often in a _Layout.cshtml page within WebProject/Pages/Shared.
Tht layout specifies where to render the body of other pages, for example:
<body>
    <div class="container">
        @RenderBody()
    <footer><p>Some copyright</p></footer>    
    </div>
</body>

Meanwhile, pages which implement the layout implement it with:
@{Layout = "_Layout"} 
or by configuring a page named _ViewStart.cshtml which identifies the layout to implement in its code
behind page. The _ViewStart.cshtml page applies to all .cshtml files within its directory, and you can
have multiple _ViewStart.cshtml pages in a series of directories, with the _ViewStart.cshtml closest
to a page determining its layout.
## How can you separate the markup from the code-behind in a Razor page
Create a file with the same name as the razorpage but with an extra .cs extension at the end.
This helps you keep your HTML + markup in one file while another one handles the C#.

## How do you configure an Entity Framework Core data context for use with an ASP.NET Core website?
You define a static IServiceCollection that extends a service collection with"
```C#
string path = "SomePathToDB"
services.AddDbContext<YourDbContextClass>(options => 
    options.UseSqlite($"Data Source={path}"); //Or equivalent
    options.LogTo(someLogger.WriteLine, new[] {Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting}); 
```

## How can you resuse Razor Pages with ASP.NET Core 2.2 or later?
You can make use of shared layouts, partial views (Razor views rendered within other views) and even ViewComponents.