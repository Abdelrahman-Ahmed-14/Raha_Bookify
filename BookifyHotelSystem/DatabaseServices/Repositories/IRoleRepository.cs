using BookifyHotelSystem.DatabaseServices.BaseRepository;
using Microsoft.AspNetCore.Identity;

namespace BookifyHotelSystem.DatabaseServices.Repositories
{
    public interface IRoleRepository : IBaseRepository<IdentityRole>
    {
        IdentityRole GetById(string id);
        List<IdentityRole> GetAll();
        void Insert(IdentityRole entity);
        Task<IdentityResult> Update(string id, IdentityRole entity);
        //int Lockout(IdentityRole entity);
        Task<IdentityResult> Delete(IdentityRole entity);
        //int Active(IdentityRole entity);
    }
}
