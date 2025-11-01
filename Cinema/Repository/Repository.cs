using Cinema.DataAcess;
using Cinema.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _Context = context;
            _dbSet = _Context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = await _dbSet.AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
                query = query.AsNoTracking();

            if (filter is not null)
                query = query.Where(filter);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetoneAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            CancellationToken cancellationToken = default)
        {
            return (await GetAsync(filter, tracked, cancellationToken)).FirstOrDefault();
        }

        public async Task<int> commitAsync(CancellationToken cancellationToken = default)
        {
            return await _Context.SaveChangesAsync(cancellationToken);
        }

        public Task AddAsync(object value)
        {
            throw new NotImplementedException();
        }
    }
}