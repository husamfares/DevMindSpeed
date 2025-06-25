using API.Enitities;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<GameSession> Games { get; set; }
    public DbSet<AnswerSubmission> AnswersInfo { get; set; }

}