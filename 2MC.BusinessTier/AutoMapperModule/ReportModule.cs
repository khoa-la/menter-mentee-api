using _2MC.BusinessTier.RequestModels.Report;
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
    public static class ReportModule
    {
        public static void ConfigReportModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Report, ReportViewModel>().ReverseMap();
            mc.CreateMap<Report, CreateReportRequestModel>().ReverseMap();
            mc.CreateMap<Report, UpdateReportRequestModel>().ReverseMap();
        }
    }
}
