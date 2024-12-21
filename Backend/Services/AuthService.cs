using Backend.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class AuthService
    {
     
        private static readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin" }, // Test korisnik
        };

        
        private static readonly Dictionary<string, string> _tokens = new Dictionary<string, string>();

        public AuthResponse Login(string username, string password)
        {
    

            var noviKorisnik = new User() { Username = username, Password = password };
            _users.Add(noviKorisnik);
            
         
            var token = Guid.NewGuid().ToString();

           
            _tokens[username] = token;

            return new AuthResponse
            {
                Token = token,
                Username = username
            };
        }

 
    
    }
}