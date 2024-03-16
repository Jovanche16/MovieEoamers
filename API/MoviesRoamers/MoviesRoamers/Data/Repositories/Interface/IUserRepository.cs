using MovieRoamers.Data.Entities;

namespace MoviesRoamers.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user, string password);
    }
}
