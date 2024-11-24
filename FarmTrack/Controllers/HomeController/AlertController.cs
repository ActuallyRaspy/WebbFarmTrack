using FarmTrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http; // Required for CookieOptions
using FarmTrack.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmTrack.Controllers.HomeController
{
    public class AlertController : Controller
    {
        private readonly FarmContext _context;

        public AlertController(FarmContext context)
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
            return View();
        }

        public IActionResult Tracker()
        {
            // Configure the cookie options
            CookieOptions options = new CookieOptions
            {
                SameSite = SameSiteMode.None, // Adjust based on your needs (None, Lax, or Strict)
                Secure = true // This ensures that the cookie is sent only over HTTPS
            };

            // Set a cookie with the configured options
            Response.Cookies.Append("trackerCookie", "cookieValue", options);

            return View();
        }

        public IActionResult Alert()
        {
            var alerts = _context.Alerts
             .Include(a => a.PlantedCrop) // Ladda relaterad PlantedCrop-data
             .ThenInclude(pc => pc.Crop)  // Ladda relaterad Crop-data
             .Where(p => p.Dismissed == 0)
             .ToList();

            var plantedCrops = _context.PlantedCrops.ToList();
            

            Console.WriteLine($"Alerts count: {alerts.Count}");
            Console.WriteLine($"PlantedCrops count: {plantedCrops.Count}");

            ViewBag.Alerts = alerts;
            ViewBag.PlantedCrops = plantedCrops;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // **Ny metod för att skapa en alert**
       
        [HttpPost]
        public IActionResult CreateAlert(string alertName, string alertDescription, DateTime alertDate)
        {
            if (string.IsNullOrEmpty(alertName) || string.IsNullOrEmpty(alertDescription) || alertDate == default)
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction("Alert");
            }

            var alert = new Alert
            {
                AlertName = alertName,
                AlertDescription = alertDescription,
                AlertDate = alertDate,
                Triggered = 0,
                Dismissed = 0
            };

            _context.Alerts.Add(alert);
            _context.SaveChanges();

            TempData["Success"] = "Alert created successfully!";
            return RedirectToAction("Alert");
        }

        [HttpPost]
        public IActionResult DeleteAlert(int alertId)
        {
            var alert = _context.Alerts.Find(alertId);
            if (alert != null)
            {
                alert.Dismissed = 1;
                _context.SaveChanges();
                TempData["Success"] = "Alert removed successfully.";
            }
            else
            {
                TempData["Error"] = "Alert not found.";
            }

            return RedirectToAction("Alert");
        }

    }
}
