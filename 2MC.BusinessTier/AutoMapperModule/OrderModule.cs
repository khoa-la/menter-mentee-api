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
    public static class OrderModule
    {
        public static void ConfigOrderModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Order, OrderViewModel>().ReverseMap();
          //mc.CreateMap<Order, CreateOrderRequestModel>().ReverseMap();
        }
    }
}
