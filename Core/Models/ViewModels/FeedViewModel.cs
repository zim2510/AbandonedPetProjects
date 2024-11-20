using System.Collections.Generic;

namespace Core.Models.ViewModels
{
    public class FeedViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int SolvedCount { get; set; }
        public List<FeedProblemViewModel> Problems { get; set; }
    }
}
