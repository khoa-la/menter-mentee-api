using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/roles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminRolesController : ControllerBase
    {
        // private readonly IRoleService _service;
        //
        // public AdminRolesController(IRoleService service)
        // {
        //     _service = service;
        // }
        //
        // [MapToApiVersion("1.0")]
        // [HttpGet]
        // public ActionResult<BaseResponsePagingViewModel<RoleViewModel>> GetRoles([FromQuery] RoleViewModel filter,
        //     [FromQuery] PagingModel paging)
        // {
        //     return _service.GetRoles(filter, paging);
        // }
        //
        // [MapToApiVersion("1.0")]
        // [HttpGet("{roleId}")]
        // public ActionResult<RoleViewModel> GetRoleById(int roleId)
        // {
        //     return _service.GetRoleById(roleId);
        // }
        //
        // [MapToApiVersion("1.0")]
        // [HttpPost]
        // public ActionResult<RoleViewModel> CreateRole(CreateRoleRequestModel roleRequestModel)
        // {
        //     return _service.CreateRole(roleRequestModel);
        // }
        //
        // [MapToApiVersion("1.0")]
        // [HttpPut] 
        // public ActionResult<RoleViewModel> UpdateRole(int roleId, UpdateRoleRequestModel roleRequestModel)
        // {
        //     return _service.UpdateRole(roleId, roleRequestModel);
        // }
        
    }
}
