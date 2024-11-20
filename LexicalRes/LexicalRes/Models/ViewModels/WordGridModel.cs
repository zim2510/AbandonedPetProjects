using System.Collections.Generic;

namespace LexicalRes.Models.ViewModels
{
    public class WordGridModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<int> WordListIds { get; set; }
    }
}
