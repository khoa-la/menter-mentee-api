using System;

namespace _2MC.BusinessTier.RequestModels.User
{
    public class UpdateLoginUserRequestModel
    {
        public string FullName { get; set; }
        public int Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Bio { get; set; }
        public DateTime? DayOfBirth { get; set; }
    }
}