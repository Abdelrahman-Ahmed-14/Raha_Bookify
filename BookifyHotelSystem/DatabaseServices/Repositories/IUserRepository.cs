using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using BookifyHotelSystem.View_Model;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        ApplicationUser GetById(string id);
        List<ApplicationUser> GetAll();
        void Insert(ApplicationUser entity);
        void Update(string id, ApplicationUser entity);
        int Lockout(ApplicationUser entity);
        int Delete(ApplicationUser entity);
        int Active(ApplicationUser entity);
        List<AllRolesWithUsersViewModel> GetUsersWithRoles();
    }
}
