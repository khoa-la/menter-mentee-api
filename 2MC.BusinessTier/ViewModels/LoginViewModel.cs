using System;

namespace _2MC.BusinessTier.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            IsFirstLogin = false;
        }

        public string AccessToken { get; set; }
        public int UserId { get; set; }
        public bool IsFirstLogin { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
    }
}