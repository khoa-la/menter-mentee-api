using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class User
    {
        public User()
        {
            Certificates = new HashSet<Certificate>();
            Courses = new HashSet<Course>();
            MenteeSessions = new HashSet<MenteeSession>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public int? Status { get; set; }
        public int? Badge { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Bio { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MenteeSession> MenteeSessions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
