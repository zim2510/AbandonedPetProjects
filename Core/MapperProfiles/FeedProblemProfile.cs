using Core.Models;
using Core.Models.ViewModels;
using AutoMapper;

namespace Core.MapperProfiles
{
    public class FeedProblemProfile : Profile
    {
        public FeedProblemProfile()
        {
            CreateMap<Problem, FeedProblemViewModel>()
                .ForMember(dest => dest.IsSolved, opt => opt.Ignore());
        }
    }
}
