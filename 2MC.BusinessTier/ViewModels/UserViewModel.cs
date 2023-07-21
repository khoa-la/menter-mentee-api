using System;//N
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class UserViewModel
    {
        public const string HiddenParams = "ImageUrl,Address";
        [Int]
        public int? Id { get; set; }
        [String]
        public string FullName { get; set; }
        [Int]
        public int? Gender { get; set; }
        public string ImageUrl { get; set; }
        [String]
        public string Phone { get; set; }
        [String]
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string Bio { get; set; }
        [Int]
        public int? Status { get; set; }
        [Int]
        public int? Badge { get; set; }
        [Int]
        public int? RoleId { get; set; }
        
    }
}
