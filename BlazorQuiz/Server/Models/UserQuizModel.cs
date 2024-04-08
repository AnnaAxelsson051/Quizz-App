using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorQuiz.Server.Models

{
    public class UserQuizModel
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }

        // QuizRefPublicId property is foreign key that
        // references PublicId property of QuizModel entity
        // Stores the reference to the public ID of the associated quiz

        [ForeignKey("Quiz")]
        public string QuizRefPublicId { get; set; }
        // navigation property to access the associated QuizModel entity
        public virtual QuizModel Quiz { get; set; }

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
