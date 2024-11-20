using System;
using System.ComponentModel.DataAnnotations;

namespace NullpointerAPI.Models.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
