public class GameSummaryDto
{
    public required string Name { get; set; }
    public int Difficulty { get; set; }
    public float CurrentScore { get; set; }
    public double TotalTimeSpent { get; set; }
    public BestScoreDto? BestScore { get; set; }
    public List<HistoryDto>? History { get; set; }
}

public class BestScoreDto
{
    public string? Question { get; set; }
    public float Answer { get; set; }
    public double TimeTaken { get; set; }
}

public class HistoryDto
{
    public string? Question { get; set; }
    public float Answer { get; set; }
    public double TimeTaken { get; set; }
}
