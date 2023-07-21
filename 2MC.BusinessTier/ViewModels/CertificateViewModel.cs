using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class CertificateViewModel
    {
        public const string HiddenParams = "ImageUrl";
        [Int]
        public int? Id { get; set; }
        public string ImageUrl { get; set; }
        [String]
        public string Name { get; set; }
        [Int]
        public int? SubjectId { get; set; }
        [Int]
        public int? MentorId { get; set; }
        [Sort]
        public string Sort { get; set; }
    }
}