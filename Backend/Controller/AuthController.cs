using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(401)]
        public IActionResult Login([FromBody] User request)
        {
            _logger.LogInformation("U loginu sam!!");
            var response = _authService.Login(request.Username, request.Password);
            if (response == null)
            {
                _logger.LogWarning("Pokušaj nevalidnog logovanja za korisnika {Username}", request.Username);
                return Unauthorized(new { message = "Nevalidni kredencijali" });
            }

            _logger.LogInformation("Korisnik {Username} je uspešno ulogovan", request.Username);
            return Ok(response);
        }

       
    }
}


 

