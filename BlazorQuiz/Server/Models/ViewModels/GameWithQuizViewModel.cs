namespace BlazorQuiz.Server.Models
{
    public class GameWithQuizViewModel
    {
        public int UserQuizId { get; set; }
        public int Score { get; set; }

        public int QuizId { get; set; }
        public string QuizPublicId { get; set; }
        public string QuizName { get; set; }
        public int QuizTimer { get; set; }
        public ICollection<QuestionModel> Questions { get; set; }

    }
}
