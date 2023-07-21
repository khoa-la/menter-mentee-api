using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Major
    {
        public Major()
        {
            SubjectMajors = new HashSet<SubjectMajor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<SubjectMajor> SubjectMajors { get; set; }
    }
}
