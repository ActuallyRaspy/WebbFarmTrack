using FarmTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace FarmTrack.Controllers.HomeController
{
    public class AnalysisController : Controller
    {
        private readonly FarmContext _context; // Deklarera _context

        // Konstruktor för att initialisera _context
        public AnalysisController(FarmContext context)
        {
            _context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(string name)
        {
            ViewData["username"] = "Placeholder" + name;
            return View();
        }

        public IActionResult Analysis()
        {

            // Hämta alla PlantedCrops och relaterad data från databasen
            var plantedCrops = _context.PlantedCrops
                .Include(pc => pc.Crop) // Ladda relaterade Crop-objekt
                .Include(pc => pc.Field) // Ladda relaterade Field-objekt
                .ToList(); // Hämtar alla utan att filtrera på dismissed eller andra statusar

            Debug.WriteLine($"PlantedCrops count: {plantedCrops.Count}");

            // Skicka den hämtade datan till vyn
            return View(plantedCrops);
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
