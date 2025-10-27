using Cinema.DataAcess;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cinema.Repository
{
   
    public class Repository<T>where T : class
    {
        private ApplicationDbContext _Context = new();
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _Context = context;
            _dbSet = _Context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity , CancellationToken cancellationToken = default)
        {
          var entitycreate= await _dbSet.AddAsync(entity);
            return entitycreate.Entity;
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>>? Expression = null, bool tracked = true, CancellationToken cancellationToken = default)
        {
            var entities = _dbSet.AsQueryable();
            if(Expression is not null)
            
                entities = entities.Where(Expression);
            return await entities.ToListAsync(cancellationToken);
            if (!tracked)
                entities = entities.AsNoTracking();
        }
        public async Task<T?> GetoneAsync(Expression<Func<T, bool>>? Expression = null, bool tracked = true, CancellationToken cancellationToken = default
            )
        {
            return (await GetAsync(Expression,tracked, cancellationToken)).FirstOrDefault();
        }
        public async Task<int> commitAsync(CancellationToken cancellationToken =default)
        {
            try
            {
                return await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
                return 0;
            }
           
        }
    }
}
