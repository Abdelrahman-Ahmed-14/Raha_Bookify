using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class RoomBookingRepository : BaseRepository<RoomBooking>, IRoomBookingRepository
    {
        public DbSet<RoomBooking> bookingsRepo { get; set; }
        public RoomBookingRepository(AppDbContext context) : base(context)
        {
            bookingsRepo = context.RoomBookings;
        }
    }
}
