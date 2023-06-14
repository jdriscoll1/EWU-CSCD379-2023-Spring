using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Services;
using Wordle.Api.Dtos;

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
        [HttpPost("AcceptInput")]
        public async Task<string> AcceptInput(DataDto data)
        {
            return await _gameService.AcceptInput(data);
        }

        [HttpGet("ViewUsedWords")]
        public string ViewUsedWords(int gameId) {
            return _gameService.ViewUsedWords(gameId);
        }

    }
}
