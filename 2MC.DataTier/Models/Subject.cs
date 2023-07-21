﻿using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Certificates = new HashSet<Certificate>();
            Courses = new HashSet<Course>();
            SubjectMajors = new HashSet<SubjectMajor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SubjectMajor> SubjectMajors { get; set; }
    }
}
