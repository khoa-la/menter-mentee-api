using _2MC.BusinessTier.RequestModels.Role;
using _2MC.BusinessTier.RequestModels.User;//N
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
    public static class RoleModule
    {
        public static void ConfigRoleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Role, RoleViewModel>().ReverseMap();
            mc.CreateMap<Role, CreateRoleRequestModel>().ReverseMap();
            mc.CreateMap<Role, UpdateRoleRequestModel>().ReverseMap();
        }
    }
}
