using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Certificate
{
    public class UpdateCertificateRequestModel
    {
        [Required]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
    }
}