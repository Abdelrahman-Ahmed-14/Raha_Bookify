using BookifyHotelSystem.DatabaseServices.Repositories;

namespace BookifyHotelSystem.DatabaseServices.Unit_Of_Work
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository BookingRepo { get; }
        IRoleRepository RoleRepo { get; }
        IRoomBookingRepository RoomBookingRepo { get; }
        IRoomRepository RoomRepo { get; }
        IRoomTypeRepository RoomTypeRepo { get; }
        IUserRepository UserRepo { get; }

    }
}
