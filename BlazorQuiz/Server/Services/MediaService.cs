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
 // Saves an uploaded file based on type (image or video),
        // determines the destination directory based on the file's content type (image or video),
        // creates the appropriate directory if doesn't exist
        // saves the file to location with a unique file name
        // returning the relative path to the saved file

        private async Task<string> SaveFileAsync(IFormFile file, string fileName)
        {
            string imageDirectoryPath = "wwwroot/images";
            string videoDirectoryPath = "wwwroot/videos";
            string destinationDirectory = imageDirectoryPath;

            if (file.ContentType.StartsWith("video"))
            {
                bool videoDirectoryExists = Directory.Exists(videoDirectoryPath);
                if (!videoDirectoryExists)
                {
                    Directory.CreateDirectory(videoDirectoryPath);
                }
                destinationDirectory = videoDirectoryPath;
            }
            else if(file.ContentType.StartsWith("image"))
            {
                bool imageDirectoryExists = Directory.Exists(imageDirectoryPath);
                if (!imageDirectoryExists)
                {
                    Directory.CreateDirectory(imageDirectoryPath);
                }
            }
            Directory.CreateDirectory(fileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), destinationDirectory, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            destinationDirectory = destinationDirectory.Replace("wwwroot/", "");
            var uploadedFilePath = Path.Combine(destinationDirectory, fileName);

            uploadedFilePath = uploadedFilePath.Replace("\\", "/");

            return uploadedFilePath;
        }

        // Retrieves a media file from the database based on its GUID

        public async Task<MediaModel> GetMediaByIdAsync(Guid guid)
        {
            var media = _context.MediaModels.Where(file => file.Guid == guid).Single();

            return media;
        }

       
    }
}
