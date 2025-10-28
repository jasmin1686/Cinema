using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Cinema.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);



         void Update(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            CancellationToken cancellationToken = default);


         Task<T?> GetoneAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = true, CancellationToken cancellationToken = default);



         Task<int> commitAsync(CancellationToken cancellationToken = default);
       
    }
}
