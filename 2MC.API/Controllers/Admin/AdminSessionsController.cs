using _2MC.BusinessTier.RequestModels.Session;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/sessions")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminSessionsController : ControllerBase
    {
        private readonly ISessionService _service;

        public AdminSessionsController(ISessionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all sessions
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Sessions" })]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<SessionViewModel>> GetSessions(
            [FromQuery] SessionViewModel filter, [FromQuery] PagingModel paging)
        {
            return _service.GetSessions(filter, paging);
        }

        /// <summary>
        /// Get session by id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Sessions" })]
        [HttpGet("{sessionId}")]
        public ActionResult<SessionViewModel> GetSessionById(int sessionId)
        {
            return _service.GetSessionById(sessionId);
        }


        /// <summary>
        /// Create a new session
        /// </summary>
        /// <param name="sessionRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Sessions" })]
        [HttpPost]
        public ActionResult<SessionViewModel> CreateSession(CreateSessionRequestModel sessionRequestModel)
        {
            return _service.CreateSession(sessionRequestModel);
        }

        
        /// <summary>
        /// Update a session
        /// </summary>
        /// <param name="sessionRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Sessions" })]
        [HttpPut]
        public ActionResult<SessionViewModel> UpdateSession(
            UpdateSessionRequestModel sessionRequestModel)
        {
            return _service.UpdateSession(sessionRequestModel);
        }

        
        /// <summary>
        /// Remove a session
        /// </summary>
        /// <param name="sessionId"></param>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Sessions" })]
        [HttpDelete("{sessionId}")]
        public void DeleteSession(int sessionId)
        {
            _service.DeleteSession(sessionId);
        }
    }
}