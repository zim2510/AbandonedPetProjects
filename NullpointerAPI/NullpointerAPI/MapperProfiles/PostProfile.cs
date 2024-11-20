using AutoMapper;
using NullpointerAPI.Models;
using NullpointerAPI.Models.ViewModels;

namespace NullpointerAPI.MapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostViewModel>();
            CreateMap<PostViewModel, Post>();
        }
    }
}
