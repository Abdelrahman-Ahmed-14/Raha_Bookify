using System.Linq.Expressions;

namespace BookifyHotelSystem.DatabaseServices.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        T GetById(string id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> expression, string[]? includes = null);
        T Add(T entity);
        T Update(int id, T entity);
        bool Delete(T entity);
        int Count();
    }
}
