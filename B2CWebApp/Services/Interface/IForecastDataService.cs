using B2C_3CloudDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Services.Interface
{
    public interface IForecastDataService
    {
        Task<ForecastViewModel> GetForecastByCity(string city);
        Task<ForecastViewModel> GetForecastById(int index);
        Task<IEnumerable<ForecastViewModel>> GetForecasts();
    }
}