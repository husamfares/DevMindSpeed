using API.Data;
using API.DTO;
using API.Enitities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SetPlayerDataController : ControllerBase
{
    private readonly IGameEngineService _gameEngineService;
    private readonly DataContext _context;

    public SetPlayerDataController(DataContext context, IGameEngineService gameEngineService)
    {
        _context = context;
        _gameEngineService = gameEngineService;
    }

    [HttpPost("/start")]
    public async Task<ActionResult> PlayerInfo([FromBody] GameSessionDTO gameSessionDTO)
    {
        var game = new GameSession
        {
            //GameId = gameSessionDTO.GameId,
            PlayerName = gameSessionDTO.PlayerName,
            DifficulteLevel = gameSessionDTO.DifficultyLevel,
            StartTime = DateTime.UtcNow,
            IsActive = true
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var question = _gameEngineService.GenerateQuestion(gameSessionDTO.DifficultyLevel);

        return Ok(new
        {
            message = $"Hello {gameSessionDTO.PlayerName}, find your submit API URL below",
            question = question,
            submit_url = $"/submit/{game.GameId}",
            time_started = game.StartTime
        });
    }
}
