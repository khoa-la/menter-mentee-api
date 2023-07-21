using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class MenteeSession
    {
        public int Id { get; set; }
        public int? MenteeId { get; set; }
        public int? SessionId { get; set; }
        public int? IsAttended { get; set; }
        public int? ReportId { get; set; }
        public int? Rating { get; set; }

        public virtual User Mentee { get; set; }
        public virtual Report Report { get; set; }
        public virtual Session Session { get; set; }
    }
}
