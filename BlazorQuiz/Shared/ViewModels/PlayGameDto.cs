using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared.ViewModels
{
    public class GameQuizViewModel
    {
        public string title { get; set; }
        public int gameId { get; set; }
        public string? publicId { get; set; }
        public int timer { get; set; }
        public List<QuestionSharedViewModel> questions { get; set; }
    }
    public class GuessCheckViewModel
    {
        public string Guess { get; set; }
        public int GuessId { get; set; }
        public int Seconds { get; set; }

    }

    public class UserQuizViewModel
    {

        public string User { get; set; }
        public int Score { get; set; }
    }

    public class SubmitQuizViewModel
    {
        public int gameId { get; set; }
        public List<GuessCheckViewModel> guesses { get; set; }
    }


}
