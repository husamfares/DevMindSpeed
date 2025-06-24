using System.ComponentModel.DataAnnotations;

namespace API.Enitities;

public class GameSession()
{
    [Key]
    public int GameId { get; set; }

    public string? PlayerName { get; set; }

    [Range (1,4)]
    public int DifficulteLevel { get; set; }

    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    public DateTime EndTime { get; set; }

    public bool IsActive { get; set; } = true;
}

