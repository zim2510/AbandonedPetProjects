namespace LexicalRes.Models.Entities
{
    public class WordWordList
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int WordListId { get; set; }
        public virtual Word Word { get; set; }
        public virtual WordList WordList { get; set; }
    }
}
