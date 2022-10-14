using B2C_3CloudDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using Services.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace B2C_3CloudDemo.Services
{
    public static class WeatherDataServiceExtensions
    {
        public static void AddWeatherDataService(this IServiceCollection services, IConfiguration configuration)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IForecastDataService, ForecastDataService>();
        }
    }
    public class ForecastDataService : IForecastDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _WeatherForecastListScope = string.Empty;
        private readonly string _WeatherForecastListBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;

        public ForecastDataService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _WeatherForecastListScope = configuration["WeatherForecast:Scope"];
            _WeatherForecastListBaseAddress = configuration["WeatherForecast:BaseAddress"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<ForecastViewModel> GetForecastByCity(string city)
        {

            await PrepareAuthenticatedClient();
            

            var response = await _httpClient.GetAsync($"{_WeatherForecastListBaseAddress}/api/WeatherForecast/GetByCity/{city}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                ForecastViewModel forecast = JsonConvert.DeserializeObject<ForecastViewModel>(content);

                return forecast;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            

            //var forecast = from data in SampleData() where data.City == city select data;

            //return Task.FromResult(forecast.FirstOrDefault());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<ForecastViewModel> GetForecastById(int index)
        {

            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{_WeatherForecastListBaseAddress}/api/WeatherForecast/GetByID?id={index}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                ForecastViewModel todo = JsonConvert.DeserializeObject<ForecastViewModel>(content);

                return todo;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");

            //var forecast = from data in SampleData() where data.index == index select data;

            //return Task.FromResult(forecast.FirstOrDefault());

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<ForecastViewModel>> GetForecasts()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{_WeatherForecastListBaseAddress}/api/WeatherForecast/Get");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                IEnumerable<ForecastViewModel> todolist = JsonConvert.DeserializeObject<IEnumerable<ForecastViewModel>>(content);

                return todolist;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ForecastViewModel> SampleData()
        {

            var forecasts = new List<ForecastViewModel>
            {
                new ForecastViewModel { City = "London", Country = "UK", Temperature = "10", Summary = "Sunny", Icon = "sun", index = 0 },
                new ForecastViewModel { City = "Paris", Country = "France", Temperature = "15", Summary = "Cloudy", Icon = "cloud", index = 1 },
                new ForecastViewModel { City = "New York", Country = "USA", Temperature = "20", Summary = "Rainy", Icon = "rain", index = 2 }
            };

            return forecasts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task PrepareAuthenticatedClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _WeatherForecastListScope });
            Debug.WriteLine($"access token-{accessToken}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
