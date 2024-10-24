using FarmTrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace FarmTrack.Controllers
{
    public class AnalysisController : Controller
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
