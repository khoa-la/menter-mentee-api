using System;
using System.Collections.Generic;

#nullable disable

namespace _2MC.DataTier.Models
{
    public partial class Order
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

        public virtual Course Course { get; set; }
        public virtual User Mentee { get; set; }
    }
}
