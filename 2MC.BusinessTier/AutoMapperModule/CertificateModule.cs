using _2MC.BusinessTier.RequestModels.Certificate;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using AutoMapper;

namespace _2MC.BusinessTier.AutoMapperModule
{
    public static class CertificateModule
    {
        public static void ConfigCertificateModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Certificate, CertificateViewModel>().ReverseMap();
            mc.CreateMap<Certificate, CreateCertificateRequestModel>().ReverseMap();
            mc.CreateMap<Certificate, UpdateCertificateRequestModel>().ReverseMap();
        }
    }
}