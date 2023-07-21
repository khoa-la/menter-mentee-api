using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class ReportViewModel
    {
        public const string HiddenParams = "Content";
        [Int] public int? Id { get; set; }
        [String] public string Content { get; set; }
        [Int] public int? Type { get; set; }
        [Int] public int? Status { get; set; }
        [Sort] public string Sort { get; set; }
    }
}