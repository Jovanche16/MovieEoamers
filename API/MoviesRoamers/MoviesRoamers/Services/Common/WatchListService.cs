using Data.Entities;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Interface;

namespace MoviesRoamers.Services.Common
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListRepository watchListRepository;

        public WatchListService(IWatchListRepository watchListRepository)
        {
            this.watchListRepository = watchListRepository;
        }

        public async Task AddToWatchListAsync(string userId, int movieId)
        {
            await watchListRepository.AddToWatchListAsync(userId, movieId);
        }

        public async Task RemoveFromWatchListAsync(string userId, int movieId)
        {
            await watchListRepository.RemoveFromWatchListAsync(userId, movieId);
        }

        public async Task<List<Watchlist>> GetUserWatchListAsync(string userId)
        {
            return await watchListRepository.GetUserWatchListAsync(userId);
        }
    }
}
