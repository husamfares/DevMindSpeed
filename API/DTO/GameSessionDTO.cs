namespace API.DTO;

public class GameSessionDTO
{
    public int GameId { get; set; }

    public string? PlayerName { get; set; }

    public int DifficultyLevel { get; set; }

    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;



}