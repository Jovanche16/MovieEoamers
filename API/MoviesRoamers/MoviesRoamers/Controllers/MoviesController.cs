using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesRoamers.Data.Requests;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Common;
using MoviesRoamers.Services.Interface;

namespace MoviesRoamers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesServices)
        {
            _moviesService = moviesServices;
        }

        [HttpGet("latest-movies")]
        public async Task<IActionResult> LatestMovies(int count)
        {
            var data = await _moviesService.LatestMovies(count);
            var res = new CustomResponseDto<List<MovieDto>>
            {
                success = true,
                data = data
            };
            return Ok(res);
        }

        [HttpGet("paginated-movies")]
        public async Task<IActionResult> MoviesPaginated([FromQuery] PagingRequest model)
        {

            var data = await _moviesService.MoviesPaginated(model);
            var res = new CustomResponseDto<PagedResultDto<MovieDto>>
            {
                success = true,
                data = data
            };
            return Ok(res);

        }

        [HttpGet("id")]
        public async Task<IActionResult> SingleMovie(int movieId)
        {
            var data = await _moviesService.MovieById(movieId);
            var res = new CustomResponseDto<MovieDto>
            {
                success = true,
                data = data
            };
            return Ok(res);

        }

    }
}
