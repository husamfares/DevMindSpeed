using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AnswerValidationService : IAnswerValidationService 
    {
        private readonly DataContext _context;
        private readonly IGameEngineService _gameEngine;

        public AnswerValidationService(DataContext context, IGameEngineService gameEngine)
        {
            _context = context;
            _gameEngine = gameEngine;
        }

        public async Task<AnswerResultDto> ValidateAnswerAsync(int gameId, string question, float userAnswer)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null || !game.IsActive)
                throw new Exception("Game not found or already ended.");

            // Find the previous submission for the same question
            var previous = await _context.AnswersInfo
            .Where(a => a.GameId == gameId && a.Question == question && a.Answer != 0)
            .OrderByDescending(a => a.SubmittedAt)
            .FirstOrDefaultAsync();

        if (previous != null)
            throw new Exception("This question was already answered.");

            // Calculate time taken
            var lastSubmission = await _context.AnswersInfo
                .Where(a => a.GameId == gameId)
                .OrderByDescending(a => a.SubmittedAt)
                .FirstOrDefaultAsync();

            var startTime = lastSubmission?.SubmittedAt ?? game.StartTime;
            var submittedAt = DateTime.UtcNow;
            var timeTaken = submittedAt - startTime;

            // Get correct answer
            float correctAnswer = EvaluateStoredAnswer(gameId, question);
            bool isCorrect = Math.Round(userAnswer, 1) == Math.Round(correctAnswer, 1);



            // Save the answer
            var answer = new AnswerSubmission
            {
                GameId = gameId,
                Question = question,
                Answer = userAnswer,
                CorrectAnswer = correctAnswer,
                IsCorrect = isCorrect,
                SubmittedAt = submittedAt,
                TimeTaken = timeTaken
            };

            _context.AnswersInfo.Add(answer);
            await _context.SaveChangesAsync();

            // Generate next question
            var generated = _gameEngine.GenerateQuestion(game.DifficulteLevel);

            // Save new question to DB for next submission
            var preloadNext = new AnswerSubmission
            {
                GameId = gameId,
                Question = generated.Equation,
                CorrectAnswer = generated.Answer,
                SubmittedAt = DateTime.UtcNow
            };

            _context.AnswersInfo.Add(preloadNext);
            await _context.SaveChangesAsync();

            // Calculate current score
            var total = await _context.AnswersInfo.CountAsync(a => a.GameId == gameId && a.Answer != 0);
            var correct = await _context.AnswersInfo.CountAsync(a => a.GameId == gameId && a.IsCorrect);
            float score = total == 0 ? 0 : (float)correct / total;

            return new AnswerResultDto
            {
                Result = isCorrect
                    ? $"Good job {game.PlayerName}, your answer is correct!"
                    : $"Sorry {game.PlayerName}, your answer is incorrect.",
                TimeTaken = timeTaken.TotalSeconds,
                NextQuestion = new QuestionDto
                {
                    Question = generated.Equation,
                    SubmitUrl = $"/submit/{gameId}"
                },
                CurrentScore = score
            };
        }

        private float EvaluateStoredAnswer(int gameId, string question)
        {
            var q = _context.AnswersInfo
                .Where(a => a.GameId == gameId && a.Question == question)
                .Select(a => a.CorrectAnswer)
                .FirstOrDefault();

            return q ?? throw new Exception("Correct answer not found.");
        }
    }


}
