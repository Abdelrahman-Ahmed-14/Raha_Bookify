using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BookifyHotelSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name ="Price Per Night")]
        public decimal PricePerNight { get; set; }
        public string? Image { get; set; } 

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Display(Name ="Is Abailable")]
        public bool IsAvailable { get; set; }

        [Display(Name ="Room Type")]
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }


        public virtual ICollection<RoomBooking>? RoomBookings { get; set; } = new List<RoomBooking>();
    }
}
