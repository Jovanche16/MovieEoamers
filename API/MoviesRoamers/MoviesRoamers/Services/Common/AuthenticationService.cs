using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieRoamers.Data.Entities;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoviesRoamers.Services.Common
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUserRepository repository, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> Authenticate(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null;

            var token = await GenerateAccessToken(user);
            return token;
        }

        public async Task<bool> Register(SignupRequest model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Email = model.Email,
                UserName = model.UserName,
                // Set default values for non-nullable properties
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded ? true : false;

        }
        public async Task<TokenDto> GenerateAccessToken(User user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                //new Claim(ClaimTypes.Role,user.Role)

            };
            var tokenOptions = new JwtSecurityToken
            (
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.ToUniversalTime().AddMinutes(Convert.ToDouble(_configuration["JWT:AccessTokenExpiresInMinutes"])),
            signingCredentials: signingCredentials
            );
            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                Expires = tokenOptions.ValidTo,
            };
        }

        public async Task<TokenDto> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new TokenDto
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.Now.ToUniversalTime().AddDays(_configuration.GetValue<int>("JWT:RefreshTokenExpiresInDays"))
                };
            }
        }
    }
    //private string GenerateJwtToken(User user)
    //    {
    //        var jwtConfiguration = _configuration.GetSection("Tokens");
    //        var issuer = jwtConfiguration["Issuer"];
    //        var audience = jwtConfiguration["Audience"];
    //        var key = jwtConfiguration["Key"];


    //        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
    //        {
    //            throw new ArgumentNullException("Jwt configuration is missing or invalid.");
    //        }

    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    //        var claims = new[]
    //        {
    //           new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
    //           new Claim("id", user.Id.ToString())
    //};

    //        var token = new JwtSecurityToken(
    //            issuer: issuer,
    //            audience: audience,
    //            claims: claims,
    //            expires: DateTime.UtcNow.AddHours(1),
    //            signingCredentials: credentials);

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}
