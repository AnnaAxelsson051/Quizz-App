using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;
using BlazorQuiz.Server.Interfaces;

// Fixad

namespace BlazorQuiz.Server.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;

        }

        // Retrieves and organizes data on user quizzes based on provided quiz public ID,
        // sorting by score in desc order, returns a list of view models
        // containing user scores and names associated with the quiz

        public async Task<List<UserQuizViewModel>> GetDataOnGameAsync(string publicId, string username)
        {
            var userQuizzes = _context.UserQuizModels
                .Where(userQuiz => userQuiz.QuizRefPublicId == publicId)
                .OrderByDescending(userQuiz => userQuiz.Score)
                .ToList();
            var userQuizViewModels = new List<UserQuizViewModel>();

            foreach (var userQuiz in userQuizzes)
            {
                var userQuizViewModel = new UserQuizViewModel();
                userQuizViewModel.Score = userQuiz.Score;
                userQuizViewModel.User = _context.Users
                    .Where(userQuizModel => userQuizModel.Id == userQuiz.UserRefId)
                    .Select(userQuizModel => userQuizModel.UserName).FirstOrDefault();
                userQuizViewModels.Add(userQuizViewModel);
            }

            return userQuizViewModels;
        }

        // Retrieves all quizzes created by specific user,
        // extracts their name and public ID and returns them as list of view models

        public async Task<List<UserCreatedQuizViewModel>> GetUserGamesAsync(string userId)
        {
            var userQuizzes = _context.Quizzes.Where(quiz => quiz.UserRefId == userId).ToList();

            var userCreatedQuizViewModels = new List<UserCreatedQuizViewModel>();
            foreach (var userQuiz in userQuizzes)
            {
                var userCreatedQuizViewModel = new UserCreatedQuizViewModel();
                userCreatedQuizViewModel.Name = userQuiz.Name;
                userCreatedQuizViewModel.PublicId = userQuiz.PublicId;
                userCreatedQuizViewModels.Add(userCreatedQuizViewModel);
            }
            return userCreatedQuizViewModels;
        }

        // Retrieves all quizzes created by specific user based on user ID
        // returns them as a list of QuizModel objects
        // represent the full details of each quiz created by the user

        public async Task<List<QuizModel>> GetUserCreatedGamesAsync(string userId)
        {
            var userQuizzes = _context.Quizzes.Where(quiz => quiz.UserRefId == userId).ToList();
            return userQuizzes;

        }

   
    }
}
