using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Session
    {
        public Session()
        {
            MenteeSessions = new HashSet<MenteeSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Status { get; set; }
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<MenteeSession> MenteeSessions { get; set; }
    }
}
