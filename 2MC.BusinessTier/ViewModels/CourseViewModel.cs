using System;
using System.Text.Json.Serialization;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class CourseViewModel
    {
        public const string HiddenParams =
            "Description,MinQuantity,MaxQuantity,Location,TotalRating,Slug,Mentor.Id,Mentor.FullName,Mentor.Gender," +
            "Mentor.ImageUrl,Mentor.Phone,Mentor.Email,Mentor.Address,Mentor.Status,Mentor.DayOfBirth,Mentor.Badge," +
            "Subject.Id,Subject.Name,Mentor.RoleId,ImageUrl,Price";

        [Int] public int? Id { get; set; }
        [String] public string Name { get; set; }
        [Int] public int? MinQuantity { get; set; }
        [Int] public int? MaxQuantity { get; set; }
        public decimal? Price { get; set; }
        [String] public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        [Int] public int? Status { get; set; }
        [Boolean] public bool? IsActive { get; set; }
        [Int] public int? Type { get; set; }
        [Int] public int? LocationType { get; set; }
        [String] public string Location { get; set; }
        public string Description { get; set; }
        public double? TotalRating { get; set; }
        [Int] public int? MentorId { get; set; }
        [Skip] public UserViewModel Mentor { get; set; }
        [Int] public int? SubjectId { get; set; }
        [Skip] public SubjectViewModel Subject { get; set; }
        [JsonIgnore] 
        [Sort] public string Sort { get; set; }
    }
}