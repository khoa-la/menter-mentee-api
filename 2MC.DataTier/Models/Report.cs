using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Report
    {
        public Report()
        {
            MenteeSessions = new HashSet<MenteeSession>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<MenteeSession> MenteeSessions { get; set; }
    }
}
