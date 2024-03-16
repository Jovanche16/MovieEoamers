using Data.Entities;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Data.Repositories.Interface
{
    public interface IMoviesRepository
    {
        Task<List<Movie>> GetLatestMovies(int count);
        Task<int> GetTotalMovieCount();
        Task<IQueryable<Movie>> GetMoviesPaginated(PagingRequest model);
        Task<Movie> GetMovieById(int id);
    }
}
