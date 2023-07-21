using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public int MentorId { get; set; }

        public virtual User Mentor { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
