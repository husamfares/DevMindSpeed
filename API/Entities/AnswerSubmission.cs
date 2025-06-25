using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Enitities;

namespace API.Entities
{
    public class AnswerSubmission
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to GameSession
        [ForeignKey("GameSession")]
        public int GameId { get; set; }
        public GameSession? GameSession { get; set; }

        [Required]
        public string Question { get; set; } = null!;  // question text at this step

        [Required]
        public float Answer { get; set; }  // player's submitted answer

        [Required]
        public bool IsCorrect { get; set; }  // was answer correct?

        [Required]
        public TimeSpan TimeTaken { get; set; }  // time taken to answer question

        [Required]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // Optional: store the correct answer for reference
        public float? CorrectAnswer { get; set; }
    }
}
