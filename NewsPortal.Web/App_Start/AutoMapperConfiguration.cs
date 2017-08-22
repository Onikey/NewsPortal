using AutoMapper;
using NewsPortal.Data.Configuration.AutomapperProfiles;

namespace NewsPortal.Web
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config => {
                config.AddProfile(new NewsDataProfile());
            });
        }
    }
}