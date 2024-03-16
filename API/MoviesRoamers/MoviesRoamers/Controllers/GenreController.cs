using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Interface;
using Newtonsoft.Json;

namespace MoviesRoamers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IActionResult> MovieList()
        {
            var res = new CustomResponseDto<List<Genre>>();
            try
            {
                var genre = await _genreService.GenreList();

                res.success = true;
                res.data = genre;
            }
            catch (Exception ex)
            {
                // Handle errors
                res.success = false;
                res.message = "An error occurred while retrieving data";
                res.errors = new List<string> { ex.Message };
            }
            return Ok(res);


        }
    }
}

