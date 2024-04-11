namespace BlazorQuiz.Client.Models
{
    public class QuizOptions
    {
        public double Timer { get; set; } = 60;
        public string QuizTitle { get; set; }
        public bool HasTimer { get; set; }
        public bool MediaQuestion { get; set; }
        public bool IsValidated { get; set; }
        public bool HasQuestion { get; set; }
        public string QuizGuid { get; set; }


        public void ResetToDefault()
        {
            MediaQuestion = false;
        }
        public void NewGame()
        {
            QuizGuid = string.Empty;
            QuizTitle = string.Empty;
            Timer = 60;
            HasQuestion = false;
            IsValidated = false;
            HasTimer = false;
        }
    }
}
