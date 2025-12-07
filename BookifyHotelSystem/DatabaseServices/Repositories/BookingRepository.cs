using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public DbSet<Booking> bookingRepo { get; set; }
        public BookingRepository(AppDbContext context) : base(context)
        {
            bookingRepo = context.Bookings;
        }


    }
}
