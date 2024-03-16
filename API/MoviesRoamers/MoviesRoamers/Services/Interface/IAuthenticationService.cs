using MovieRoamers.Data.Entities;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<TokenDto> Authenticate(LoginRequest model);
        Task<bool> Register(SignupRequest model);
    }
}
