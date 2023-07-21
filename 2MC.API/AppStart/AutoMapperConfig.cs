using _2MC.BusinessTier.AutoMapperModule;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace _2MC.API.AppStart
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.ConfigCourseModule();
                mc.ConfigMajorModule();
                mc.ConfigSessionModule();
                mc.ConfigSubjectModule();
                mc.ConfigReportModule();
                mc.ConfigCertificateModule();
                mc.ConfigUserModule();
                mc.ConfigRoleModule();
                mc.ConfigMenteeSessionModule();
                mc.ConfigOrderModule();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}