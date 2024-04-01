using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorQuiz.Server.Models
{
    // Fixad
    // Representing media entities

    public class MediaModel
    {
        [Key]
        public int Id { get; set; }

        // Stores a globally unique identifier for the media item
        public Guid Guid { get; set; }
        public string Type { get; set; } // Type of media
        public string Description { get; set; }
        public string Path { get; set; } // Path to access the media content

        [ForeignKey("User")]
        public string UserRefId { get; set; }

        // To access associated ApplicationUser entity,for navigation from
        // media items to their resp users
        public virtual ApplicationUser User { get; set; }
    }
}
