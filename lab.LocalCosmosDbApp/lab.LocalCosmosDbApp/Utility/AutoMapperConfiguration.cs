using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace lab.LocalCosmosDbApp.Utility
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper;
        public static void RegisterMapper()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DefaultMappingProfile());
            });
            Mapper = mappingConfig.CreateMapper();
            //mappingConfig.AssertConfigurationIsValid();
        }
    }
}
