using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        
        /// <summary>
        /// Get all courses
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<SubjectViewModel>> GetSubjects(
            [FromQuery] SubjectViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetSubjects(filter, paging);
        }

        
        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{subjectId}")]
        public ActionResult<SubjectViewModel> GetSubjectById(int subjectId)
        {
            return _service.GetSubjectById(subjectId);
        }
    }
}