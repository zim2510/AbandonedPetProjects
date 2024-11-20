using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProblemTag
    {
        public int ProblemTagId { get; set; }
        [Required]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
        public string Tag { get; set; }
        
    }
}
