using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexicalRes.Models.ViewModels
{
    public class LearnViewModel
    {
        public List<WordListInfoViewModel> PredefinedLists { get; set; }
        public int LearnedCount { get; set; }
        public int LearningCount { get; set; }
    }
}
