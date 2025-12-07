using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookifyHotelSystem.Models
{
    public class RoomBooking
    {
        [Key]
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [Key]
        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}
