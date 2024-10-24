namespace FarmTrack.Models
{
    public class Validation
    {
        public bool validateLogin(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }

            if (true)//if( user.findUser(username).username == user && user.findUser(password) == password) 
            {
                return true;
            }
        }
    }
}
