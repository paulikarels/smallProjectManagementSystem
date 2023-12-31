using BCrypt.Net;

namespace backend.Models
{
    public class User
    {
        public int UserID { get; }
        public string Username { get; set; }
        public string Password { get; set; }


        public User(int userId, string username, string password)
        {
            UserID = userId;
            Username = username;
            Password = password;
        }
        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }


    }
}