using BookifyHotelSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookifyHotelSystem.View_Model
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Is Abailable")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Room Type")]
        public string RoomTypeName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public int Nights
        {
            get
            {
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    return (EndDate.Value - StartDate.Value).Days;
                }
                return 0;
            }
        }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                return PricePerNight * Nights;
            }
        }
    }
}
