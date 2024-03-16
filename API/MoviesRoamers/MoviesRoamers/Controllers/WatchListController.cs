using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesRoamers.Dto;
using MoviesRoamers.Services.Common;
using MoviesRoamers.Services.Interface;
using Newtonsoft.Json;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesRoamers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchListService;
        private string? _userId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public WatchListController(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToWatchList(int movieId)
        {
          
            await _watchListService.AddToWatchListAsync(_userId, movieId);

            var res = new CustomResponseDto<string>
            {
                success = true,
                message = "The movie was succesfuly added to the watchlist",
            };
          
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromWatchList(int movieId)
        {

            await _watchListService.RemoveFromWatchListAsync(_userId, movieId);
            var res = new CustomResponseDto<string>
            {
                success = true,
                message = "The movie was succesfuly removed from the watchlist.",
            };
          
            return Ok(res);

        }
        [HttpGet]
        public async Task<IActionResult> Watchlist()
        {
            var data = await _watchListService.GetUserWatchListAsync(_userId);
            var res = new CustomResponseDto<List<Watchlist>>
            {
                success = true,
                data = data
            };
            return Ok(res);

        }
    }
}
