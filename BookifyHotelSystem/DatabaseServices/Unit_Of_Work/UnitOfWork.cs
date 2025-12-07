using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BookifyHotelSystem.DatabaseServices.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext _context, RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> _userManager)
        {
            context = _context;

            BookingRepo = new BookingRepository(context);
            RoleRepo = new RoleRepository(context,roleManager);
            RoomBookingRepo = new RoomBookingRepository(context);
            RoomRepo = new RoomRepository(context);
            RoomTypeRepo = new RoomTypeRepository(context);
            UserRepo = new UserRepository(context,_userManager);

        }


        public IBookingRepository BookingRepo { get; private set; }

        public IRoleRepository RoleRepo { get; private set; }

        public IRoomBookingRepository RoomBookingRepo { get; private set; }

        public IRoomRepository RoomRepo { get; private set; }

        public IRoomTypeRepository RoomTypeRepo { get; private set; }

        public IUserRepository UserRepo { get; private set; }



        public void Dispose()
        {
            context.Dispose();
        }
    }
}
