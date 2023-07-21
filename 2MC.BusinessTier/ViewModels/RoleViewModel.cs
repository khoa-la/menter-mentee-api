using System;//N
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2MC.BusinessTier.Commons;

namespace _2MC.BusinessTier.ViewModels
{
    public class RoleViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Name { get; set; }
    }
}
