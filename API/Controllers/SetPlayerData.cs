using API.Data;
using API.DTO;
using API.Enitities;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Route("game")]

public class SetPlayerDataController : ControllerBase
{
    private readonly IGameEngineService _gameEngineService;

    private readonly IAnswerValidationService _answerValdationService;
    private readonly DataContext _context;

    public SetPlayerDataController(DataContext context, IGameEngineService gameEngineService, IAnswerValidationService answerValdationService)
    {
        _context = context;
        _gameEngineService = gameEngineService;
        _answerValdationService = answerValdationService;
    }

    [HttpPost("/start")]
    public async Task<ActionResult> PlayerInfo([FromBody] GameSessionDTO gameSessionDTO)
    {
        var game = new GameSession
        {
            PlayerName = gameSessionDTO.PlayerName,
            DifficulteLevel = gameSessionDTO.DifficultyLevel,
            StartTime = DateTime.UtcNow,
            IsActive = true
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var generated = _gameEngineService.GenerateQuestion(gameSessionDTO.DifficultyLevel);

        var submission = new AnswerSubmission
        {
            GameId = game.GameId,
            Question = generated.Equation,
            CorrectAnswer = generated.Answer,
            SubmittedAt = DateTime.UtcNow,
            IsCorrect = false, // not answered yet
            TimeTaken = TimeSpan.Zero // or save later
        };
        _context.AnswersInfo.Add(submission);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = $"Hello {gameSessionDTO.PlayerName}, find your submit API URL below",
            question = generated.Equation,
            submit_url = $"/submit/{game.GameId}",
            time_started = game.StartTime
        });
    }


    [HttpPost("/submit/{gameId}")]
    public async Task<IActionResult> SubmitAnswer(int gameId, [FromBody] AnswerUserDTO answerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var questionRecord = await _context.AnswersInfo
            .Where(a => a.GameId == gameId && a.Answer == 0) 
            .OrderByDescending(a => a.SubmittedAt)
            .FirstOrDefaultAsync();


            if (questionRecord == null)
            {
                return BadRequest(new { error = "No active question found for this game." });
            }


            var result = await _answerValdationService.ValidateAnswerAsync(gameId, questionRecord.Question, answerDto.Answer ?? 0f);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


[HttpGet("{gameId}/end")]
public async Task<ActionResult<GameSummaryDto>> EndGameAsync(int gameId)
{
    var game = await _context.Games.FindAsync(gameId);
    if (game == null) return NotFound("Game not found");

    var allAnswers = await _context.AnswersInfo
        .Where(a => a.GameId == gameId && a.Answer != 0)
        .OrderBy(a => a.SubmittedAt)
        .ToListAsync();

    if (!allAnswers.Any()) return BadRequest("No answers found for this game.");

    var correctAnswers = allAnswers.Where(a => a.IsCorrect).ToList();

    float score = (float)correctAnswers.Count / allAnswers.Count;
    double totalTimeSpent = allAnswers.Sum(a => a.TimeTaken.TotalSeconds);

    var bestAnswer = correctAnswers
        .OrderBy(a => a.TimeTaken.TotalSeconds)
        .FirstOrDefault();

    var result = new GameSummaryDto
    {
        Name = game.PlayerName ?? string.Empty,
        Difficulty = game.DifficulteLevel,
        CurrentScore = score,
        TotalTimeSpent = totalTimeSpent,
        BestScore = bestAnswer == null ? null : new BestScoreDto
        {
            Question = bestAnswer.Question,
            Answer = bestAnswer.Answer,
            TimeTaken = bestAnswer.TimeTaken.TotalSeconds
        },
        History = allAnswers.Select(a => new HistoryDto
        {
            Question = a.Question,
            Answer = a.Answer,
            TimeTaken = a.TimeTaken.TotalSeconds
        }).ToList()
    };

    return Ok(result);
}


}
