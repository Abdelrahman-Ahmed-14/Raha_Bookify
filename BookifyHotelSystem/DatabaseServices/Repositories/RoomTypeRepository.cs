using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
    {
        public DbSet<RoomType> roomTypesRepo { get; set; }
        public RoomTypeRepository(AppDbContext context) : base(context)
        {
            roomTypesRepo = context.RoomsTypes;
        }
    }
}
