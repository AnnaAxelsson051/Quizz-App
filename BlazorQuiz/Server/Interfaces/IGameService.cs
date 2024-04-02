using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;
using BlazorQuiz.Server.Models.ViewModels;

// Fixad

namespace BlazorQuiz.Server.Interfaces
{
    public interface IGameService
    {
        Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId);
        Task<GameWithQuizViewModel> GetQuizByPublicIdAsync(string id);
        Task<bool> CheckGuess(GuessCheckViewModel guess);
        Task<QuizViewModel> CreateNewGameAsync(string quizId, string userId);
        UserQuizModel FindUserQuiz(int id);
        Task<bool> UpdateGameAsync(int gameId, string gameState);
        Task<UserQuizModel> FinishedGame(UserQuizModel quiz, List<GuessCheckViewModel> guesses);
        List<QuestionModel> FindQuestionsByQuizRef(string quizRef);






    }
}