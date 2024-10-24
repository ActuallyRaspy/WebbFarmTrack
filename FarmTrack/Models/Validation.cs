namespace FarmTrack.Models
{
    public class Validation
    {
        public bool validateLogin(string username, string password, FarmContext _context)
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
    }
}
