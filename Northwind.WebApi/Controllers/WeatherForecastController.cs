using Microsoft.AspNetCore.Mvc;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // https://YourPort/WeatherForecast
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET /weatherforecast

        [HttpGet(Name = "GetWeatherForecastFiveDays")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Get(5);
        }

        // GET /WeatherForecast/9
        [HttpGet(template: "{numDays:int}", Name="GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(int numDays)
        {
            return Enumerable.Range(1, numDays).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
