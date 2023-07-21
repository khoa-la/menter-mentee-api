using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Report;
using _2MC.BusinessTier.ViewModels;
using _2MC.DataTier.Models;
using _2MC.DataTier.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reso.Core.Custom;

namespace _2MC.BusinessTier.Services
{
    public interface IReportService
    {
        BaseResponsePagingViewModel<ReportViewModel> GetReports(ReportViewModel filter, PagingModel paging);
        ReportViewModel GetReportById(int reportId);
        ReportViewModel UpdateReport(UpdateReportRequestModel reportRequestModel);
        ReportViewModel CreateReport(CreateReportRequestModel reportRequestModel);
        void DeleteReport(int reportId);
    }

    public class ReportService : IReportService
    {
        private readonly IReportRepository _repo;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ReportViewModel CreateReport(CreateReportRequestModel reportRequestModel)
        {
            var report = _mapper.Map<Report>(reportRequestModel);
            return _mapper.Map<ReportViewModel>(_repo.Create(report));
        }

        public void DeleteReport(int reportId)
        {
            var report = _repo.Get(reportId);
            if (report == null)
            {
                throw new ErrorResponse(404, "Not found reportId!");
            }

            _repo.Delete(report);
        }

        public ReportViewModel GetReportById(int reportId)
        {
            var result = _repo.Get(reportId);
            if (result == null)
            {
                throw new ErrorResponse(404, "Not found reportId!");
            }

            return _mapper.Map<ReportViewModel>(result);
        }

        public BaseResponsePagingViewModel<ReportViewModel> GetReports(ReportViewModel filter, PagingModel paging)
        {
            var result = _repo.Get()
                .ProjectTo<ReportViewModel>(_mapper.ConfigurationProvider)
                .DynamicFilter(filter)
                .PagingQueryable(paging.Page, paging.Size, CommonConstants.LimitPaging, CommonConstants.DefaultPaging);

            return new BaseResponsePagingViewModel<ReportViewModel>()
            {
                Metadata = new PagingMetadata()
                {
                    Page = paging.Page,
                    Size = paging.Size,
                    Total = result.Item1
                },
                Data = result.Item2.ToList(),
            };
        }

        public ReportViewModel UpdateReport(UpdateReportRequestModel reportRequestModel)
        {
            var report = _repo.Get(reportRequestModel.Id);
            if (report == null)
            {
                throw new ErrorResponse(404, "Not found reportId!");
            }

            _mapper.Map(reportRequestModel, report);
            return _mapper.Map<ReportViewModel>(_repo.Update(report));
        }
    }
}