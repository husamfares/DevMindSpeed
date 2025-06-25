using API.DTO;

namespace API.Interfaces;

public interface IAnswerValidationService
{
     Task<AnswerResultDto> ValidateAnswerAsync(int gameId, string question, float userAnswer);
}

