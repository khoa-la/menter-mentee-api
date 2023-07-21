using _2MC.BusinessTier.RequestModels.Course;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class CourseModule
    {
        public static void ConfigCourseModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Course, CourseViewModel>().ReverseMap();
            mc.CreateMap<Course, CreateCourseRequestModel>().ReverseMap();
            mc.CreateMap<Course, UpdateCourseRequestModel>().ReverseMap();
        }
    }
}