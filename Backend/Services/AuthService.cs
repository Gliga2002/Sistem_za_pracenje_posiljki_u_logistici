using Backend.Models;
using System.Collections.Generic;

namespace Backend.Services
{
    public class AuthService
    {
        // Lista korisnika (za potrebe memorije)
        private static readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin" }, // Test korisnik
        };

        // Tokeni koji su dodeljeni ulogovanim korisnicima
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

        public bool Logout(string username)
        {
            if (_tokens.ContainsKey(username))
            {
                _tokens.Remove(username); // Ukloni token kada se korisnik odjavi
                return true;
            }

            return false;
        }

        public bool IsLoggedIn(string username)
        {
            return _tokens.ContainsKey(username); // Provera da li je korisnik ulogovan
        }

          // Metoda za dobijanje svih logovanih korisnika

    }
}