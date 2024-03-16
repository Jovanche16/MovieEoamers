using Data.Entities;

namespace MoviesRoamers.Data.Repositories.Interface
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenres();
        Task<int> GetSeriesGenreId();
    }
}
