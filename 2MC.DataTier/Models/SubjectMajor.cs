using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class SubjectMajor
    {
        public int SubjectId { get; set; }
        public int MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
