using System;
using System.ComponentModel.DataAnnotations;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.RequestModels.Course
{
    public class CreateCourseRequestModel
    {
        public CreateCourseRequestModel(DateTime createDate)
        {
            CreateDate = Utils.GetCurrentDateTime();
            UpdateDate = Utils.GetCurrentDateTime();
        }

        public string Name { get; set; }
        [Required] public int MinQuantity { get; set; }
        [Required] public int MaxQuantity { get; set; }
        [Required] public decimal Price { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public int? LocationType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        [Required] public int SubjectId { get; set; }
    }
}