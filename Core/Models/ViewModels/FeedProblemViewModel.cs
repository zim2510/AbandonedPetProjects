using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class FeedProblemViewModel
    {
        [Required]
        public int ProblemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Link { get; set; }
        [Required]
        public bool IsSolved { get; set; }
        public List<ProblemTag> ProblemTags;
    }
}
