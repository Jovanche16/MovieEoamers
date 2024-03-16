using Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviesRoamers.Data.Repositories.Common;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Interface;

namespace MoviesRoamers.Services.Common
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }
        public async Task<List<MovieDto>> LatestMovies(int count)
        {
            var movies = await _moviesRepository.GetLatestMovies(count);
            var res = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                Runtime = m.Runtime,
                Description = m.Description,
                PosterUrl = m.PosterUrl,
                TrailerUrl = m.TrailerUrl,
                Genres = m.MovieGenres.Select(mg => mg.Genre.Name).ToList()
            }).ToList();

            return res;
        }

        public async Task<PagedResultDto<MovieDto>> MoviesPaginated(PagingRequest model)
        {
            var paginatedQuery = await _moviesRepository.GetMoviesPaginated(model);
            var movies = await paginatedQuery.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                Runtime = m.Runtime,
                Description = m.Description,
                PosterUrl = m.PosterUrl,
                TrailerUrl = m.TrailerUrl,
                Genres = m.MovieGenres.Select(mg => mg.Genre.Name).ToList()
            }).ToListAsync();

            var pagedResult = new PagedResultDto<MovieDto>
            {
                QueryObject = new PagingRequest
                {
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize
                },
                Items = movies,
            };
            return pagedResult.Items.Count is 0 ? throw new Exception("Movie not found!") : pagedResult;
        }


        public async Task<MovieDto> MovieById(int id)
        {
            var movie = await _moviesRepository.GetMovieById(id) ?? throw new Exception("Movie not found!");

            var res = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Runtime = movie.Runtime,
                Description = movie.Description,
                PosterUrl = movie.PosterUrl,
                TrailerUrl = movie.TrailerUrl,
                Genres = movie.MovieGenres.Select(mg => mg.Genre.Name).ToList()
            };
            return res;
        }
    }
}
