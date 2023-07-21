using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Course;
using _2MC.BusinessTier.RequestModels.MenteeSession;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using _2MC.BusinessTier.ViewModels.FilterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service, IDistributedCache distributedCache)
        {
            _service = service;
        }

        
        /// <summary>
        /// Get all courses
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterModel"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HiddenParams(CourseViewModel.HiddenParams)]
        [HttpGet]
        [Cache(30000)]
        public ActionResult<BaseResponsePagingViewModel<CourseViewModel>> GetCourses([FromQuery] CourseViewModel filter,
            [FromQuery]CourseFilterModel filterModel,
            [FromQuery] PagingModel paging)
        {
            return Ok(_service.GetCourses(filter,filterModel, paging));
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{courseId}")]
        public ActionResult<CourseViewModel> GetCourseById(int courseId)
        {
            return _service.GetCourseById(courseId);
        }

        
        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="courseRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPost]
        // [Authorize (Roles = "Mentor")]
        public ActionResult<CourseViewModel> CreateCourse(CreateCourseRequestModel courseRequestModel)
        {
            return _service.CreateCourse(courseRequestModel);
        }

        
        /// <summary>
        /// Update a course
        /// </summary>
        /// <param name="courseRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize (Roles = "Mentor")]
        public ActionResult<CourseViewModel> UpdateCourse(UpdateCourseRequestModel courseRequestModel)
        {
            return _service.UpdateCourse( courseRequestModel);
        }


        /// <summary>
        /// Remove a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpDelete("{courseId}")]
        [Authorize (Roles = "Mentor")]

        public ActionResult DeleteCourse(int courseId)
        {
            _service.DeleteCourseMentor(courseId);
            return Ok();
        }

        /// <summary>
        /// Get sessions of course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{courseId}/sessions")]
        [Authorize(Roles = "Mentor")]
        public ActionResult<List<SessionViewModel>> GetSessionsByCourse(int courseId)
        {
            int userId = Convert.ToInt32(HttpContext?.Items["UserId"]?.ToString());
            return _service.GetSessionsByCourse(courseId, userId);
        }
        /// <summary>
        /// Get details of session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{courseId}/sessions/{sessionId}")]
        [Authorize(Roles = "Mentor")]
        public ActionResult<List<MenteeSessionViewModel>> GetMenteesOfSession(int sessionId)
        {
            int userId = Convert.ToInt32(HttpContext?.Items["UserId"]?.ToString());
            return _service.GetMenteesOfSession(sessionId, userId);
        }
        /// <summary>
        /// Update session's attendances
        /// </summary>
        /// <param name="updateAttendanceRequestModels"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPut("sessions/update-attendance")]
        [Authorize(Roles = "Mentor")]
        public ActionResult UpdateSessionAttendance(List<UpdateAttendanceRequestModel> updateAttendanceRequestModels)
        {
            _service.UpdateSessionAttendance(updateAttendanceRequestModels);
            return Ok();
        }
    }
}