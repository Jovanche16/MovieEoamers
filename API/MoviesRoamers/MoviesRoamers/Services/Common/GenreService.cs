using Data.Entities;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Services.Interface;

namespace MoviesRoamers.Services.Common
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository) 
        {
            _genreRepository = genreRepository;
        }
        public async Task<List<Genre>> GenreList()
        {
            return await _genreRepository.GetGenres();
        }
    }
}
