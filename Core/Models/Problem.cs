using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Problem
    {
        [Required]
        public int ProblemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Link { get; set; }
        public List<ProblemTag> ProblemTags { get; set;  }

    }
}
