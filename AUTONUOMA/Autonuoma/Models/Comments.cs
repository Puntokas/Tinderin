using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Models
{
    public class Comment
    {
        [DisplayName("Comment ID")]
        public int CommentId { get; set; }

        [DisplayName("Content")]
        [Required]
        public string Content { get; set; }

        [DisplayName("Post ID")]
        public int PostId { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Created At")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}