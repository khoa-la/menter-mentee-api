﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.RequestModels.Report
{
    public class UpdateReportRequestModel
    {
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
    }
}