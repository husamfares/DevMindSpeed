namespace API.DTO;

    public class AnswerResultDto
    {
        public string Result { get; set; } = string.Empty;
        public double TimeTaken { get; set; }
        public QuestionDto NextQuestion { get; set; } = null!;
        public float CurrentScore { get; set; }
    }

        public class QuestionDto
    {
        public string Question { get; set; } = string.Empty;
        public string SubmitUrl { get; set; } = string.Empty;
    }