using System;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reso.Core.Custom;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mentee-sessions")]
    [ApiController]
    public class MenteeSessionsController : ControllerBase
    {
        private readonly IMenteeSessionService _service;

        public MenteeSessionsController(IMenteeSessionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get logged in mentee's session by courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{courseId}")]
        [Authorize]
        public ActionResult<BaseResponsePagingViewModel<MenteeSessionViewModel>> GetMenteeSessionByCourse(int courseId,
            [FromQuery] PagingModel paging)
        {
            int menteeId = Convert.ToInt32(HttpContext?.Items["UserId"]?.ToString());
            return _service.GetMenteeSessionByCourse(menteeId, courseId, paging);
        }
    }
}