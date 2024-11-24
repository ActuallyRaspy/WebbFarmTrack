using FarmTrack.Models;
using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FarmTrack.Controllers.HomeController
{
    public class LoginController : Controller
    {
        private readonly FarmContext _context;

        public LoginController(FarmContext context)
        {
            _context = context;
        }

        // GET Login - Render the login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST Login - Handle login form submission
        [HttpPost]
        public IActionResult Login(User user)
        {
            // Kontrollera om användarinmatningen är korrekt
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                TempData["Alert"] = "Invalid username or password.";
                return View();
            }

            // Validera användaruppgifter
            bool isValid = Validation.validateLogin(user.Username, user.Password, _context);

            if (!isValid)
            {
                TempData["Alert"] = "Invalid username or password.";
                return View();
            }

            // Lyckad inloggning, lagra användarsession
            HttpContext.Session.SetObject("CurrentUser", user);
            return RedirectToAction("Index", "Dashboard");
        }

        // POST Register - Handle register form submission
        [HttpPost]
        public IActionResult Register(User user)
        {
            int validationResult = Validation.validateRegistration(user, _context);

            if (validationResult != 0)
            {
                TempData["Alert"] = validationResult switch
                {
                    1 => "Not all fields are filled.",
                    2 => "Username already taken.",
                    _ => "An unknown error occurred."
                };
                return RedirectToAction("Login");
            }

            // Skapa och spara ny användare
            _context.Users.Add(user);
            _context.SaveChanges();

            // Redirecta till login efter lyckad registrering
            TempData["Alert"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }
    }
}

