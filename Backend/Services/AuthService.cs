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
            // // Proverite korisničke podatke
            // var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            // if (user == null)
            // {
            //     return null; // Invalid login
            // }

            var noviKorisnik = new User() { Username = username, Password = password };
            _users.Add(noviKorisnik);
            

            // Generisanje tokena (ovde koristimo GUID za demonstraciju)
            var token = Guid.NewGuid().ToString();

            
        
            // Spremanje tokena u memoriji
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
    public List<string> GetAllLoggedInUsers()
    {
        // Vraća listu svih logovanih korisnika sa njihovim username-ima
        return _tokens.Keys.ToList();
    }
    }
}