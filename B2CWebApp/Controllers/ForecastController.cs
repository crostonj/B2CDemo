using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Services.Interface;
using System.Threading.Tasks;


namespace B2C_3CloudDemo.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IForecastDataService _forecastDataService;

        public ForecastController(IForecastDataService forecastDataService)
        {
            _forecastDataService = forecastDataService;
        }


        [AuthorizeForScopes(ScopeKeySection = "WeatherForecast:Scope")]
        public async Task<ActionResult> Index()
        {
        
            return View(await _forecastDataService.GetForecasts());
        }

        // GET: HomeController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _forecastDataService.GetForecastById(id));
        }



        // GET: HomeController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _forecastDataService.GetForecastById(id));
        }
        
        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
