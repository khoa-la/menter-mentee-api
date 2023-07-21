using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Subject
{
    public class CreateSubjectRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}