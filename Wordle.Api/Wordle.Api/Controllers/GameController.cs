using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers
{
    [Route("api/GameController")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("StartGame")]
        public async Task<string> StartGame()
        {
            return await _gameService.StartGame();
        }
        [HttpGet("AcceptInput")]
        public async Task<string> AcceptInput()
        {
            return await _gameService.AcceptInput();
        }

    }
}
