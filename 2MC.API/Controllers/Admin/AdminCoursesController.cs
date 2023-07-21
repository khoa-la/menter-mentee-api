using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Course;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using _2MC.BusinessTier.ViewModels.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/courses")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminCoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public AdminCoursesController(ICourseService service)
        {
            _service = service;
        }

        /// <summary>
        /// Remove a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Courses" })]
        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCourse(int courseId)
        {
            _service.DeleteCourseAdmin(courseId);
            return Ok();
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterModel"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Courses" })]
        [HiddenParams(CourseViewModel.HiddenParams)]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<CourseViewModel>> GetCourses([FromQuery] CourseViewModel filter,
            [FromQuery]CourseFilterModel filterModel,
            [FromQuery] PagingModel paging)
        {
            return _service.GetCourses(filter,filterModel, paging);
        }
        
        /// <summary>
        /// Get all certificates of a courses
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Courses" })]
        [HttpGet("{courseId}/certificates")]
        public ActionResult<List<CertificateViewModel>> GetCourseCertificates(int courseId)
        {
            return _service.GetCertificatesByCourse(courseId);
        }

        /// <summary>
        /// Update a course
        /// </summary>
        /// <param name="courseRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Courses" })]
        [HttpPut("{courseId}")]
        public ActionResult<CourseViewModel> UpdateCourse(UpdateCourseRequestModel courseRequestModel)
        {
            return _service.UpdateAdminCourse(courseRequestModel);
        }
    }
}