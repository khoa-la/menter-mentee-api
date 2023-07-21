using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class MenteeSessionViewModel
    {
        [Int] public int Id { get; set; }
        [Int] public int? MenteeId { get; set; }
        public UserViewModel Mentee { get; set; }
        [Int] public int? SessionId { get; set; }
        [Int] public int? IsAttended { get; set; }
        [Int] public int? ReportId { get; set; }
        [Int] public int? Rating { get; set; }
    }
}
