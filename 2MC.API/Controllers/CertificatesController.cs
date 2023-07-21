using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.RequestModels.Certificate;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2MC.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/certificates")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _service;

        public CertificatesController(ICertificateService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Get all certificates of login user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [HiddenParams(CertificateViewModel.HiddenParams)]
        public ActionResult<BaseResponsePagingViewModel<CertificateViewModel>> GetCertificate([FromQuery] CertificateViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetLoginUserCertificates(filter, paging);
        }

        /// <summary>
        /// Get certificate by id
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{certificateId}")]
        public ActionResult<CertificateViewModel> GetCertificateById(int certificateId)
        {
            return _service.GetCertificateById(certificateId);
        }

        /// <summary>
        /// Create a new certificate
        /// </summary>
        /// <param name="certificateRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Authorize]
        public ActionResult<CertificateViewModel> CreateCertificate(CreateCertificateRequestModel certificateRequestModel)
        {
            return _service.CreateCertificate(certificateRequestModel);
        }

        /// <summary>
        /// Update a certificate
        /// </summary>
        /// <param name="certificateRequestModel"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Authorize]
        public ActionResult<CertificateViewModel> UpdateCertificate(UpdateCertificateRequestModel certificateRequestModel)
        {
            return _service.UpdateCertificate( certificateRequestModel);
        }
    }
}
