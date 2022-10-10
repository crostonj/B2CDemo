using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Graph;
using ApiDemo.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace ApiDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [RequiredScope(scopeRequiredByAPI)]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        const string scopeRequiredByAPI = "Read.Weather";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            //var user = await _graphServiceClient.Me.Request().GetAsync();;
            return SampleData();
        }

        [HttpGet(Name = "GetWeatherForecastByID")]
        public Task<WeatherForecast> GetByIDAsync(int id)
        {
            List<WeatherForecast> list = SampleData();

            var forecast = from f in list where f.index == id select f;

            return Task.FromResult(forecast.FirstOrDefault());

        }

        [HttpGet(Name = "GetWeatherForecastByCity")]
        public Task<WeatherForecast> GetByCity(string city)
        {
            List<WeatherForecast> list = SampleData();

            var forecast = from f in list where f.City == city select f;

            return Task.FromResult(forecast.FirstOrDefault());

        }
        private List<WeatherForecast> SampleData()
        {
            int index = 0;
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>
            {
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "US",
                    City = "Denver",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                },
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "US",
                    City = "Portland",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                },
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "US",
                    City = "Saint Louis",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                },
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "US",
                    City = "Orlando",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                },
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "US",
                    City = "New York",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                },
                new WeatherForecast()
                {
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Country = "UK",
                    City = "London",
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Date = DateTime.Now.AddDays(Random.Shared.Next(1, 5)),
                    Icon = "None",
                    index = ++index
                }
            };

            return weatherForecasts;

        }
    }
}