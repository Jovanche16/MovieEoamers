using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRoamers.Data.Entities;
using MovieRoamers.Data.MovieRoamersContext;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Services.Interface;

namespace MovieRoamers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IAuthenticationService authenticationService, SignInManager<User> signInManager)
        {
            _authenticationService = authenticationService;
            _signInManager = signInManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var token = await _authenticationService.Authenticate(model);
            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignupRequest model)
        {
            var res = await _authenticationService.Register(model);
            if (res == false)
                return BadRequest(new { message = "Failed to register user" });

            return Ok(res);
        }

    }

}
