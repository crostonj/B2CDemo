using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp_OpenIDConnect_DotNet.Models;

namespace Services.Interface
{
    public interface IForecastDataService
    {
        Task<ForecastViewModel> GetForecastByCity(string city);
        Task<ForecastViewModel> GetForecastById(int index);
        Task<IEnumerable<ForecastViewModel>> GetForecasts();
    }
}