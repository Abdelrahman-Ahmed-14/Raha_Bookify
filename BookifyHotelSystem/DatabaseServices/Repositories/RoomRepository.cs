using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public DbSet<Room> roomsRepo { get; set; }
        public RoomRepository(AppDbContext context) : base(context)
        {
            roomsRepo = context.Rooms;
        }

        public Room FindByIdWithInclude(int id)
        {
            return roomsRepo.Include("RoomType").SingleOrDefault(r=>r.Id==id)!;
        }

        public List<Room> GetAllWithInclude()
        {
            return roomsRepo.Include("RoomType").ToList();
        }
    }
}
