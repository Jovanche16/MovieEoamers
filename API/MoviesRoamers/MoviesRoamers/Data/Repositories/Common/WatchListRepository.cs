using Data.Entities;
using Microsoft.EntityFrameworkCore;
using MovieRoamers.Data.MovieRoamersContext;
using MoviesRoamers.Data.Repositories.Interface;
using System.Linq.Expressions;

namespace MoviesRoamers.Data.Repositories.Common
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly DataContext _dbcontext;

        public WatchListRepository(DataContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // Method to add a movie to the user's watch list
        public async Task AddToWatchListAsync(string userId, int movieId)
        {
            var movie = await _dbcontext.Movies.SingleOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
            {
                // Movie doesn't exist, handle the case appropriately
                throw new Exception("Movie not found.");
            }
            var watchListContains = await _dbcontext.Watchlists.AnyAsync(wl => wl.UserId == userId && wl.MovieId == movieId);
            if (watchListContains)
            {
                throw new Exception("The movie has already been added!");
            }
            var watchListItem = new Watchlist
            {
                UserId = userId,
                MovieId = movieId,
                AddedAt = DateTime.Now
            };

            await _dbcontext.Watchlists.AddAsync(watchListItem);
            await _dbcontext.SaveChangesAsync();
        }


        // Method to remove a movie from the user's watch list
        public async Task RemoveFromWatchListAsync(string userId, int movieId)
        {
            var watchListItem = _dbcontext.Watchlists.FirstOrDefault(w => w.UserId == userId && w.MovieId == movieId);
            if (watchListItem != null)
            {
                _dbcontext.Watchlists.Remove(watchListItem);
                await _dbcontext.SaveChangesAsync();
            }

        }

        // Method to get the user's watch list
        public async Task<List<Watchlist>> GetUserWatchListAsync(string userId)
        {
            var lis  = await _dbcontext.Watchlists.Where(w => w.UserId == userId)
                .Include(m => m.Movie)
                .ToListAsync();
            return lis;
        }
    }
}
