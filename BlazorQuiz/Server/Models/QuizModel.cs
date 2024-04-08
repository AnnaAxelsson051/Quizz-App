using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityModel;

namespace BlazorQuiz.Server.Models
{
    public class QuizModel
    {
        public int Id { get; set; }

        // PublicId is primary key for the entity

        [Key]
        public string PublicId { get; set; }
        public string Name { get; set; }  // Name of quiz
        public int Timer { get; set; }

        // UserRefId is foreign key that references 
        // ApplicationUser entity / for the associated user

        [ForeignKey("User")]
        public string UserRefId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // Collection of questions

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
