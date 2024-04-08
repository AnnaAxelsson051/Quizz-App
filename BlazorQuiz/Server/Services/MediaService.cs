using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;
using BlazorQuiz.Server.Interfaces;

// Fixad 

namespace BlazorQuiz.Server.Services
{
    public class MediaService : IMediaService
    {
        private readonly ApplicationDbContext _context;
        public MediaService(ApplicationDbContext context)
        {
            _context = context;
        }

   

        // Uploading media files, associate it with user
        // generating a unique identifier for each file,
        // saving it to a specified file path and storing relevant metadata in db
        // Constructs and returns a view model with details of the uploaded media 

        public async Task<NewMediaViewModel> UploadMediaAsync(IFormFile file, string userId)
        {
            var guid = Guid.NewGuid();
            var fileName = $"{guid}{Path.GetExtension(file.FileName)}";
            var filePath = await SaveFileAsync(file, fileName); 

            var uploadedFile = new MediaModel
            {
                Guid = guid,
                Path = filePath,
                Type = file.ContentType,
                UserRefId = userId,
                Description = "Example description" 
                
            };

            _context.Add(uploadedFile);
            await _context.SaveChangesAsync();

            var mediaViewModel = new NewMediaViewModel
            {
                Guid = uploadedFile.Guid,
                Path = uploadedFile.Path
            };

            return mediaViewModel;

        }

       
    }
}
