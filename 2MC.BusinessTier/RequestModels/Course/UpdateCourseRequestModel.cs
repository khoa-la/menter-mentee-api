using System;
using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Course
{
    public class UpdateCourseRequestModel
    {
        public UpdateCourseRequestModel(DateTime? updateDate)
        {
            UpdateDate = Commons.Utils.GetCurrentDateTime();
        }

        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal Price { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public int? Type { get; set; }
        public int? LocationType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double TotalRating { get; set; }
        public int SubjectId { get; set; }
    }
}