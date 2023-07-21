using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.RequestModels.MenteeSession
{
    public class UpdateAttendanceRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IsAttended { get; set; }
    }
}
