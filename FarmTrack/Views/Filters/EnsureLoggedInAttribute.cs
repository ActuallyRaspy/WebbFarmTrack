using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FarmTrack.Models;

namespace FarmTrack.Views.Filters
{
    public class EnsureLoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the "CurrentUser" session is null
            var currentUser = context.HttpContext.Session.GetObject<User>("CurrentUser");

            // Get controller and action names
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();

            // Define public pages to exclude
            var publicControllers = new[] { "Login", "Register" };

            // Skip checking for public controllers or actions
            if (publicControllers.Contains(controllerName))
            {
                base.OnActionExecuting(context);
                return;
            }

            // If the user is not logged in, redirect them to the login page
            if (currentUser == null)
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
