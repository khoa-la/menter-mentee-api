using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.RequestModels.Major
{
    public class CreateMajorRequestModel
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
