using BlazorQuiz.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorQuiz.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<QuizModel> Quizzes { get; set; }
        public DbSet<UserQuizModel> UserQuizModels { get; set; }
        public DbSet<QuestionModel> QuestionModels { get; set; }
        public DbSet<MediaModel> MediaModels { get; set; }

    }
}