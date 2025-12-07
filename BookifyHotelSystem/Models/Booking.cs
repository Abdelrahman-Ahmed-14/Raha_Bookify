using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookifyHotelSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingNo { get; set; }
        public string UserName { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public int TotalPrice { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<RoomBooking>? RoomBookings { get; set; } = new List<RoomBooking>();
    }
}
