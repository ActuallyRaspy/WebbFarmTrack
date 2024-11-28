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
            var trackerViewModel = new TrackerViewModel
            {
                cropList = _context.Crop.ToList(),
                fieldList = _context.Fields.ToList(),
                plantedCrops = _context.PlantedCrops
                .Include(pc => pc.Crop)
                .Include(pc => pc.Field)
                .Where(pc => pc.Harvested == 0)
                .ToList()
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
                    return RedirectToAction("Tracker");
                case 2:
                    ViewBag.Error = "Description is too large! Max 500 characters.";
                    return RedirectToAction("Tracker");
                case 3:
                    ViewBag.Error = "Number of days to grow must be in whole numbers.";
                    return RedirectToAction("Tracker");
                case 4:
                    ViewBag.Error = "Crop name already taken.";
                    return RedirectToAction("Tracker");
            }

            ViewBag.Error = "Error occurred.";
            return View("Tracker");
        }

        // POST Register - Handle register crop submission
        [HttpPost]
        public IActionResult TrackCrop(PlantedCrop plantedCrop)
        {
            Crop dbCrop = _context.Crop.FirstOrDefault(c => c.CropId == plantedCrop.CropId);
            Field dbField = _context.Fields.FirstOrDefault(c => c.FieldId == plantedCrop.FieldId);
            plantedCrop.Field = dbField;
            plantedCrop.Crop = dbCrop;
            
            switch (Validation.validateTrackCrop(plantedCrop, _context))
            {
                case 0:
                    // Create and save new user
                    Alert plantedCropAlert = new Alert
                    {
                        AlertDate = plantedCrop.PlantDate,
                        AlertName = plantedCrop.Crop.CropName,
                        PlantedCrop = plantedCrop,
                        //PlantedCropId = plantedCrop.PlantedCropId,
                        Dismissed = 0,
                        Triggered = 0,
                        AlertDescription = ""
                    };

                    plantedCrop.Harvested = 0;

                    _context.Alerts.Add(plantedCropAlert);
                    _context.PlantedCrops.Add(plantedCrop);
                    
                    
                    _context.SaveChanges();

                    // Redirect to login page after successful registration
                    return RedirectToAction("Tracker");
                case 1:
                    ViewBag.Error = "Not all fields are filled.";
                    return View("Tracker");
            }
            ViewBag.Error = "Error occurred.";
            return View("Tracker");
        }
        [HttpPost]
        public IActionResult RemoveCrop(int PlantedCropId)
        {
            var crop = _context.PlantedCrops.FirstOrDefault(pc => pc.PlantedCropId == PlantedCropId);
            if (crop != null)
            {
                crop.Harvested = 1;
                _context.SaveChanges();
            }
            return RedirectToAction("Tracker");
        }
        //public int PlantedCropId { get; set; }
        //public DateTime PlantDate { get; set; }
        //public int Harvested { get; set; } // 0 if not yet harvested, 1 if harvested.
        //public int FieldId { get; set; }
        //public Field Field { get; set; }
        //public int CropId { get; set; }
        //public Crop Crop { get; set; }
        //public ICollection<Alert> Alerts { get; set; }


        // POST Register - Handle register crop submission
        [HttpPost]
        public IActionResult CreateField(Field field)
        {
            User sessionUser = HttpContext.Session.GetObject<User>("CurrentUser");
            if (sessionUser == null)
            {
                ViewBag.Error = "No user detected, try refreshing the page and logging in again.";
                return RedirectToAction("Tracker");
            }

            field.User = sessionUser;
            field.UserId = sessionUser.UserID;

            switch (Validation.validateCreateField(field, _context))
            {
                case 0:
                    // Create and save new user
                    _context.Fields.Add(field);
                    _context.SaveChanges();

                    // Redirect to login page after successful registration
                    return RedirectToAction("Tracker");
                case 1:
                    ViewBag.Error = "Not all fields are filled.";
                    return RedirectToAction("Tracker");
                case 2:
                    ViewBag.Error = "Description is too long (max 500 characters)";
                    return RedirectToAction("Tracker");
            }
            ViewBag.Error = "Error occurred.";
            return RedirectToAction("Tracker");
        }
    }
}