using System;//N
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.BusinessTier.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Bio { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public int? Status { get; set; }
        public int? Badge { get; set; }
        public int? RoleId { get; set; }
    }
}
