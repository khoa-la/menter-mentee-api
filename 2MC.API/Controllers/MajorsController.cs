using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Major;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/majors")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
        private readonly IMajorService _service;

        public MajorsController(IMajorService service)
        {
            _service = service;
        }
 
        /// <summary>
        /// Get all majors
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [HiddenParams(MajorViewModel.HiddenParams)]
        public ActionResult<BaseResponsePagingViewModel<MajorViewModel>> GetMajors([FromQuery] MajorViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetMajors(filter, paging);
        }

        /// <summary>
        /// Get major by id
        /// </summary>
        /// <param name="majorId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{majorId}")]
        public ActionResult<MajorViewModel> GetMajorById(int majorId)
        {
            return _service.GetMajorById(majorId);
        }
    }
}
