using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorQuiz.Server.Controllers
{
    public class BaseController : ControllerBase
    {

        // Retrieves unique id and user name of auth user

        protected string UserId { get => User.FindFirst(ClaimTypes.NameIdentifier)?.Value; }

        protected string UserName { get => User.FindFirst(ClaimTypes.Name)?.Value; }
    }
}
