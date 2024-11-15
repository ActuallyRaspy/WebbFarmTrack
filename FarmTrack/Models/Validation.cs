using FarmTrack.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FarmTrack.Models
{
    public class Validation
    {
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
            var foundUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (user.Username.IsNullOrEmpty() || user.Password.IsNullOrEmpty()) return 1; //Make sure all fields are filled
            if (user.Username == foundUser.Username) return 2; //No duplicate usernames
            return 0; // No problems found

        }
    }
}