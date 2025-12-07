using BookifyHotelSystem.DatabaseServices.BaseRepository;
using BookifyHotelSystem.Models;
using BookifyHotelSystem.View_Model;
using Microsoft.AspNetCore.Identity;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class UserRepository :BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public UserRepository(AppDbContext _context,
            UserManager<ApplicationUser> _userManager) : base(_context) 
        {
            context = _context;
            userManager = _userManager;
        }

        public int Active(ApplicationUser entity)
        {
            var user = GetById(entity.Id);

            user.LockoutEnd = DateTime.Now.AddDays(-1);

            return context.SaveChanges();
        }

        public int Lockout(ApplicationUser entity)
        {
            ApplicationUser user = GetById(entity.Id);

            user.LockoutEnd = DateTime.Now.AddYears(100);
            return context.SaveChanges();
        }

        public List<ApplicationUser> GetAll()
        {
            return context.Users.ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return context.Users.SingleOrDefault(u => u.Id == id);
        }

        public void Insert(ApplicationUser entity)
        {
            context.Users.Add(entity);
        }

        public async void Update(string id, ApplicationUser entity)
        {
            var userDb = context.Users.SingleOrDefault(u => u.Id == id);

            //userDb.UserName = entity.UserName;
            userDb.Email = entity.Email;
            userDb.FullName = entity.FullName;
            userDb.PasswordHash = entity.PasswordHash;

            //await userManager.UpdateAsync(userDb);

            context.SaveChanges();
        }

        public int Delete(ApplicationUser entity)
        {
            context.Users.Remove(entity);
            return context.SaveChanges();
        }

        public List<AllRolesWithUsersViewModel> GetUsersWithRoles()
        {
            var result = context.UserRoles
                .Join(context.Roles, iur => iur.RoleId, r => r.Id, (iur, r) => new { iur, r })
                .Join(context.Users, iur => iur.iur.UserId, u => u.Id, (iur, u) => new { iur, u });

            var rolesWithUsers = new List<AllRolesWithUsersViewModel>();

            foreach (var item in result)
            {
                rolesWithUsers.Add(new AllRolesWithUsersViewModel
                {
                    UserId = item.u.Id,
                    RoleId = item.iur.r.Id,
                    UserName = item.u.UserName,
                    RoleName = item.iur.r.Name
                });
            }
            return rolesWithUsers;
        }
    }
}
