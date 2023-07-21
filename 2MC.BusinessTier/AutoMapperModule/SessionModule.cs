using _2MC.BusinessTier.RequestModels.Session;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class SessionModule
    {
        public static void ConfigSessionModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Session, SessionViewModel>().ReverseMap();
            mc.CreateMap<Session, CreateSessionRequestModel>().ReverseMap();
            mc.CreateMap<Session, UpdateSessionRequestModel>().ReverseMap();
        }
    }
}
