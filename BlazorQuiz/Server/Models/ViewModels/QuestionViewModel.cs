using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Services;
using System;

namespace BlazorQuiz.Server.ViewModels
{
    public class QuestionViewModel
    {

        private static Random _random = new Random();
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string? QuizMediaUrl { get; set; } = string.Empty;
        public int Id { get; set; }
        public QuestionViewModel()
        {
            
        }
        public QuestionViewModel(ApplicationDbContext context, QuestionModel questions)
        {
            Question = questions.Question;

            var answers = new List<string>
            {
                questions.Answer1, 
                questions.Answer2,
                questions.Answer3,
                questions.Answer4
            };

            Shuffle(answers);


            Answer1 = answers[0];
            Answer2 = answers[1];
            Answer3 = answers[2];
            Answer4 = answers[3];

            var mediaUrl = context.MediaModels.Where(m => m.Id == questions.MediaRefId).Select(m => m.Guid).FirstOrDefault();

            QuizMediaUrl = mediaUrl.ToString();
            Id = questions.Id;

        }



   

    }
}
