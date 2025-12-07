using System.ComponentModel.DataAnnotations;

namespace BookifyHotelSystem.View_Model
{
    public class LoginViewModel
    {
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
