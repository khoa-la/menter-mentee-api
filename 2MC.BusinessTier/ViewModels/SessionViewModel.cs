using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class SessionViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Int]
        public int? Status { get; set; }
        [Int]
        public int? CourseId { get; set; }
    }
}
