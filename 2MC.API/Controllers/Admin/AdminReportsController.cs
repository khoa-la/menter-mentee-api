using _2MC.BusinessTier.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Report;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/reports")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public AdminReportsController(IReportService service)
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
        [SwaggerOperation(Tags = new[] { "Admin - Reports" })]
        [HttpGet]
        [HiddenParams(ReportViewModel.HiddenParams)]
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
        [SwaggerOperation(Tags = new[] { "Admin - Reports" })]
        [HttpGet("{reportId}")]
        public ActionResult<ReportViewModel> GetReportById(int reportId)
        {
            return _service.GetReportById(reportId);
        }


        /// <summary>
        /// Update a report
        /// </summary>
        /// <param name="reportRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Reports" })]
        [HttpPut]
        public ActionResult<ReportViewModel> UpdateReport(UpdateReportRequestModel reportRequestModel)
        {
            return _service.UpdateReport(reportRequestModel);
        }

        /// <summary>
        /// Remove a report
        /// </summary>
        /// <param name="reportId"></param>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Reports" })]
        [HttpDelete("{reportId}")]
        public void DeleteReport(int reportId)
        {
            _service.DeleteReport(reportId);
        }
    }
}