using _2MC.BusinessTier.Commons;
using _2MC.BusinessTier.Services;
using _2MC.BusinessTier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace _2MC.API.Controllers.Admin
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/certificates")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminCertificatesController : ControllerBase
    {
        private readonly ICertificateService _service;

        public AdminCertificatesController(ICertificateService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all certificates
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Certificates" })]
        [HiddenParams(CertificateViewModel.HiddenParams)]
        [HttpGet]
        public ActionResult<BaseResponsePagingViewModel<CertificateViewModel>> GetCertificates(
            [FromQuery] CertificateViewModel filter,
            [FromQuery] PagingModel paging)
        {
            return _service.GetCertificates(filter, paging);
        }

        /// <summary>
        /// Get certificate by id
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Certificates" })]
        [HttpGet("{certificateId}")]
        public ActionResult<CertificateViewModel> GetCertificateById(int certificateId)
        {
            return _service.GetCertificateById(certificateId);
        }

        /// <summary>
        /// Delete a certificate
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Tags = new[] { "Admin - Certificates" })]
        [HttpDelete("{certificateId}")]
        public ActionResult DeleteCertificate(int certificateId)
        {
            _service.DeleteCertificate(certificateId);
            return Ok();
        }
    }
}