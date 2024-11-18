using FarmTrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http; // Required for CookieOptions

namespace FarmTrack.Controllers.HomeController
{
    public class AlertController : Controller
    {
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
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
