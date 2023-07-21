using System.ComponentModel.DataAnnotations;

namespace _2MC.BusinessTier.RequestModels.Authentication
{
    public class LoginByFireBaseTokenRequest
    {
        [Required]
        public string IdToken { get; set; }

        public string FcmToken { get; set; } = "";
    }
}
