using Cinema.Models;

namespace Cinema.Repository.IRepository
{
    public interface IMovieImgRepository : IRepository<MovieSubimg>
    {
        void Remove(IEnumerable<MovieImgRepository> movies);
    }
}
