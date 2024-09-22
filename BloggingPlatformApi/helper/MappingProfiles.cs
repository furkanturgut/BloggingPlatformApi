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
        }
    }
}
