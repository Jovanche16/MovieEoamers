using Data.Entities;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Services.Interface
{
    public interface IWatchListService
    {
        //da smenam iminja
        Task AddToWatchListAsync(string userId, int movieId);
        Task RemoveFromWatchListAsync(string userId, int movieId);
        Task<List<Watchlist>> GetUserWatchListAsync(string userId);
    }
}
