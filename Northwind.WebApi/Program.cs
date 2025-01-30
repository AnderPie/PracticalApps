using Microsoft.AspNetCore.Mvc.Formatters; // To use IOutputFormatter
using Northwind.EntityModels; // To use AddNorthwindContext method
using Microsoft.Extensions.Caching.Memory; // To use IMemoryCache and manage caches

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNorthwindContext();
builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters:");
    foreach(IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if(mediaFormatter is null)
        {
            WriteLine($"  {formatter.GetType().Name}");
        }
        else // Output formatter has SupportedMediaTypes
        {
            WriteLine($"{mediaFormatter.GetType().Name}, Media types: {string.Join(",",mediaFormatter.SupportedMediaTypes)}");
        }
    }
})
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
