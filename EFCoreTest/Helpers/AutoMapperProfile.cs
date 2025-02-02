using AutoMapper;
using EFCoreTest.Models;
using EFCoreTest.Models.second_dbfirstTry;

namespace EFCoreTest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookModel>()
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ReverseMap();
        }
    }
}
