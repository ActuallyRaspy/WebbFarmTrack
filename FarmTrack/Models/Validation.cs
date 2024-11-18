using FarmTrack.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FarmTrack.Models
{
    public class Validation
    {
        public static bool isLoggedIn(User user)
        {
            if (user == null) return false;
            else return true;
        }
        public static bool validateLogin(string username, string password, FarmContext _context)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false; // Om någon av fälten är tomma
            }

            // Hitta användaren i databasen
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            // Returnera true om användaren finns och lösenordet matchar
            return user != null;
        }

        public static int validateRegistration(User user, FarmContext _context)
        {
            User foundUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (user.Username.IsNullOrEmpty() || user.Password.IsNullOrEmpty()) return 1; //Make sure all fields are filled
            if (foundUser != null) return 2; //No duplicate usernames
            return 0; // No problems found

        }

        public static int validateCreateCrop(Crop crop, FarmContext _context)
        {
            var foundCrop = _context.Crop.FirstOrDefault(u => u.CropName == crop.CropName);

            if (foundCrop != null) return 4;// Crop name already exists

            if (crop.CropName.IsNullOrEmpty() || crop.HarvestSeasonWarm.IsNullOrEmpty() ||
                crop.HarvestSeasonCold.IsNullOrEmpty() || crop.PlantingSeasonCold.IsNullOrEmpty() ||
                crop.PlantingSeasonWarm.IsNullOrEmpty() || crop.DaysToGrow == null)
            {
                return 1; //Missing data
            }

            if (crop.CropDescription.Length > 500) return 2; // Description too large
            if (!int.TryParse(crop.DaysToGrow.ToString(), out int o)) return 3; // Days are not in Int format, cannot be parsed.
            return 0; // Valid inputs
        }
    }
}