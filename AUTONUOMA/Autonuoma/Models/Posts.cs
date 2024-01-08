using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
    public class Post
    {
        [DisplayName("Content")]
        [Required]
        public string Content { get; set; }

        [DisplayName("Post ID")]
        public int PostId { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Image ID")]
        public int? ImageId { get; set; }  // Nullable if an image is optional

        [DisplayName("Post Name")]
        public string PostName { get; set; }

        [DisplayName("Created At")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedAt { get; set; }

    }
}
