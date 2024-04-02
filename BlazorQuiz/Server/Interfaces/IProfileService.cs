using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;

namespace BlazorQuiz.Server.Interfaces

{
    public interface IProfileService
    {
        Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username);
        Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId);
        Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId);
       

    }
}
