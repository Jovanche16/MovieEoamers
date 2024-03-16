using Microsoft.EntityFrameworkCore;
using MovieRoamers.Data.Entities;
using MovieRoamers.Data.MovieRoamersContext;
using MoviesRoamers.Data.Repositories.Interface;

namespace MoviesRoamers.Data.Repositories.Common
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            // Logic to create a new user in the database
            _context.Users.Add(user);
            return await _context.Users.FirstOrDefaultAsync();
        }

    }
}
