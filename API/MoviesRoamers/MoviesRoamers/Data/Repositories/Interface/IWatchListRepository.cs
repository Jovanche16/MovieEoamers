using Data.Entities;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Data.Repositories.Interface
{
    public interface IWatchListRepository
    {
        Task AddToWatchListAsync(string userId, int movieId);
        Task RemoveFromWatchListAsync(string userId, int movieId);
        Task<List<Watchlist>> GetUserWatchListAsync(string userId);
    }
}
