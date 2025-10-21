using Cinema.Models;

namespace Cinema.ViewModels
{
    public class MovieCreateVM
    {
        public IEnumerable<Cinemaa> Cinemaas { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<MovieSubimg>? MovieSubimgs { get; set; }
        public Movie? Movie { get; set; } 


    }
}
