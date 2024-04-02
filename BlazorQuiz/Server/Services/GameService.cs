using BlazorQuiz.Server.Data;
using Microsoft.EntityFrameworkCore;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;
using BlazorQuiz.Shared.ViewModels;
using BlazorQuiz.Server.ViewModels;
using BlazorQuiz.Server.Interfaces;

//Fixad

namespace BlazorQuiz.Server.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediaService _mediaService;


        public GameService(ApplicationDbContext context, IMediaService mediaService)
        {
            _context = context;
            _mediaService = mediaService;

        }
        
        // Creates a new quiz with spec title, timer and user ID
        // Generating a unique ID for the quiz
        // Creates question models for new q's with answers and media refs

        public async Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId)
        {
            var createdQuiz = new QuizModel
            {
                Name = title,
                PublicId = Guid.NewGuid().ToString(),
                Timer = seconds,
                UserRefId = userId
            };

            _context.Quizzes.Add(createdQuiz);
            await _context.SaveChangesAsync();

            foreach (var question in questions)
            {
                var newQuestion = new QuestionModel
                {
                    Question = question.Question,
                    Answer1 = question.Answer1,
                    Answer2 = question.Answer2,
                    Answer3 = question.Answer3,
                    Answer4 = question.Answer4,
                    QuizRefId = createdQuiz.PublicId
                };

                if (!string.IsNullOrEmpty(question.QuizImageUrl))
                {
                    var media = await _mediaService.GetMediaByIdAsync(Guid.Parse(question.QuizImageUrl));
                    int mediaID = media.Id;

                    newQuestion.MediaRefId = mediaID;
                }
                _context.QuestionModels.Add(newQuestion);
            }
            await _context.SaveChangesAsync();

            return createdQuiz;
        }

        // Creates a new game associated with a spec quiz and user,
        // Adds questions from db to viewmodel
        // initializing game with score zero

        public async Task<QuizViewModel> CreateNewGameAsync(string quizId, string userId)
        {

            var retrievedQuiz = FindQuiz(quizId);

            var newGame = new UserQuizModel
            {
                QuizRefPublicId = retrievedQuiz.PublicId,
                UserRefId = userId,
                Score = 0,
            };

            _context.UserQuizModels.Add(newGame);
            await _context.SaveChangesAsync();

            List<QuestionModel> retrievedQuestions = _context.QuestionModels.Where(question => question.QuizRefId == retrievedQuiz.PublicId).ToList();
            List<QuestionViewModel> questionViewModels = new();

            foreach (var question in retrievedQuestions)
            {
                QuestionViewModel questionViewModel = new(_context, question);

                questionViewModels.Add(questionViewModel);
            }

            QuizViewModel quizViewModel = new()
            {
                title = retrievedQuiz.Name,
                timer = retrievedQuiz.Timer,
                publicId = retrievedQuiz.PublicId,
                questions = questionViewModels,
                gameId = newGame.Id
            };

            return quizViewModel;
        }

        // Retrieves a quiz based on its public id

        private QuizModel FindQuiz(string refId)
        {
            QuizModel? retrievedQuiz = _context.Quizzes
                .Where(quiz => quiz.PublicId == refId)
                .FirstOrDefault();

            return retrievedQuiz;
        }

        // Calculates the score for a finished game based on correctness of
        // guesses and penalties
        // Updates the corresponding user quiz model with calculated score and returns it

        public async Task<UserQuizModel> FinishedGame(UserQuizModel quiz, List<GuessCheckViewModel> guesses)
        {
            int finalScore = 0;

            foreach (var guess in guesses)
            {
                bool guessIsCorrect = await CheckGuess(guess);

                if (guessIsCorrect == true)
                {
                    finalScore += 1000;
                    if (guess.Seconds != null)
                    {
                        finalScore -= guess.Seconds;
                    }
                }
            }

            var userQuiz = _context.UserQuizModels.FirstOrDefault(quiz => quiz.Id == quiz.Id);

            userQuiz.Score = finalScore;
            await _context.SaveChangesAsync();

            return userQuiz;

        }

        // Checks if guess matches correct answer for a question

        public async Task<bool> CheckGuess(GuessCheckViewModel guess)
        {
            var correctAnswer = _context.QuestionModels.Where(questions => questions.Id == guess.GuessId).Select(question => question.Answer1).Single();

            if (correctAnswer == guess.Guess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Retrieves quiz details and associated user quiz information

        public async Task<GameWithQuizViewModel> GetQuizByPublicIdAsync(string id)
        {
            var retrievedUserQuiz = await _context.UserQuizModels
                             .Include(userQuiz => userQuiz.Quiz)
                             .ThenInclude(quiz => quiz.Questions)
                             .FirstOrDefaultAsync(game => game.QuizRefPublicId == id);

            if (retrievedUserQuiz == null)
            {
                return null;
            }

            var quiz = retrievedUserQuiz.Quiz;

            var viewModel = new GameWithQuizViewModel
            {
                UserQuizId = retrievedUserQuiz.Id,
                Score = retrievedUserQuiz.Score,
                QuizId = quiz.Id,
                QuizPublicId = quiz.PublicId,
                QuizName = quiz.Name,
                QuizTimer = quiz.Timer,
                Questions = quiz.Questions
            };

            return viewModel;
        }

        public Task<bool> UpdateGameAsync(int gameId, string gameState)
        {
            throw new NotImplementedException();
        }

        // Get a UserQuizModel based on game Id

        public UserQuizModel FindUserQuiz(int id)
        {
            UserQuizModel? game = _context.UserQuizModels.Where(game => game.Id == id).FirstOrDefault();

            return game;
        }

        // Retrieves a list of question models based on quiz reference ID

        public List<QuestionModel> FindQuestionsByQuizRef(string quizRef)
        {

            var questions = _context.QuestionModels.Where(questions => questions.QuizRefId == quizRef).ToList();
            return questions;

        }
    }
}
