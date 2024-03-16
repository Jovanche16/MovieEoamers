using Data.Entities;
using Microsoft.EntityFrameworkCore;
using MovieRoamers.Data.MovieRoamersContext;
using MoviesRoamers.Data.Repositories.Interface;

namespace MoviesRoamers.Data.Repositories.Common
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _dataContext;
        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Genre>> GetGenres()
        {
            var res = await _dataContext.Genres.ToListAsync();
            return res;
        }

        public async Task<int> GetSeriesGenreId()
        {
            var seriesGenre = await _dataContext.Genres.SingleOrDefaultAsync(g => g.Name == "Series");
            return seriesGenre?.Id ?? -1;

        }
    }
}
