using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookifyHotelSystem.DatabaseServices.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private AppDbContext context; 
        public BaseRepository(AppDbContext _context)
        {
            context = _context;
        }


        public T Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();

            return entity;
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }

        public bool Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T Find(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            var entitiesDb = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                    entitiesDb.Include(include);
            }
            return entitiesDb.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetById(string id)
        {
            return context.Set<T>().Find(id)!;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public T Update(int id, T entity)
        {
            //context.Entry(entity).State = EntityState.Detached;
            context.Set<T>().Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
