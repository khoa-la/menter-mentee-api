using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.RequestModels.Session
{
    public class CreateSessionRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Status { get; set; }
        public int? CourseId { get; set; }
    }
}
