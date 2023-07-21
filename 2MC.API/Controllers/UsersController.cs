using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using _2MC.BusinessTier.RequestModels.User;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get login user info
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("me")]
        [Authorize]
        public ActionResult<UserViewModel> GetUserById()
        {
            return _service.GetCurrentLoginUser();
        }

        
        /// <summary>
        /// Get login user info
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{mentorId}")]
        public ActionResult<UserViewModel> GetMentorById(int mentorId)
        {
            return _service.GetMentorById(mentorId);
        }

        /// <summary>
        /// Update current login user
        /// </summary>
        /// <param name="userRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize]
        public ActionResult<UserViewModel> UpdateUser(UpdateLoginUserRequestModel userRequestModel)
        {
            return _service.UpdateLoginUser(userRequestModel);
        }

        /// <summary>
        /// Get logged in user's orders
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("orders")]
        [Authorize]
        public ActionResult<BaseResponsePagingViewModel<OrderViewModel>> GetUserOrders([FromQuery] PagingModel  paging)
        {
            int userId = Convert.ToInt32(HttpContext?.Items["UserId"]?.ToString());
            return _service.GetOrdersByUser(userId, paging);
        }
    }
}