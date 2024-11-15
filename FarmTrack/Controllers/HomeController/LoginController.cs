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
            // Use validation class to check credentials
            bool isValid = Validation.validateLogin(user.Username, user.Password, _context);

            if (!isValid)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Successful login, redirect to dashboard
            HttpContext.Session.SetObject("CurrentUser", user);
            return RedirectToAction("Index", "Dashboard");
        }

        //// GET Register - Render the registration view
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        // POST Register - Handle register form submission
        [HttpPost]
        public IActionResult Register(User user)
        {
            switch (Validation.validateRegistration(user, _context))
            {
                case 0:
                    break;
                case 1:
                    ViewBag.Error = "Not all fields are filled";
                    return View("Login");
                case 2:
                    ViewBag.Error = "Username already taken.";
                    return View("Login");
            }


            // Create and save new user
            _context.Users.Add(user);
            _context.SaveChanges();

            // Redirect to login page after successful registration
            return RedirectToAction("Login");
        }
    }
}

