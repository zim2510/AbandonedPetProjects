using AutoMapper;
using LexicalRes.Data;
using LexicalRes.Models.Entities;
using LexicalRes.Models.ViewModels;
using System.Linq;

namespace LexicalRes.Models.MapperProfiles
{
    public class MiscProfile : Profile
    {
        public MiscProfile()
        {
            CreateMap<Word, WordViewModel>();

            CreateMap<Word, WordGridModel>()
                .ForMember(dest => dest.WordListIds, opt => opt.MapFrom(src => src.WordLists.Select(x => x.Id).ToList()));

            CreateMap<WordGridModel, Word>();

            CreateMap<WordList, WordListGridModel>()
                .ForMember(dest => dest.WordIds, opt => opt.MapFrom(src => string.Join(", ", src.Words.Select(x => x.Id.ToString()).ToList())));

            CreateMap<WordListGridModel, WordList>();

            CreateMap<WordList, WordListInfoViewModel>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Words.Count));

            CreateMap<WordList, WordListDetailedViewModel>();

            CreateMap<Meaning, MeaningViewModel>()
                .ForMember(x => x.Examples, opt => opt.MapFrom(src => src.Examples.Select(x => x.Value).ToList()));

        }
    }
}