using BookifyHotelSystem.DatabaseServices.BaseRepository;
using Microsoft.AspNetCore.Identity;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole>,IRoleRepository
    {
        private readonly AppDbContext context;
        RoleManager<IdentityRole> roleManager;
        public RoleRepository(AppDbContext _context, RoleManager<IdentityRole> roleManager) : base(_context)
        {
            context = _context;
            this.roleManager = roleManager;
        }


        public async Task<IdentityResult> Delete(IdentityRole entity)
        {
            return await roleManager.DeleteAsync(entity);

        }

        public List<IdentityRole> GetAll()
        {
            return context.Roles.ToList();
        }

        public IdentityRole GetById(string id)
        {
            return context.Roles.SingleOrDefault(r => r.Id == id);
        }

        public void Insert(IdentityRole entity)
        {
            context.Roles.Add(entity);
        }

        public async Task<IdentityResult> Update(string id, IdentityRole entity)
        {
            var role = GetById(id);

            role.Name = entity.Name;

            return await roleManager.UpdateAsync(role);
        }
    }
}
