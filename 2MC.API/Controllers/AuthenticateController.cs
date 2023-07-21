using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2MC.BusinessTier.RequestModels.Authentication;
using _2MC.BusinessTier.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthenticateController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login with firebase
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        public async Task<ActionResult> LoginWithEmail([FromBody] LoginByFireBaseTokenRequest request)
        {
            var result = await _service.LoginByEmail(request.IdToken, request.FcmToken);
            return Ok(result);
        }
    }
}