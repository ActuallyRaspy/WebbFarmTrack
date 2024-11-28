using FarmTrack.Models;
using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Globalization;

namespace FarmTrack.Controllers.HomeController
{
    public class DashboardController : Controller
    {

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // API URL
            string apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=57&longitude=13.41&hourly=temperature_2m&timezone=Europe%2FLondon&forecast_days=3";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Fetch JSON from the API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON into the Weather object
                    var weather = JsonSerializer.Deserialize<Weather>(jsonResponse);
                    string todaysDate = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    // Find the index of the specific time "2024-11-29T12:00"
                    var timeIndex = Array.IndexOf(weather?.hourly?.time, todaysDate + "T12:00");

                    if (timeIndex >= 0)
                    {
                        // Retrieve the temperature at that time
                        var tempAtNoon = weather?.hourly?.temperature_2m?[timeIndex];

                        // Assign the temperature to ViewData
                        ViewData["TemperatureAtNoon"] = tempAtNoon.HasValue ? tempAtNoon.Value + "°C" : "Data not found";
                    }
                    else
                    {
                        // If the time is not found, show an error message
                        ViewData["TemperatureAtNoon"] = "Time not found";
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to fetch data from API.");
                ViewData["TemperatureAtNoon"] = "Error fetching data.";
            }

            return View();
        }


        public IActionResult Analysis()
        {
            return View();
        }

        public IActionResult Tracker()
        {
            return View();
        }

        public IActionResult Alert()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
