using AutoMapper;
using NewsPortal.Core.Model;
using NewsPortal.Dto;

namespace NewsPortal.Data.Configuration.AutomapperProfiles
{
    public class NewsDataProfile : Profile
    {
        public NewsDataProfile()
        {
            CreateMap<Author, AuthorDto>();

            CreateMap<NewsItem, NewsItemDto>();

            CreateMap<Author, AuthorShortInfoDto>()
                .ForMember(
                    dest => dest.Name,
                    opts => opts.MapFrom(x => x.FirstName + " " + x.LastName)
                );

            CreateMap<NewsItem, NewsItemFullInfoDto>()
                .ForMember(
                    dest => dest.Author,
                    opts => opts.MapFrom(x => x.Author.FirstName + " " + x.Author.LastName)
                );
            
        }
    }
}
