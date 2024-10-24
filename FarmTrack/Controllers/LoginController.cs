using FarmTrack.Models;
using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FarmTrack.Controllers
{
    public class LoginController : Controller
    {
        private readonly FarmContext _context;
        private readonly Validation _validation;

        public LoginController(FarmContext context)
        {
            _context = context;
            _validation = new Validation();
        }

        // GET Login - Render the login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST Login - Handle login form submission
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Use validation class to check credentials
            bool isValid = _validation.validateLogin(username, password, _context);

            if (!isValid)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Successful login, redirect to dashboard
            return RedirectToAction("Dashboard", "Home");
        }

        // GET Register - Render the registration view
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST Register - Handle register form submission
        [HttpPost]
        public IActionResult Register(string username, string password, string passwordConfirm)
        {
            // Check if passwords match
            if (password != passwordConfirm)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            // Check if username already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                ViewBag.Error = "Username already taken.";
                return View();
            }

            // Create and save new user
            var newUser = new User { Username = username, Password = password, Email = "" };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Redirect to login page after successful registration
            return RedirectToAction("Login");
        }
    }
}

