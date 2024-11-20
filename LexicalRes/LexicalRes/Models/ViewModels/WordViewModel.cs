using System.Collections.Generic;

namespace LexicalRes.Models.ViewModels
{
    public class WordViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<WordListInfoViewModel> WordLists { get; set; }
    }
}
