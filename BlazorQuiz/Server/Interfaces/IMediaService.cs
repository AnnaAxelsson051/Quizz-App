using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;

namespace BlazorQuiz.Server.Interfaces

{
    public interface IMediaService
    {

        Task<MediaModel> GetMediaByIdAsync(Guid guid);

        Task<NewMediaViewModel> UploadMediaAsync(IFormFile file, string userId);
    }
}
