using BlazorQuiz.Server.Services;
using BlazorQuiz.Server.Interfaces;
using BlazorQuiz.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {

        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {

            _gameService = gameService;
        }

        //Creates a new quiz using data provided in model

        [HttpPost("create")]
        public async Task<IActionResult> PostQuiz([FromBody] NewQuizViewModel quiz)
        {
         var newQuiz = await _gameService.CreateQuizAsync(quiz.Title, quiz.Questions, quiz.Timer, UserId);

            var newQuizViewModel = new NewQuizViewModel()
            {
                PublicId = newQuiz.PublicId,
                Title = newQuiz.Name
            };

            return Ok(newQuizViewModel);
        }

        // Retrieves quiz by ID

       [HttpGet("{id}")]
        public async Task<IActionResult> GetUserQuiz(string id)
        {
            var retrievedQuiz = await _gameService.GetQuizByPublicIdAsync(id);

            return Ok(retrievedQuiz);
        }

        // Creates a new game for the specified quiz 
        // and current user

        [HttpPost("newgame/{quizId}")]
        public async Task<IActionResult> PostNewGame(string quizId)
        {
            var newGame = await _gameService.CreateNewGameAsync(quizId, UserId);

            return Ok(newGame);
        }

        // Checking if a guess is correct by validating provided guess data

        [HttpPost("guess")]
        public async Task<IActionResult> GuessCheck([FromBody] GuessCheckViewModel guess)
        {
            var isCorrect = await _gameService.CheckGuess(guess);

            return Ok(isCorrect);
        }

        // Updates a finished game,
        // verifies the number of guesses made matches the
        // number of questions in the quiz
        // returns the updated game score

        [HttpPut("gameresult")]
        public async Task<IActionResult> UpdateGame([FromBody] SubmitQuizViewModel quiz)
        {
            var userGame = _gameService.FindUserQuiz(quiz.gameId);
            var questions = _gameService.FindQuestionsByQuizRef(userGame.QuizRefPublicId);

            int numberOfUserGuesses = quiz.guesses.Count();
            int numberOfQuizQuestions = questions.Count();

            if (!numberOfUserGuesses.Equals(numberOfQuizQuestions))
            {
                return BadRequest("The number of user guesses does not match number of quiz questions");
            }
            else if (numberOfUserGuesses.Equals(numberOfQuizQuestions))
            {
                var updateScore = await _gameService.FinishedGame(userGame, quiz.guesses);

                return Ok(updateScore.Score);
            }
            return BadRequest("There is a problem while attempting to update game");
        }


    }
}
