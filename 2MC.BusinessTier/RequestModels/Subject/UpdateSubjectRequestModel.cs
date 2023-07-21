using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Subject
{
    public class UpdateSubjectRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}