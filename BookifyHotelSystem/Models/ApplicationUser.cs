using Microsoft.AspNetCore.Identity;

namespace BookifyHotelSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
