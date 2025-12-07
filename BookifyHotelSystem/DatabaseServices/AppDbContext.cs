using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem.DatabaseServices
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomsTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomBooking>(entityBuilder =>
            {
                entityBuilder.HasKey(rb => new { rb.RoomId, rb.BookingId });
            });

            
            base.OnModelCreating(modelBuilder);
        }

         
         
    }
}
