using BlazorQuiz.Server.ViewModels;

namespace BlazorQuiz.Server.Models.ViewModels
{
    public class QuizViewModel
    {
        public string title { get; set; }
        public string? publicId { get; set; }
        public int timer { get; set; }
        public List<QuestionViewModel> questions { get; set; }
        public int gameId { get; set; }
    }
}
