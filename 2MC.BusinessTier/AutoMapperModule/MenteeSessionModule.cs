using _2MC.BusinessTier.RequestModels.MenteeSession;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class MeeteeSessionModule
    {
        public static void ConfigMenteeSessionModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<MenteeSession, MenteeSessionViewModel>().ReverseMap();
            mc.CreateMap<MenteeSession, UpdateAttendanceRequestModel>().ReverseMap();
        }
    }
}
