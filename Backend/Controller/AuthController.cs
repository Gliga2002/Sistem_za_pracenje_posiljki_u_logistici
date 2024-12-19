
using Backend.Services;
using Microsoft.AspNetCore.Mvc;


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
        public IActionResult Login([FromBody] LoginRequest request)
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
        
        // ovo mi mozda ni ne treba
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutRequest request)
        {
            var result = _authService.Logout(request.Username);
            if (!result)
            {
                _logger.LogWarning("Pokušaj odjave korisnika {Username} koji nije ulogovan", request.Username);
                return BadRequest(new { message = "Korisnik nije ulogovan" });
            }

            _logger.LogInformation("Korisnik {Username} se uspešno odjavio", request.Username);
            return Ok(new { message = "Uspesno ste se odjavili" });
        }

        [HttpGet("check")]
        public IActionResult CheckLogin([FromQuery] string username)
        {
            var isLoggedIn = _authService.IsLoggedIn(username);
            return Ok(new { IsLoggedIn = isLoggedIn });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LogoutRequest
    {
        public string Username { get; set; }
    }
}