using System.Collections.Generic;

namespace LexicalRes.Models.ViewModels
{
    public class WordListGridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
        public string WordIds { get; set; }
    }
}
