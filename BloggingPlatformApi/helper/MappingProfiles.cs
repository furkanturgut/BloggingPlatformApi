using AutoMapper;
using BloggingPlatformApi.Dto_s;
using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Writer,WriterDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
        }
    }
}
