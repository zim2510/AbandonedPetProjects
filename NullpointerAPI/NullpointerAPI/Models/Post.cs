using System;
using System.ComponentModel.DataAnnotations;

namespace NullpointerAPI.Models
{
    public class Post
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
    }
}
