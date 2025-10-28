using Cinema.DataAcess;
using Cinema.Models;
using Cinema.Repository.IRepository;

namespace Cinema.Repository
{
    public class MovieImgRepository : Repository<MovieSubimg> ,IMovieImgRepository
    {
        private ApplicationDbContext _context;

        //public MovieImgRepository()
        //{
        //}

        public MovieImgRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Remove(IEnumerable<MovieImgRepository> movies)
        {
            _context.Remove(movies);
        }

        internal async Task CreateAsync(MovieSubimg movieSubimg, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}