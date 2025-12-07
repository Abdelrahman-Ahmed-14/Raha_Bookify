using System.ComponentModel.DataAnnotations;

namespace BookifyHotelSystem.View_Model
{
    public class RegiterViewModel
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Compare("Password")]
        public string Confirmpasword { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
