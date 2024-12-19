namespace Backend.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; } // Možete koristiti enkripciju u pravoj aplikaciji
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}