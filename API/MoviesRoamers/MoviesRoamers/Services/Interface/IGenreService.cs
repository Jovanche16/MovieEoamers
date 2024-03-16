using Data.Entities;

namespace MoviesRoamers.Services.Interface
{
    public interface IGenreService
    {
        public Task<List<Genre>> GenreList();
    }
}