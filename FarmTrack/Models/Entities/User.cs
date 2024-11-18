namespace FarmTrack.Models.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Field> Fields { get; set; }  // No initialization because it will do it automatically when calling .include(Fields)

    }
}
