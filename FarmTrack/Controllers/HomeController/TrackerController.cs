using FarmTrack.Models;
using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace FarmTrack.Controllers.HomeController
{
    public class TrackerController : Controller
    {

        private readonly FarmContext _context;

        public TrackerController(FarmContext context)
        {
            _context = context;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Analysis()
        {
            return View();
        }

        public IActionResult Tracker()
        {
            // Hämta alla PlantedCrops där grödan inte är "No planted crop" och där dismissed == 0
            var trackedCrops = _context.PlantedCrops
                .Include(pc => pc.Crop)   // Ladda relaterade Crop-objekt
                .Include(pc => pc.Field)   // Ladda relaterade Field-objekt
                .Where(pc => pc.Crop.CropName != "No planted crop" && pc.Alerts.Any(a => a.Dismissed == 0)) // Filtrera baserat på dina villkor
                .ToList();

            var trackerViewModel = new TrackerViewModel
            {
                cropList = _context.Crop.ToList(),
                fieldList = _context.Fields.ToList(),
                trackedCrops = trackedCrops
            };

            return View(trackerViewModel);
        }

        public IActionResult Alert()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST Register - Handle register crop submission
        [HttpPost]
        public IActionResult CreateCrop(Crop crop)
        {
            switch (Validation.validateCreateCrop(crop, _context))
            {
                case 0:
                    // Create and save new user
                    _context.Crop.Add(crop);
                    _context.SaveChanges();

                    // Redirect to login page after successful registration
                    return RedirectToAction("Tracker");

                case 1:
                    ViewBag.Error = "Not all fields are filled";
                    return View("Tracker");
                case 2:
                    ViewBag.Error = "Description is too large! Max 500 characters.";
                    return View("Tracker");
                case 3:
                    ViewBag.Error = "Number of days to grow must be in whole numbers.";
                        return View("Tracker");
                case 4:
                    ViewBag.Error = "Crop name already taken.";
                    return View("Tracker");
            }

            ViewBag.Error = "Error occurred.";
            return View("Tracker");
        }

        // **Ny metod för att spåra och skapa crop samt alert**
        [HttpPost]
        public IActionResult TrackCrop(int Field, int CropName, DateTime PlantDate)
        {
            if (Field == 0 || CropName == 0 || PlantDate == default)
            {
                TempData["Alert"] = "All fields are required.";
                return RedirectToAction("Tracker");
            }

            // Skapa en ny PlantedCrop
            var plantedCrop = new PlantedCrop
            {
                FieldId = Field,
                CropId = CropName,
                PlantDate = PlantDate,
                Climate = 0,
                Harvested = 0
            };

            // Lägg till PlantedCrop i databasen
            _context.PlantedCrops.Add(plantedCrop);
            _context.SaveChanges();

            // Skapa en Alert kopplad till PlantedCrop
            var alert = new Alert
            {
                AlertName = "Harvest Alert",
                AlertDescription = $"Your crop needs harvesting soon!",
                AlertDate = PlantDate.AddDays(90),
                Triggered = 0,
                Dismissed = 0,
                PlantedCropId = plantedCrop.PlantedCropId
            };

            // Lägg till Alert i databasen
            _context.Alerts.Add(alert);
            _context.SaveChanges();

            // Bekräftelse till användaren
            TempData["Alert"] = "Crop tracked successfully, and alert created!";
            return RedirectToAction("Tracker");
        }
    }
}
