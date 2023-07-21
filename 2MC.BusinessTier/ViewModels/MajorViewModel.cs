using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class MajorViewModel
    {

        public const string HiddenParams = "ImageUrl";
        [Int]
        public int? Id { get; set; }
        [String]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Sort]
        public string Sort { get; set; }
    }

}
