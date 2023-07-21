using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/orders")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public AdminOrdersController(IOrderService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Orders" })]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<OrderViewModel>> GetMajor([FromQuery] OrderViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetOrders(filter, paging);
        }
    }
}
