using BlazorQuiz.Server.Data;
using Microsoft.AspNetCore.Mvc;
using BlazorQuiz.Shared.ViewModels;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Services;
using BlazorQuiz.Server.Interfaces;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : BaseController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService )
        {
            _mediaService = mediaService;
        }

        // Uploading media files, perform check for file size and type before saving

        [HttpPost]
        public async Task<IActionResult> UploadMediaAsync([FromForm] IFormFile file)
        {
            long megaByteSize = 1024 * 1024;
            int maxFileSizeInMB = 13;
           
            long maxAllowedSizeInBytes = maxFileSizeInMB * megaByteSize;
            string[] allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".mp4" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (file.Length > maxAllowedSizeInBytes)
            {
                return BadRequest("The size of the file you are attemting to upload is to large");
            }

            if (string.IsNullOrEmpty(fileExtension) || !allowedFileExtensions.Contains(fileExtension))
            {
                return BadRequest("The file type: " + file.ContentType + " is not a valid filetype");
            }

            var uploadedMedia = await _mediaService.UploadMediaAsync(file, UserId);

            return Ok(uploadedMedia);
        }

        // Retrieves media data based on the provided GUID

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetMedia(Guid guid)
        {

            var mediaData = await _mediaService.GetMediaByIdAsync(guid);

            MediaViewModel returnMedia = new()
            {
                Type = mediaData.Type,
                Path = mediaData.Path
            };

            return Ok(returnMedia);
        }

    }
}
