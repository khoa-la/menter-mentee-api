using _2MC.BusinessTier.RequestModels.User;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        private readonly IUserService _service;

        public AdminUsersController(IUserService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Users" })]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<UserViewModel>> GetUsers([FromQuery] UserViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetUsers(filter, paging);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Users" })]
        [HttpGet("{userId}")]
        public ActionResult<UserViewModel> GetUserById(int userId)
        {
            return _service.GetUserById(userId);
        }
        
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Users" })]
        [HttpPut]
        public ActionResult<UserViewModel> UpdateUser(UpdateUserRequestModel userRequestModel)
        {
            return _service.UpdateUser(userRequestModel);
        }
    }
}