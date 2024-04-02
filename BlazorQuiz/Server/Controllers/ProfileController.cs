using Microsoft.AspNetCore.Mvc;
using BlazorQuiz.Server.Services;
using BlazorQuiz.Server.Interfaces;

// Fixad

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {

        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {

            _profileService = profileService;
        }

        // Returns data on users who played a specific game

        [HttpGet("myquizzes/{publicid}")]
        public async Task<IActionResult> GetDataOnSpecificGame(string publicId)
        {
            var gamePlayerData = await _profileService.GetDataOnGameAsync(publicId, UserName);

            return Ok(gamePlayerData);
        }

        // Returns games created by user

        [HttpGet("myquizzes")]
        public async Task<IActionResult> GetCreatedGames()
        {
            var gamesCreatedByUser = await _profileService.GetUserCreatedGamesAsync(UserId);

            return Ok(gamesCreatedByUser);
        }


        // Get player specific games

        [HttpGet("mygames")]
        public async Task<IActionResult> GetUserGames()
        {
            var playerGames = await _profileService.GetUserGamesAsync(UserId);

            return Ok(playerGames);
        }

  
    
    }
}
