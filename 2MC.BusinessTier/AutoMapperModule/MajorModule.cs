using _2MC.BusinessTier.RequestModels.Major;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class MajorModule
    {
        public static void ConfigMajorModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Major, MajorViewModel>().ReverseMap();
            mc.CreateMap<Major, CreateMajorRequestModel>().ReverseMap();
            mc.CreateMap<Major, UpdateMajorRequestModel>().ReverseMap();
        }
    }
}
