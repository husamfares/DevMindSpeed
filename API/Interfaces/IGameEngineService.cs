using API.DTO;

namespace API.Interfaces;

public interface IGameEngineService
{
    public GeneratedQuestionDto GenerateQuestion(int difficulty);
}