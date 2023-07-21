using _2MC.BusinessTier.RequestModels.User; //N
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class UserModule
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, UserViewModel>().ReverseMap();
            mc.CreateMap<User, CreateUserRequestModel>().ReverseMap();
            mc.CreateMap<User, UpdateUserRequestModel>().ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            mc.CreateMap<User, UpdateLoginUserRequestModel>().ReverseMap()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}