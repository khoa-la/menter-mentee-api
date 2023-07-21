using System;
using System.Collections.Generic;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Certificate;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface ICertificateService
    {
        BaseResponsePagingViewModel<CertificateViewModel> GetCertificates(CertificateViewModel filter,
            PagingModel paging);

        CertificateViewModel GetCertificateById(int certificateId);
        CertificateViewModel UpdateCertificate(UpdateCertificateRequestModel certificateRequestModel);
        CertificateViewModel CreateCertificate(CreateCertificateRequestModel certificateRequestModel);
        void DeleteCertificate(int certificateId);

        BaseResponsePagingViewModel<CertificateViewModel> GetLoginUserCertificates(CertificateViewModel filter,
            PagingModel paging);
    }

    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CertificateService(ICertificateRepository repo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public CertificateViewModel CreateCertificate(CreateCertificateRequestModel certificateRequestModel)
        {
            
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            var certificate = _mapper.Map<Certificate>(certificateRequestModel);
            certificate.MentorId = mentorId;
            return _mapper.Map<CertificateViewModel>(_repo.Create(certificate));
        }

        public void DeleteCertificate(int certificateId)
        {
            var certificate = _repo.Get(certificateId);
            if (certificate == null)
            {
                throw new ErrorResponse(404, "Not found certificateId!");
            }

            _repo.Delete(certificate);
        }

        public CertificateViewModel GetCertificateById(int certificateId)
        {
            var result = _repo.Get(certificateId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found certificateId!");
            }

            return _mapper.Map<CertificateViewModel>(result);
        }

        public BaseResponsePagingViewModel<CertificateViewModel> GetCertificates(CertificateViewModel filter,
            PagingModel paging)
        {
            var result = _repo.Get()
                .ProjectTo<CertificateViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);
            return new BaseResponsePagingViewModel<CertificateViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = result.Item2.ToList()
            };
        }

        public BaseResponsePagingViewModel<CertificateViewModel> GetLoginUserCertificates(CertificateViewModel filter,
            PagingModel paging)
        {
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            var result = _repo.Get(x => x.MentorId == mentorId)
                .ProjectTo<CertificateViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);
            return new BaseResponsePagingViewModel<CertificateViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = result.Item2.ToList()
            };
        }

        public CertificateViewModel UpdateCertificate(UpdateCertificateRequestModel certificateRequestModel)
        {
            int mentorId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["UserId"]?.ToString());
            var certificate = _repo.Get()
                .SingleOrDefault(x => x.Id == certificateRequestModel.Id && x.MentorId == mentorId);
            if (certificate == null)
            {
                throw new ErrorResponse(404, "Not found certificateId!");
            }

            _mapper.Map(certificateRequestModel, certificate);
            return _mapper.Map<CertificateViewModel>(_repo.Update(certificate));
        }
    }
}