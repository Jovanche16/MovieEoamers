using Data.Entities;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Services.Interface
{
    public interface IMoviesService
    {
        Task<List<MovieDto>> LatestMovies(int count);
        Task<PagedResultDto<MovieDto>> MoviesPaginated(PagingRequest model);
        Task<MovieDto> MovieById(int id);
    }
}
