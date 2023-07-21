using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Course
    {
        public Course()
        {
            Orders = new HashSet<Order>();
            Sessions = new HashSet<Session>();
        }

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
        public bool IsActive { get; set; }
        public int? Type { get; set; }
        public int? LocationType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double TotalRating { get; set; }
        public int MentorId { get; set; }
        public int SubjectId { get; set; }

        public virtual User Mentor { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
