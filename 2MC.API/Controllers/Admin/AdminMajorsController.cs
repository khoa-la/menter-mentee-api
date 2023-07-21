using _2MC.BusinessTier.RequestModels.Major;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/majors")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminMajorsController : ControllerBase
    {
        private readonly IMajorService _service;

        public AdminMajorsController(IMajorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all certificates
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Majors" })]
        [HttpGet]
        [HiddenParams(MajorViewModel.HiddenParams)]
        public ActionResult<BaseResponsePagingViewModel<MajorViewModel>> GetMajor([FromQuery] MajorViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetMajors(filter, paging);
        }

        /// <summary>
        /// Get certificate by id
        /// </summary>
        /// <param name="majorId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Majors" })]
        [HttpGet("{majorId}")]
        public ActionResult<MajorViewModel> GetMajorById(int majorId)
        {
            return _service.GetMajorById(majorId);
        }

        /// <summary>
        /// Create a new major
        /// </summary>
        /// <param name="majorRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Majors" })]
        [HttpPost]
        public ActionResult<MajorViewModel> CreateMajor(CreateMajorRequestModel majorRequestModel)
        {
            return _service.CreateMajor(majorRequestModel);
        }

        /// <summary>
        /// Remove a major
        /// </summary>
        /// <param name="majorId"></param>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Majors" })]
        [HttpDelete("{majorId}")]
        public void DeleteMajor(int majorId)
        {
            _service.DeleteMajor(majorId);
        }

        /// <summary>
        /// Update a major
        /// </summary>
        /// <param name="majorRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Majors" })]
        [HttpPut]
        public ActionResult<MajorViewModel> UpdateMajor(UpdateMajorRequestModel majorRequestModel)
        {
            return _service.UpdateMajor(majorRequestModel);
        }
    }
}