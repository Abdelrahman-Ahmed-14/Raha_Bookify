using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Room FindByIdWithInclude(int id);
        List<Room> GetAllWithInclude();
    }
}
