using _2MC.BusinessTier.RequestModels.Course;
using _2MC.BusinessTier.RequestModels.Subject;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class SubjectModule
    {
        public static void ConfigSubjectModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Subject, SubjectViewModel>().ReverseMap();
            mc.CreateMap<Subject, CreateSubjectRequestModel>().ReverseMap();
            mc.CreateMap<Subject, UpdateSubjectRequestModel>().ReverseMap();
        }
    }
}