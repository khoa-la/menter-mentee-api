using _2MC.BusinessTier.RequestModels.Report;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all reports
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<ReportViewModel>> GetReports([FromQuery] ReportViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetReports(filter, paging);
        }

        /// <summary>
        /// Get report by id
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{reportId}")]
        public ActionResult<ReportViewModel> GetReportById(int reportId)
        {
            return _service.GetReportById(reportId);
        }

        /// <summary>
        /// Create a new report
        /// </summary>
        /// <param name="reportRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Authorize]
        public ActionResult<ReportViewModel> CreateReport(CreateReportRequestModel reportRequestModel)
        {
            return _service.CreateReport(reportRequestModel);
        }

        /// <summary>
        /// Update a report
        /// </summary>
        /// <param name="reportRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize]
        public ActionResult<ReportViewModel> UpdateReport(UpdateReportRequestModel reportRequestModel)
        {
            return _service.UpdateReport(reportRequestModel);
        }
    }
}