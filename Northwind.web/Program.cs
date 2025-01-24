var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


#region Configure the HTTP pipeline and routes
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    //app.UseDeveloperExceptionPage(); Executed automatically in .NET6 and greater
}
app.UseHttpsRedirection(); // Forces use of more secure https if browser allows 
app.MapGet("/", () => $"Environment is {app.Environment.EnvironmentName}!");
#endregion


// Start the web server, host the website, and wait for requests.
app.Run(); // This is a thread blocking call
WriteLine("This executes after the web server has stopped!");

// Let off at the top of page 665