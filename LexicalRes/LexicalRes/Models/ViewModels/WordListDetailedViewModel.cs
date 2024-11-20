using System.Collections.Generic;

namespace LexicalRes.Models.ViewModels
{
    public class WordListDetailedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WordCount { get; set; }
        public int LearnedCount { get; set; }
        public List<WordViewModel> Words { get; set; }
    }
}
