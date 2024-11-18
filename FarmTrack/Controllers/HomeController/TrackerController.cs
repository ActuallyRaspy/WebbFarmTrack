using FarmTrack.Models;
using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;
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
            var cropList = _context.Crop.ToList();
            return View(cropList);
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
    }
}
