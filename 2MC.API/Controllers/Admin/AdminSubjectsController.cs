using _2MC.BusinessTier.RequestModels.Subject;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/subjects")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminSubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public AdminSubjectsController(ISubjectService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all subjects
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Subjects" })]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<SubjectViewModel>> GetSubjects(
            [FromQuery] SubjectViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetSubjects(filter, paging);
        }


        /// <summary>
        /// Get subject by id
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Subjects" })]
        [HttpGet("{subjectId}")]
        public ActionResult<SubjectViewModel> GetSubjectById(int subjectId)
        {
            return _service.GetSubjectById(subjectId);
        }


        /// <summary>
        /// Create a new subject
        /// </summary>
        /// <param name="subjectRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Subjects" })]
        [HttpPost]
        public ActionResult<SubjectViewModel> CreateSubject(CreateSubjectRequestModel subjectRequestModel)
        {
            return _service.CreateSubject(subjectRequestModel);
        }

        /// <summary>
        /// Update a subject
        /// </summary>
        /// <param name="subjectRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Subjects" })]
        [HttpPut]
        public ActionResult<SubjectViewModel> UpdateSubject(
            UpdateSubjectRequestModel subjectRequestModel)
        {
            return _service.UpdateSubject(subjectRequestModel);
        }


        /// <summary>
        /// Remove a subject
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Subjects" })]
        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(int courseId)
        {
            _service.DeleteSubject(courseId);
            return Ok();
        }
    }
}