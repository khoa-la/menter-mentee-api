using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Certificate
{
    public class CreateCertificateRequestModel
    {
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}