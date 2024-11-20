using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Solved
    {
        public int SolvedId { get; set; }
        public string UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
    }
}
