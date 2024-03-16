using Data.Entities;
using Microsoft.EntityFrameworkCore;
using MovieRoamers.Data.MovieRoamersContext;
using MoviesRoamers.Data.Repositories.Interface;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;

namespace MoviesRoamers.Data.Repositories.Common
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DataContext _dbContext;
        public MoviesRepository(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<List<Movie>> GetLatestMovies(int count)
        {
            var movies = await _dbContext.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .OrderByDescending(m => m.ReleaseDate)
                .Where(m => !m.MovieGenres.Any(mg => mg.Genre.Name == "Series"))
                .Take(count)
                .ToListAsync();

            return movies;
        }

        public async Task<int> GetTotalMovieCount()
        {
            return await _dbContext.Movies.CountAsync();
        }

        public async Task<IQueryable<Movie>> GetMoviesPaginated(PagingRequest model)
        {
            var query = _dbContext.Movies
                 .Include(m => m.MovieGenres)
                 .ThenInclude(mg => mg.Genre)
                 .OrderByDescending(m => m.ReleaseDate)
                 .AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(model.Search))
            {
                query = query.Where(m => m.Title.Contains(model.Search));
            }

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                var propertyInfo = typeof(MovieDto).GetProperty(model.SortBy);
                if (propertyInfo != null)
                {
                    if (model.SortDirection.ToLower() == "desc")
                    {
                        query = query.OrderByDescending(m => propertyInfo.Name);
                    }
                    else
                    {
                        query = query.OrderBy(m => propertyInfo.Name);
                    }
                }
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var paginatedQuery = query.Skip((model.PageNumber - 1) * model.PageSize)
                                      .Take(model.PageSize);

            return paginatedQuery;
        }
        public async Task<Movie> GetMovieById(int movieId)
        {

            var movie = await _dbContext.Movies
          .Include(m => m.MovieGenres)
          .ThenInclude(mg => mg.Genre)
          .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie;
        }
    }
}
