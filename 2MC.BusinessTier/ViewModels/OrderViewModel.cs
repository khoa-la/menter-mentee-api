using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public int MenteeId { get; set; }
        public int CourseId { get; set; }
    }
}
